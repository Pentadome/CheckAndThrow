using System.Collections.Immutable;
using System.Composition;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;

namespace CheckAndThrow.Analyzers.Diagnostics.AddNotNullGuard;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AddNotNullGuardCodeFixProvider)), Shared]
public sealed class AddNotNullGuardCodeFixProvider : CodeFixProvider
{
    private const string Title = "Add Check.Arg.NotNull guard";

    public override ImmutableArray<string> FixableDiagnosticIds =>
        ImmutableArray.Create(AddNotNullGuardAnalyzer.DiagnosticId);

    public override FixAllProvider? GetFixAllProvider() => null;

    public override Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        context.RegisterCodeFix(
            CodeAction.Create(
                Title,
                cancellationToken =>
                    AddGuardAsync(context.Document, context.Diagnostics[0], cancellationToken),
                equivalenceKey: Title
            ),
            context.Diagnostics[0]
        );

        return Task.CompletedTask;
    }

    private static async Task<Document> AddGuardAsync(
        Document document,
        Diagnostic diagnostic,
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
        {
            return document;
        }

        var parameter = root.FindToken(diagnostic.Location.SourceSpan.Start)
            .Parent?.AncestorsAndSelf()
            .OfType<ParameterSyntax>()
            .FirstOrDefault();
        var parameterSymbol = parameter is null
            ? null
            : semanticModel.GetDeclaredSymbol(parameter, cancellationToken);
        var guard = AddNotNullGuardAnalyzer.FindGuard(compilation);

        if (
            parameter is null
            || parameterSymbol is null
            || guard is null
            || parameter.Parent?.Parent
                is not BaseMethodDeclarationSyntax { Body: { } body } declaration
            || !AddNotNullGuardAnalyzer.IsEligible(parameter, parameterSymbol)
            || AddNotNullGuardAnalyzer.HasExistingGuard(
                body,
                parameterSymbol,
                guard,
                semanticModel,
                cancellationToken
            )
        )
        {
            return document;
        }

        var statement = SyntaxFactory
            .ParseStatement(
                $"global::CheckAndThrow.Check.Arg.NotNull({parameter.Identifier.Text});"
            )
            .WithAdditionalAnnotations(Formatter.Annotation);
        var replacement = declaration.WithBody(
            body.WithStatements(body.Statements.Insert(0, statement))
        );

        return document.WithSyntaxRoot(root.ReplaceNode(declaration, replacement));
    }
}
