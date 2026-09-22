using System.Collections.Immutable;
using System.Composition;
using CheckAndThrow.Analyzers.Diagnostics.PropertyGuard;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;

namespace CheckAndThrow.Analyzers.Diagnostics.AddRangeGuard;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AddRangeGuardCodeFixProvider)), Shared]
public sealed class AddRangeGuardCodeFixProvider : CodeFixProvider
{
    const string Title = "Add range guard clause";

    public override ImmutableArray<string> FixableDiagnosticIds =>
        ImmutableArray.Create(AddRangeGuardAnalyzer.DiagnosticId);

    public override FixAllProvider? GetFixAllProvider() => null;

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context
            .Document.GetSyntaxRootAsync(context.CancellationToken)
            .ConfigureAwait(false);
        var model = await context
            .Document.GetSemanticModelAsync(context.CancellationToken)
            .ConfigureAwait(false);
        var compilation = await context
            .Document.Project.GetCompilationAsync(context.CancellationToken)
            .ConfigureAwait(false);
        if (root is null || model is null || compilation is null)
            return;

        var propertyTarget = PropertyGuardSupport.FindTarget(
            root,
            context.Diagnostics[0],
            model,
            context.CancellationToken
        );
        var parameter = root.FindToken(context.Diagnostics[0].Location.SourceSpan.Start)
            .Parent?.AncestorsAndSelf()
            .OfType<ParameterSyntax>()
            .FirstOrDefault();
        var symbol =
            propertyTarget?.ValueParameter
            ?? (
                parameter is null
                    ? null
                    : model.GetDeclaredSymbol(parameter, context.CancellationToken)
            );
        var arg = AddRangeGuardAnalyzer.FindArgType(compilation);
        if (
            arg is null
            || symbol is null
            || propertyTarget is null
                && (parameter is null || !AddRangeGuardAnalyzer.IsEligible(parameter, symbol))
            || propertyTarget is not null && !AddRangeGuardAnalyzer.IsEligible(propertyTarget)
        )
            return;

        var actions = AddRangeGuardAnalyzer
            .Guards.Where(guard =>
                AddRangeGuardAnalyzer.BindGuard(
                    arg,
                    guard,
                    symbol,
                    model,
                    propertyTarget?.Declaration.SpanStart ?? parameter!.SpanStart,
                    context.CancellationToken
                )
                    is not null
            )
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
        AddRangeGuardAnalyzer.Guard guard,
        CancellationToken cancellationToken
    )
    {
        var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
        var model = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);
        var compilation = await document
            .Project.GetCompilationAsync(cancellationToken)
            .ConfigureAwait(false);
        if (root is null || model is null || compilation is null)
            return document;

        var propertyTarget = PropertyGuardSupport.FindTarget(
            root,
            diagnostic,
            model,
            cancellationToken
        );
        var arg = AddRangeGuardAnalyzer.FindArgType(compilation);
        if (propertyTarget is not null)
        {
            if (
                arg is null
                || !AddRangeGuardAnalyzer.IsEligible(propertyTarget)
                || AddRangeGuardAnalyzer.HasExistingGuard(
                    propertyTarget,
                    arg,
                    model,
                    cancellationToken
                )
                || AddRangeGuardAnalyzer.BindGuard(
                    arg,
                    guard,
                    propertyTarget.ValueParameter,
                    model,
                    propertyTarget.Declaration.SpanStart,
                    cancellationToken
                )
                    is null
            )
                return document;

            var propertyRoot = PropertyGuardSupport.AddGuard(
                root,
                propertyTarget,
                $"global::CheckAndThrow.Check.Arg.{guard.MethodName}({propertyTarget.ValueParameter.Name})",
                compilation
            );
            return document.WithSyntaxRoot(
                propertyRoot.WithAdditionalAnnotations(Formatter.Annotation)
            );
        }

        var parameter = root.FindToken(diagnostic.Location.SourceSpan.Start)
            .Parent?.AncestorsAndSelf()
            .OfType<ParameterSyntax>()
            .FirstOrDefault();
        var symbol = parameter is null
            ? null
            : model.GetDeclaredSymbol(parameter, cancellationToken);
        if (
            parameter is null
            || symbol is null
            || arg is null
            || parameter.Parent?.Parent is not BaseMethodDeclarationSyntax declaration
            || !AddRangeGuardAnalyzer.IsEligible(parameter, symbol)
            || AddRangeGuardAnalyzer.HasExistingGuard(
                declaration,
                symbol,
                arg,
                model,
                cancellationToken
            )
            || AddRangeGuardAnalyzer.BindGuard(
                arg,
                guard,
                symbol,
                model,
                parameter.SpanStart,
                cancellationToken
            )
                is null
        )
            return document;

        var call =
            $"global::CheckAndThrow.Check.Arg.{guard.MethodName}({parameter.Identifier.Text})";
        if (declaration.Body is { } body)
        {
            if (
                TryInline(
                    body,
                    parameter,
                    symbol,
                    call,
                    model,
                    cancellationToken,
                    out var replacement
                )
            )
                return document.WithSyntaxRoot(root.ReplaceNode(body, replacement));

            var statement = SyntaxFactory
                .ParseStatement(call + ";")
                .WithAdditionalAnnotations(Formatter.Annotation);
            return document.WithSyntaxRoot(
                root.ReplaceNode(
                    declaration,
                    declaration.WithBody(body.WithStatements(body.Statements.Insert(0, statement)))
                )
            );
        }

        if (declaration.ExpressionBody is not { } expressionBody)
            return document;

        var guardStatement = SyntaxFactory
            .ParseStatement(call + ";")
            .WithAdditionalAnnotations(Formatter.Annotation);
        StatementSyntax expressionStatement = declaration switch
        {
            ConstructorDeclarationSyntax => SyntaxFactory.ExpressionStatement(
                expressionBody.Expression
            ),
            MethodDeclarationSyntax method
                when method.ReturnType
                    is PredefinedTypeSyntax { Keyword.RawKind: (int)SyntaxKind.VoidKeyword } =>
                SyntaxFactory.ExpressionStatement(expressionBody.Expression),
            _ => SyntaxFactory.ReturnStatement(expressionBody.Expression),
        };
        var block = SyntaxFactory
            .Block(guardStatement, expressionStatement)
            .WithTriviaFrom(expressionBody);
        return document.WithSyntaxRoot(
            root.ReplaceNode(
                declaration,
                declaration.WithBody(block).WithExpressionBody(null).WithSemicolonToken(default)
            )
        );
    }

    static bool TryInline(
        BlockSyntax body,
        ParameterSyntax parameter,
        IParameterSymbol symbol,
        string call,
        SemanticModel model,
        CancellationToken cancellationToken,
        out BlockSyntax replacement
    )
    {
        replacement = body;
        if (
            body.Statements.FirstOrDefault() is not LocalDeclarationStatementSyntax local
            || local.Declaration.Variables.Count != 1
        )
            return false;

        var value = local.Declaration.Variables[0].Initializer?.Value;
        var receiver = value switch
        {
            MemberAccessExpressionSyntax { Expression: IdentifierNameSyntax identifier } =>
                identifier,
            InvocationExpressionSyntax
            {
                Expression: MemberAccessExpressionSyntax
                {
                    Expression: IdentifierNameSyntax identifier
                }
            } => identifier,
            _ => null,
        };
        if (
            receiver is null
            || !SymbolEqualityComparer.Default.Equals(
                model.GetSymbolInfo(receiver, cancellationToken).Symbol,
                symbol
            )
        )
            return false;

        var guarded = SyntaxFactory
            .ParseExpression(call)
            .WithTriviaFrom(receiver)
            .WithAdditionalAnnotations(Formatter.Annotation);
        replacement = body.ReplaceNode(receiver, guarded);
        return true;
    }
}
