using System.Collections.Immutable;
using System.Composition;
using CheckAndThrow.Analyzers.Diagnostics.AddNotNullGuard;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;

namespace CheckAndThrow.Analyzers.Diagnostics.AddNotNullGuards;

[
    ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AddNotNullGuardsCodeFixProvider)),
    Shared
]
public sealed class AddNotNullGuardsCodeFixProvider : CodeFixProvider
{
    const string Title = "Add Check.Args.NotNull guard";

    public override ImmutableArray<string> FixableDiagnosticIds =>
        ImmutableArray.Create(AddNotNullGuardsAnalyzer.DiagnosticId);

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

    static async Task<Document> AddGuardAsync(
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

        var declaration = root.FindToken(diagnostic.Location.SourceSpan.Start)
            .Parent?.AncestorsAndSelf()
            .OfType<BaseMethodDeclarationSyntax>()
            .FirstOrDefault();
        var singleGuard = AddNotNullGuardAnalyzer.FindGuard(compilation);
        var argsGuardType = AddNotNullGuardAnalyzer.FindArgsGuardType(compilation);
        if (declaration is null || singleGuard is null || argsGuardType is null)
        {
            return document;
        }

        var parameters = AddNotNullGuardsAnalyzer.GetEligibleParameters(
            declaration,
            semanticModel,
            cancellationToken
        );
        if (
            parameters.Length is < 2 or > 16
            || AddNotNullGuardsAnalyzer.FindGuard(argsGuardType, parameters.Length) is null
            || (
                declaration.Body is { } existingBody
                && AddNotNullGuardsAnalyzer.IsNormalizedGuard(
                    existingBody,
                    parameters,
                    argsGuardType,
                    semanticModel,
                    cancellationToken
                )
            )
        )
        {
            return document;
        }

        var arguments = string.Join(
            ", ",
            declaration
                .ParameterList.Parameters.Where(parameter =>
                    parameters.Any(symbol =>
                        SymbolEqualityComparer.Default.Equals(
                            symbol,
                            semanticModel.GetDeclaredSymbol(parameter, cancellationToken)
                        )
                    )
                )
                .Select(parameter => parameter.Identifier.Text)
        );
        var bulkCall = $"global::CheckAndThrow.Check.Args.NotNull({arguments})";
        if (declaration.Body is not { } body)
        {
            var tuple = AddNotNullGuardsAnalyzer.GetConstructorTuple(
                declaration,
                parameters,
                singleGuard,
                semanticModel,
                cancellationToken
            );
            return tuple is null
                ? document
                : document.WithSyntaxRoot(
                    root.ReplaceNode(
                        tuple,
                        SyntaxFactory.ParseExpression(bulkCall).WithTriviaFrom(tuple)
                    )
                );
        }

        var indexByParameter = parameters
            .Select((parameter, index) => (parameter, index))
            .ToDictionary(
                item => item.parameter,
                item => item.index,
                SymbolEqualityComparer.Default
            );
        var rewrites = new Dictionary<StatementSyntax, StatementSyntax?>();
        var tupleName = FindTupleName(body);
        var usesTupleResult = false;

        foreach (var statement in body.Statements)
        {
            var invocation = AddNotNullGuardAnalyzer.GetTopLevelGuardInvocation(statement);
            if (invocation is null)
            {
                continue;
            }

            if (
                TryGetIndividualParameter(
                    invocation,
                    singleGuard,
                    semanticModel,
                    cancellationToken,
                    out var parameter
                ) && indexByParameter.TryGetValue(parameter, out var index)
            )
            {
                var member = SyntaxFactory.ParseExpression($"{tupleName}.arg{index + 1}");
                switch (statement)
                {
                    case ExpressionStatementSyntax { Expression: InvocationExpressionSyntax }:
                        rewrites[statement] = null;
                        break;
                    case ExpressionStatementSyntax
                    {
                        Expression: AssignmentExpressionSyntax assignment,
                    }:
                        rewrites[statement] = statement.ReplaceNode(invocation, member);
                        usesTupleResult = true;
                        break;
                    case LocalDeclarationStatementSyntax:
                        rewrites[statement] = statement.ReplaceNode(invocation, member);
                        usesTupleResult = true;
                        break;
                }

                continue;
            }

            if (
                AddNotNullGuardsAnalyzer.IsArgsGuard(
                    invocation,
                    argsGuardType,
                    semanticModel,
                    cancellationToken
                )
                && invocation.ArgumentList.Arguments.All(argument =>
                    indexByParameter.ContainsKey(
                        semanticModel.GetSymbolInfo(argument.Expression, cancellationToken).Symbol!
                    )
                )
            )
            {
                rewrites[statement] = null;
            }
        }

        // Combine only an uninterrupted, parameter-ordered prefix with simple targets.
        // Otherwise keep the tuple local so target evaluation and declaration order stay intact.
        var targets = new List<string>();
        foreach (var statement in body.Statements.Take(parameters.Length))
        {
            var invocation = AddNotNullGuardAnalyzer.GetTopLevelGuardInvocation(statement);
            if (
                invocation is null
                || invocation.ArgumentList.Arguments.Count != 1
                || !TryGetIndividualParameter(
                    invocation,
                    singleGuard,
                    semanticModel,
                    cancellationToken,
                    out var parameter
                )
                || !SymbolEqualityComparer.Default.Equals(parameter, parameters[targets.Count])
                || statement.ContainsDirectives
                || statement
                    .DescendantTrivia()
                    .Any(trivia =>
                        trivia.IsKind(SyntaxKind.SingleLineCommentTrivia)
                        || trivia.IsKind(SyntaxKind.MultiLineCommentTrivia)
                    )
            )
                break;

            if (
                statement
                    is ExpressionStatementSyntax
                    {
                        Expression: AssignmentExpressionSyntax
                        {
                            RawKind: (int)SyntaxKind.SimpleAssignmentExpression
                        } assignment
                    }
                && (
                    assignment.Left is IdentifierNameSyntax
                    || assignment.Left
                        is MemberAccessExpressionSyntax { Expression: ThisExpressionSyntax }
                )
                && semanticModel.GetSymbolInfo(assignment.Left, cancellationToken).Symbol
                    is IFieldSymbol
                        or ILocalSymbol
                        or IParameterSymbol
            )
                targets.Add(assignment.Left.ToString());
            else if (
                statement is LocalDeclarationStatementSyntax local
                && local.Modifiers.Count == 0
                && local.Declaration.Variables.Count == 1
            )
                targets.Add(
                    $"{local.Declaration.Type} {local.Declaration.Variables[0].Identifier}"
                );
            else
                break;
        }
        var combine = targets.Count == parameters.Length && rewrites.Count == parameters.Length;
        if (combine)
            foreach (var statement in body.Statements.Take(parameters.Length))
                rewrites[statement] = null;
        var guardStatement = SyntaxFactory
            .ParseStatement(
                combine ? $"({string.Join(", ", targets)}) = {bulkCall};"
                : usesTupleResult ? $"var {tupleName} = {bulkCall};"
                : $"{bulkCall};"
            )
            .WithAdditionalAnnotations(Formatter.Annotation);

        var statements = SyntaxFactory.List(
            new[] { guardStatement }.Concat(
                body.Statements.Where(statement =>
                        !rewrites.TryGetValue(statement, out var replacement)
                        || replacement is not null
                    )
                    .Select(statement =>
                        rewrites.TryGetValue(statement, out var replacement)
                            ? replacement!
                            : statement
                    )
            )
        );
        var replacementDeclaration = declaration.WithBody(body.WithStatements(statements));

        return document.WithSyntaxRoot(root.ReplaceNode(declaration, replacementDeclaration));
    }

    static bool TryGetIndividualParameter(
        InvocationExpressionSyntax invocation,
        IMethodSymbol guard,
        SemanticModel semanticModel,
        CancellationToken cancellationToken,
        out IParameterSymbol parameter
    )
    {
        parameter = null!;
        if (
            !SymbolEqualityComparer.Default.Equals(
                semanticModel
                    .GetSymbolInfo(invocation, cancellationToken)
                    .Symbol?.OriginalDefinition,
                guard.OriginalDefinition
            ) || invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression is not { } argument
        )
        {
            return false;
        }

        if (
            semanticModel.GetSymbolInfo(argument, cancellationToken).Symbol
            is not IParameterSymbol resolved
        )
        {
            return false;
        }

        parameter = resolved;
        return true;
    }

    static string FindTupleName(BlockSyntax body)
    {
        var names = new HashSet<string>(
            body.DescendantTokens()
                .Where(token => token.IsKind(SyntaxKind.IdentifierToken))
                .Select(token => token.ValueText),
            StringComparer.Ordinal
        );
        var name = "checkAndThrowArgs";
        var suffix = 1;
        while (names.Contains(name))
        {
            name = $"checkAndThrowArgs{suffix++}";
        }

        return name;
    }
}
