using System.Collections.Immutable;
using System.Composition;
using CheckAndThrow.Analyzers.Diagnostics.AddNotNullGuard;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;

namespace CheckAndThrow.Analyzers.Diagnostics.AddStringGuard;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AddStringGuardCodeFixProvider)), Shared]
public sealed class AddStringGuardCodeFixProvider : CodeFixProvider
{
    const string Title = "Add string guard clause";

    public override ImmutableArray<string> FixableDiagnosticIds =>
        ImmutableArray.Create(AddStringGuardAnalyzer.DiagnosticId);

    public override FixAllProvider? GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var compilation = await context
            .Document.Project.GetCompilationAsync(context.CancellationToken)
            .ConfigureAwait(false);
        if (compilation is null)
            return;

        var arg = AddStringGuardAnalyzer.FindArgType(compilation);
        if (arg is null)
            return;

        var actions = AddStringGuardAnalyzer
            .Guards.Where(guard => AddStringGuardAnalyzer.FindGuard(arg, guard) is not null)
            .Select(guard =>
                CodeAction.Create(
                    guard.Title,
                    cancellationToken =>
                        AddGuardAsync(
                            context.Document,
                            context.Diagnostics[0],
                            guard,
                            cancellationToken
                        ),
                    equivalenceKey: $"{Title}:{guard.MethodName}"
                )
            )
            .ToImmutableArray();
        if (actions.IsEmpty)
            return;

        context.RegisterCodeFix(
            CodeAction.Create(Title, actions, isInlinable: false),
            context.Diagnostics[0]
        );
    }

    public static async Task<Document> AddGuardAsync(
        Document document,
        Diagnostic diagnostic,
        AddStringGuardAnalyzer.Guard guard,
        CancellationToken cancellationToken
    )
    {
        var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
        var semanticModel = await document
            .GetSemanticModelAsync(cancellationToken)
            .ConfigureAwait(false);
        var compilation = await document
            .Project.GetCompilationAsync(cancellationToken)
            .ConfigureAwait(false);
        if (root is null || semanticModel is null || compilation is null)
            return document;

        var parameter = root.FindToken(diagnostic.Location.SourceSpan.Start)
            .Parent?.AncestorsAndSelf()
            .OfType<ParameterSyntax>()
            .FirstOrDefault();
        var parameterSymbol = parameter is null
            ? null
            : semanticModel.GetDeclaredSymbol(parameter, cancellationToken);
        var arg = AddStringGuardAnalyzer.FindArgType(compilation);
        if (
            parameter is null
            || parameterSymbol is null
            || arg is null
            || AddStringGuardAnalyzer.FindGuard(arg, guard) is null
            || parameter.Parent?.Parent
                is not BaseMethodDeclarationSyntax { Body: { } body } declaration
            || !AddStringGuardAnalyzer.IsEligible(parameter, parameterSymbol)
            || AddStringGuardAnalyzer.HasExistingGuard(
                body,
                parameterSymbol,
                arg,
                semanticModel,
                cancellationToken
            )
        )
            return document;

        var callTarget = $"global::CheckAndThrow.Check.Arg.{guard.MethodName}";
        var notNull = AddNotNullGuardAnalyzer.FindGuard(compilation);
        var existingNotNull = notNull is null
            ? null
            : AddStringGuardAnalyzer.FindExistingNotNull(
                body,
                parameterSymbol,
                notNull,
                semanticModel,
                cancellationToken
            );
        if (existingNotNull is not null)
        {
            var replacement = existingNotNull
                .WithExpression(
                    SyntaxFactory
                        .ParseExpression(callTarget)
                        .WithTriviaFrom(existingNotNull.Expression)
                )
                .WithAdditionalAnnotations(Formatter.Annotation);
            return document.WithSyntaxRoot(root.ReplaceNode(existingNotNull, replacement));
        }

        var call = $"{callTarget}({parameter.Identifier.Text})";
        if (
            body.Statements.FirstOrDefault() is LocalDeclarationStatementSyntax local
            && local.Declaration.Variables.Count == 1
            && local.Declaration.Variables[0].Initializer?.Value
                is MemberAccessExpressionSyntax { Expression: IdentifierNameSyntax receiver }
            && SymbolEqualityComparer.Default.Equals(
                semanticModel.GetSymbolInfo(receiver, cancellationToken).Symbol,
                parameterSymbol
            )
        )
        {
            var inlineGuard = SyntaxFactory
                .ParseExpression(call)
                .WithTriviaFrom(receiver)
                .WithAdditionalAnnotations(Formatter.Annotation);
            return document.WithSyntaxRoot(root.ReplaceNode(receiver, inlineGuard));
        }

        var statement = SyntaxFactory
            .ParseStatement(call + ";")
            .WithAdditionalAnnotations(Formatter.Annotation);
        var replacementDeclaration = declaration.WithBody(
            body.WithStatements(body.Statements.Insert(0, statement))
        );
        return document.WithSyntaxRoot(root.ReplaceNode(declaration, replacementDeclaration));
    }
}
