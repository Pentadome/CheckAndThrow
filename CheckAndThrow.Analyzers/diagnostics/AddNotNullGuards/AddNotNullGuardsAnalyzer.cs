using System.Collections.Immutable;
using CheckAndThrow.Analyzers.Diagnostics.AddNotNullGuard;
using CheckAndThrow.Analyzers.Diagnostics.AddStringGuard;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CheckAndThrow.Analyzers.Diagnostics.AddNotNullGuards;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class AddNotNullGuardsAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "CAT0002";

    internal static readonly DiagnosticDescriptor Rule = new(
        DiagnosticId,
        "Add Check.Args.NotNull guard",
        "Add Check.Args.NotNull guard",
        "Style",
        DiagnosticSeverity.Hidden,
        isEnabledByDefault: true
    );

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterCompilationStartAction(startContext =>
        {
            var singleGuard = AddNotNullGuardAnalyzer.FindGuard(startContext.Compilation);
            var argsGuardType = AddNotNullGuardAnalyzer.FindArgsGuardType(startContext.Compilation);
            if (singleGuard is null || argsGuardType is null)
            {
                return;
            }

            startContext.RegisterSyntaxNodeAction(
                context => AnalyzeDeclaration(context, singleGuard, argsGuardType),
                SyntaxKind.MethodDeclaration,
                SyntaxKind.ConstructorDeclaration
            );
        });
    }

    internal static IMethodSymbol? FindGuard(INamedTypeSymbol argsGuardType, int arity)
    {
        return argsGuardType
            .GetMembers("NotNull")
            .OfType<IMethodSymbol>()
            .SingleOrDefault(method =>
                method.Arity == arity
                && method.Parameters.Length == arity * 2
                && method.Parameters.Take(arity).All(parameter => !parameter.IsOptional)
                && method.Parameters.Skip(arity).All(parameter => parameter.IsOptional)
            );
    }

    internal static ImmutableArray<IParameterSymbol> GetEligibleParameters(
        BaseMethodDeclarationSyntax declaration,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    )
    {
        var arg = AddStringGuardAnalyzer.FindArgType(semanticModel.Compilation);
        return declaration
            .ParameterList.Parameters.Select(parameter =>
                (
                    Parameter: parameter,
                    Symbol: semanticModel.GetDeclaredSymbol(parameter, cancellationToken)
                )
            )
            .Where(item =>
                item.Symbol is not null
                && AddNotNullGuardAnalyzer.IsEligible(
                    item.Parameter,
                    item.Symbol,
                    allowExpressionBody: declaration is ConstructorDeclarationSyntax
                )
                && (
                    declaration.Body is not { } body
                    || arg is null
                    || !AddStringGuardAnalyzer.HasExistingGuard(
                        body,
                        item.Symbol,
                        arg,
                        semanticModel,
                        cancellationToken
                    )
                )
            )
            .Select(item => item.Symbol!)
            .ToImmutableArray();
    }

    internal static bool IsNormalizedGuard(
        BlockSyntax body,
        ImmutableArray<IParameterSymbol> parameters,
        INamedTypeSymbol argsGuardType,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    )
    {
        foreach (var statement in body.Statements)
        {
            var invocation = AddNotNullGuardAnalyzer.GetTopLevelGuardInvocation(statement);
            if (
                invocation is null
                || !IsArgsGuard(invocation, argsGuardType, semanticModel, cancellationToken)
            )
            {
                continue;
            }

            if (invocation.ArgumentList.Arguments.Count != parameters.Length)
            {
                continue;
            }

            if (
                invocation
                    .ArgumentList.Arguments.Select(argument =>
                        semanticModel.GetSymbolInfo(argument.Expression, cancellationToken).Symbol
                    )
                    .SequenceEqual(parameters, SymbolEqualityComparer.Default)
            )
            {
                return true;
            }
        }

        return false;
    }

    internal static bool IsArgsGuard(
        InvocationExpressionSyntax invocation,
        INamedTypeSymbol argsGuardType,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    )
    {
        var method =
            semanticModel.GetSymbolInfo(invocation, cancellationToken).Symbol as IMethodSymbol;
        return method?.Name == "NotNull"
            && SymbolEqualityComparer.Default.Equals(method.ContainingType, argsGuardType);
    }

    static void AnalyzeDeclaration(
        SyntaxNodeAnalysisContext context,
        IMethodSymbol singleGuard,
        INamedTypeSymbol argsGuardType
    )
    {
        var declaration = (BaseMethodDeclarationSyntax)context.Node;
        var parameters = GetEligibleParameters(
            declaration,
            context.SemanticModel,
            context.CancellationToken
        );
        if (
            parameters.Length is < 2 or > 16
            || FindGuard(argsGuardType, parameters.Length) is null
            || (
                declaration.Body is null
                    ? GetConstructorTuple(
                        declaration,
                        parameters,
                        singleGuard,
                        context.SemanticModel,
                        context.CancellationToken
                    )
                        is null
                    : IsNormalizedGuard(
                        declaration.Body,
                        parameters,
                        argsGuardType,
                        context.SemanticModel,
                        context.CancellationToken
                    )
                        || HasUnsupportedConsumedGuard(
                            declaration.Body,
                            parameters,
                            singleGuard,
                            context.SemanticModel,
                            context.CancellationToken
                        )
            )
        )
        {
            return;
        }

        GuardSeverity.Report(
            context,
            Rule,
            declaration.ParameterList.GetLocation(),
            context.SemanticModel.GetDeclaredSymbol(declaration, context.CancellationToken)
        );
    }

    internal static TupleExpressionSyntax? GetConstructorTuple(
        BaseMethodDeclarationSyntax declaration,
        ImmutableArray<IParameterSymbol> parameters,
        IMethodSymbol singleGuard,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    )
    {
        if (
            declaration
                is not ConstructorDeclarationSyntax
                {
                    ExpressionBody.Expression: AssignmentExpressionSyntax
                    {
                        RawKind: (int)SyntaxKind.SimpleAssignmentExpression,
                        Left: TupleExpressionSyntax,
                        Right: TupleExpressionSyntax tuple
                    }
                }
            || tuple.Arguments.Count != parameters.Length
        )
        {
            return null;
        }

        for (var index = 0; index < parameters.Length; index++)
        {
            ExpressionSyntax expression = tuple.Arguments[index].Expression;
            if (expression is InvocationExpressionSyntax invocation)
            {
                if (
                    invocation.ArgumentList.Arguments.Count != 1
                    || !SymbolEqualityComparer.Default.Equals(
                        semanticModel
                            .GetSymbolInfo(invocation, cancellationToken)
                            .Symbol?.OriginalDefinition,
                        singleGuard.OriginalDefinition
                    )
                )
                {
                    return null;
                }

                expression = invocation.ArgumentList.Arguments[0].Expression;
            }

            if (
                !SymbolEqualityComparer.Default.Equals(
                    semanticModel.GetSymbolInfo(expression, cancellationToken).Symbol,
                    parameters[index]
                )
            )
            {
                return null;
            }
        }

        return tuple;
    }

    static bool HasUnsupportedConsumedGuard(
        BlockSyntax body,
        ImmutableArray<IParameterSymbol> parameters,
        IMethodSymbol singleGuard,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    )
    {
        foreach (var invocation in body.DescendantNodes().OfType<InvocationExpressionSyntax>())
        {
            if (
                !SymbolEqualityComparer.Default.Equals(
                    semanticModel
                        .GetSymbolInfo(invocation, cancellationToken)
                        .Symbol?.OriginalDefinition,
                    singleGuard.OriginalDefinition
                )
                || invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression
                    is not { } argument
                || !parameters.Contains(
                    semanticModel.GetSymbolInfo(argument, cancellationToken).Symbol,
                    SymbolEqualityComparer.Default
                )
            )
            {
                continue;
            }

            var statement = invocation.Ancestors().OfType<StatementSyntax>().FirstOrDefault();
            if (
                statement is null
                || !ReferenceEquals(
                    AddNotNullGuardAnalyzer.GetTopLevelGuardInvocation(statement),
                    invocation
                )
            )
            {
                return true;
            }
        }

        return false;
    }
}
