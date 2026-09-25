using System.Collections.Immutable;
using CheckAndThrow.Analyzers.Diagnostics.AddNotNullGuard;
using CheckAndThrow.Analyzers.Diagnostics.PropertyGuard;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace CheckAndThrow.Analyzers.Diagnostics.AddStringGuard;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class AddStringGuardAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "CAT0004";

    internal static readonly DiagnosticDescriptor Rule = new(
        DiagnosticId,
        "Add string guard clause",
        "Add string guard clause for '{0}'",
        "Style",
        DiagnosticSeverity.Hidden,
        isEnabledByDefault: true
    );

    public static readonly ImmutableArray<Guard> Guards = ImmutableArray.Create(
        new Guard("Add Check.Arg.NotNullOrEmpty guard", "NotNullOrEmpty"),
        new Guard("Add Check.Arg.NotNullOrWhiteSpace guard", "NotNullOrWhiteSpace")
    );

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterCompilationStartAction(startContext =>
        {
            var arg = FindArgType(startContext.Compilation);
            if (arg is null || !Guards.Any(guard => FindGuard(arg, guard) is not null))
                return;

            startContext.RegisterSyntaxNodeAction(
                nodeContext => AnalyzeNode(nodeContext, arg),
                SyntaxKind.Parameter,
                SyntaxKind.PropertyDeclaration
            );
        });
    }

    internal static INamedTypeSymbol? FindArgType(Compilation compilation) =>
        compilation
            .GetTypeByMetadataName("CheckAndThrow.Check")
            ?.GetTypeMembers("Arg")
            .SingleOrDefault();

    internal static IMethodSymbol? FindGuard(INamedTypeSymbol arg, Guard guard) =>
        arg.GetMembers(guard.MethodName)
            .OfType<IMethodSymbol>()
            .SingleOrDefault(method =>
                method.Arity == 0
                && method.Parameters.Length == 2
                && method.Parameters[0].Type.SpecialType == SpecialType.System_String
            );

    internal static bool IsEligible(ParameterSyntax parameter, IParameterSymbol symbol) =>
        symbol.Type.SpecialType == SpecialType.System_String
        && AddNotNullGuardAnalyzer.IsEligible(parameter, symbol);

    internal static bool IsEligible(PropertyGuardTarget target) =>
        target.PropertySymbol.Type.SpecialType == SpecialType.System_String
        && AddNotNullGuardAnalyzer.IsEligible(target);

    internal static bool HasExistingGuard(
        BlockSyntax body,
        IParameterSymbol parameter,
        INamedTypeSymbol arg,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    ) =>
        GetGuardInvocations(body)
            .Any(invocation =>
                IsStringGuardForParameter(
                    invocation,
                    parameter,
                    arg,
                    semanticModel,
                    cancellationToken
                )
            );

    internal static bool HasExistingGuard(
        PropertyGuardTarget target,
        INamedTypeSymbol arg,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    ) =>
        GetGuardInvocations(target)
            .Any(invocation =>
                IsStringGuardForParameter(
                    invocation,
                    target.ValueParameter,
                    arg,
                    semanticModel,
                    cancellationToken
                )
            );

    internal static InvocationExpressionSyntax? FindExistingNotNull(
        BlockSyntax body,
        IParameterSymbol parameter,
        IMethodSymbol notNull,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    ) =>
        FindExistingNotNull(
            GetGuardInvocations(body),
            parameter,
            notNull,
            semanticModel,
            cancellationToken
        );

    internal static InvocationExpressionSyntax? FindExistingNotNull(
        PropertyGuardTarget target,
        IMethodSymbol notNull,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    ) =>
        FindExistingNotNull(
            GetGuardInvocations(target),
            target.ValueParameter,
            notNull,
            semanticModel,
            cancellationToken
        );

    static InvocationExpressionSyntax? FindExistingNotNull(
        IEnumerable<InvocationExpressionSyntax> invocations,
        IParameterSymbol parameter,
        IMethodSymbol notNull,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    ) =>
        invocations.FirstOrDefault(invocation =>
            SymbolEqualityComparer.Default.Equals(
                semanticModel
                    .GetSymbolInfo(invocation, cancellationToken)
                    .Symbol?.OriginalDefinition,
                notNull.OriginalDefinition
            ) && IsValueArgument(invocation, parameter, semanticModel, cancellationToken)
        );

    static IEnumerable<InvocationExpressionSyntax> GetGuardInvocations(
        PropertyGuardTarget target
    ) => PropertyGuardSupport.GetTopLevelGuardInvocations(target.Setter);

    static IEnumerable<InvocationExpressionSyntax> GetGuardInvocations(BlockSyntax body)
    {
        foreach (var statement in body.Statements)
        {
            var invocation = AddNotNullGuardAnalyzer.GetTopLevelGuardInvocation(statement);
            if (invocation is not null)
                yield return invocation;

            if (
                statement is LocalDeclarationStatementSyntax local
                && local.Declaration.Variables.Count == 1
                && local.Declaration.Variables[0].Initializer?.Value
                    is MemberAccessExpressionSyntax
                    {
                        Expression: InvocationExpressionSyntax inlineGuard
                    }
            )
                yield return inlineGuard;
        }
    }

    static bool IsStringGuardForParameter(
        InvocationExpressionSyntax invocation,
        IParameterSymbol parameter,
        INamedTypeSymbol arg,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    )
    {
        var method =
            semanticModel.GetSymbolInfo(invocation, cancellationToken).Symbol as IMethodSymbol;
        return method is not null
            && Guards.Any(guard => guard.MethodName == method.Name)
            && SymbolEqualityComparer.Default.Equals(method.ContainingType, arg)
            && IsValueArgument(invocation, parameter, semanticModel, cancellationToken);
    }

    static bool IsValueArgument(
        InvocationExpressionSyntax invocation,
        IParameterSymbol parameter,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    )
    {
        if (
            semanticModel.GetOperation(invocation, cancellationToken)
            is not IInvocationOperation operation
        )
            return false;

        var value = operation.Arguments.FirstOrDefault(candidate =>
            candidate.Parameter?.Ordinal == 0
        );
        return value is not null
            && SymbolEqualityComparer.Default.Equals(
                semanticModel
                    .GetSymbolInfo(
                        value.Syntax is ArgumentSyntax argument
                            ? argument.Expression
                            : value.Syntax,
                        cancellationToken
                    )
                    .Symbol,
                parameter
            );
    }

    static void AnalyzeNode(SyntaxNodeAnalysisContext context, INamedTypeSymbol arg)
    {
        if (context.Node is PropertyDeclarationSyntax property)
        {
            var target = PropertyGuardSupport.CreateTarget(
                property,
                context.SemanticModel,
                context.CancellationToken
            );
            if (
                target is null
                || !IsEligible(target)
                || HasExistingGuard(target, arg, context.SemanticModel, context.CancellationToken)
            )
                return;

            GuardSeverity.Report(
                context,
                Rule,
                property.Identifier.GetLocation(),
                target.PropertySymbol,
                target.PropertySymbol.Name
            );
            return;
        }

        var parameter = (ParameterSyntax)context.Node;
        var symbol = context.SemanticModel.GetDeclaredSymbol(parameter, context.CancellationToken);
        if (
            symbol is null
            || !IsEligible(parameter, symbol)
            || parameter.Parent?.Parent is not BaseMethodDeclarationSyntax { Body: { } body }
            || HasExistingGuard(body, symbol, arg, context.SemanticModel, context.CancellationToken)
        )
            return;

        GuardSeverity.Report(
            context,
            Rule,
            parameter.Identifier.GetLocation(),
            symbol.ContainingSymbol,
            symbol.Name
        );
    }

    public sealed class Guard
    {
        public Guard(string title, string methodName)
        {
            Title = title;
            MethodName = methodName;
        }

        public string Title { get; }

        public string MethodName { get; }
    }
}
