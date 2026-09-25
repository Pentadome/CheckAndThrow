using System.Collections.Immutable;
using CheckAndThrow.Analyzers.Diagnostics.PropertyGuard;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CheckAndThrow.Analyzers.Diagnostics.AddRangeGuard;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class AddRangeGuardAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "CAT0003";

    internal static readonly DiagnosticDescriptor Rule = new(
        DiagnosticId,
        "Add range guard clause",
        "Add range guard clause for '{0}'",
        "Style",
        DiagnosticSeverity.Hidden,
        isEnabledByDefault: true
    );

    public static readonly ImmutableArray<Guard> Guards = ImmutableArray.Create(
        new Guard("Guard against negative", "ZeroOrPositive"),
        new Guard("Guard against negative and zero", "Positive"),
        new Guard("Guard against positive", "ZeroOrNegative"),
        new Guard("Guard against positive and zero", "Negative"),
        new Guard(
            "Guard against negative and zero (allow positive infinity)",
            "PositiveOrInfinity"
        ),
        new Guard(
            "Guard against positive and zero (allow negative infinity)",
            "NegativeOrNegativeInfinity"
        )
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
            if (arg is null)
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

    internal static bool IsEligible(ParameterSyntax parameter, IParameterSymbol symbol)
    {
        if (
            parameter.Parent?.Parent is not BaseMethodDeclarationSyntax declaration
            || declaration is OperatorDeclarationSyntax
            || (declaration.Body is null && declaration.ExpressionBody is null)
            || symbol.RefKind == RefKind.Out
            || symbol.Type.TypeKind is TypeKind.Pointer or TypeKind.Enum
            || symbol.Type.NullableAnnotation == NullableAnnotation.Annotated
        )
            return false;

        return IsNumber(symbol.Type);
    }

    internal static bool IsEligible(PropertyGuardTarget target) =>
        target.PropertySymbol.Type.TypeKind is not (TypeKind.Pointer or TypeKind.Enum)
        && target.PropertySymbol.Type.NullableAnnotation != NullableAnnotation.Annotated
        && IsNumber(target.PropertySymbol.Type);

    internal static bool IsNumber(ITypeSymbol type)
    {
        if (
            type.SpecialType
            is SpecialType.System_SByte
                or SpecialType.System_Byte
                or SpecialType.System_Int16
                or SpecialType.System_UInt16
                or SpecialType.System_Int32
                or SpecialType.System_UInt32
                or SpecialType.System_Int64
                or SpecialType.System_UInt64
                or SpecialType.System_Single
                or SpecialType.System_Double
                or SpecialType.System_Decimal
        )
            return true;

        if (type is ITypeParameterSymbol parameter)
            return parameter.ConstraintTypes.Any(IsNumberInterface);

        return ImplementsNumberBase(type);
    }

    static bool ImplementsNumberBase(ITypeSymbol type) =>
        type.AllInterfaces.Any(IsNumberInterface) || IsNumberInterface(type);

    static bool IsNumberInterface(ITypeSymbol type) =>
        type is INamedTypeSymbol named
        && named.OriginalDefinition.ToDisplayString() == "System.Numerics.INumberBase<TSelf>";

    internal static bool HasApplicableGuard(
        INamedTypeSymbol arg,
        IParameterSymbol parameter,
        SemanticModel semanticModel,
        int position,
        CancellationToken cancellationToken
    ) =>
        Guards.Any(guard =>
            BindGuard(arg, guard, parameter, semanticModel, position, cancellationToken) is not null
        );

    internal static IMethodSymbol? BindGuard(
        INamedTypeSymbol arg,
        Guard guard,
        IParameterSymbol parameter,
        SemanticModel semanticModel,
        int position,
        CancellationToken cancellationToken
    )
    {
        // The speculative expression has no parameter declaration context, so bind
        // availability from the API surface here; the code fix reuses the exact call.
        return arg.GetMembers(guard.MethodName)
            .OfType<IMethodSymbol>()
            .FirstOrDefault(method => method.Parameters.Length >= 1);
    }

    internal static bool HasExistingGuard(
        BaseMethodDeclarationSyntax declaration,
        IParameterSymbol parameter,
        INamedTypeSymbol arg,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<InvocationExpressionSyntax> invocations = declaration.Body is { } body
            ? body.DescendantNodes().OfType<InvocationExpressionSyntax>()
            : declaration
                .ExpressionBody?.Expression.DescendantNodesAndSelf()
                .OfType<InvocationExpressionSyntax>()
                ?? Enumerable.Empty<InvocationExpressionSyntax>();

        return HasExistingGuard(invocations, parameter, arg, semanticModel, cancellationToken);
    }

    internal static bool HasExistingGuard(
        PropertyGuardTarget target,
        INamedTypeSymbol arg,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<InvocationExpressionSyntax> invocations = target.Setter.Body is { } body
            ? body.DescendantNodes().OfType<InvocationExpressionSyntax>()
            : target
                .Setter.ExpressionBody?.Expression.DescendantNodesAndSelf()
                .OfType<InvocationExpressionSyntax>()
                ?? Enumerable.Empty<InvocationExpressionSyntax>();

        return HasExistingGuard(
            invocations,
            target.ValueParameter,
            arg,
            semanticModel,
            cancellationToken
        );
    }

    static bool HasExistingGuard(
        IEnumerable<InvocationExpressionSyntax> invocations,
        IParameterSymbol parameter,
        INamedTypeSymbol arg,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    )
    {
        foreach (var invocation in invocations)
        {
            var method =
                semanticModel.GetSymbolInfo(invocation, cancellationToken).Symbol as IMethodSymbol;
            if (
                method is null
                || !SymbolEqualityComparer.Default.Equals(method.ContainingType, arg)
                || !Guards.Any(guard => guard.MethodName == method.Name) && method.Name != "InRange"
            )
                continue;

            var valueArgument = invocation.ArgumentList.Arguments.FirstOrDefault(argument =>
                argument.NameColon?.Name.Identifier.ValueText == "value"
                || (
                    argument.NameColon is null
                    && invocation.ArgumentList.Arguments.IndexOf(argument) == 0
                )
            );
            if (
                valueArgument?.Expression is not null
                && SymbolEqualityComparer.Default.Equals(
                    semanticModel.GetSymbolInfo(valueArgument.Expression, cancellationToken).Symbol,
                    parameter
                )
            )
                return true;
        }

        return false;
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
                || !HasApplicableGuard(
                    arg,
                    target.ValueParameter,
                    context.SemanticModel,
                    property.SpanStart,
                    context.CancellationToken
                )
            )
                return;

            context.ReportDiagnostic(
                Diagnostic.Create(
                    Rule,
                    property.Identifier.GetLocation(),
                    target.PropertySymbol.Name
                )
            );
            return;
        }

        var parameter = (ParameterSyntax)context.Node;
        var symbol = context.SemanticModel.GetDeclaredSymbol(parameter, context.CancellationToken);
        if (
            symbol is null
            || !IsEligible(parameter, symbol)
            || parameter.Parent?.Parent is not BaseMethodDeclarationSyntax declaration
            || HasExistingGuard(
                declaration,
                symbol,
                arg,
                context.SemanticModel,
                context.CancellationToken
            )
            || !HasApplicableGuard(
                arg,
                symbol,
                context.SemanticModel,
                parameter.SpanStart,
                context.CancellationToken
            )
        )
            return;

        context.ReportDiagnostic(
            Diagnostic.Create(Rule, parameter.Identifier.GetLocation(), symbol.Name)
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
