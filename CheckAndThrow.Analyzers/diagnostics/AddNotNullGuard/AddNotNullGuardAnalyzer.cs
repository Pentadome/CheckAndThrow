using System.Collections.Immutable;
using CheckAndThrow.Analyzers.Diagnostics.PropertyGuard;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace CheckAndThrow.Analyzers.Diagnostics.AddNotNullGuard;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class AddNotNullGuardAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "CAT0001";

    internal static readonly DiagnosticDescriptor Rule = new(
        DiagnosticId,
        "Add Check.Arg.NotNull guard",
        "Add Check.Arg.NotNull guard for '{0}'",
        "Usage",
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
            var guard = FindGuard(startContext.Compilation);
            if (guard is null)
            {
                return;
            }

            var argsGuardType = FindArgsGuardType(startContext.Compilation);
            startContext.RegisterSyntaxNodeAction(
                context => AnalyzeNode(context, guard, argsGuardType),
                SyntaxKind.Parameter,
                SyntaxKind.PropertyDeclaration
            );
        });
    }

    internal static IMethodSymbol? FindGuard(Compilation compilation)
    {
        var check = compilation.GetTypeByMetadataName("CheckAndThrow.Check");
        var arg = check?.GetTypeMembers("Arg").SingleOrDefault();

        return arg
            ?.GetMembers("NotNull")
            .OfType<IMethodSymbol>()
            .SingleOrDefault(method => method.Arity == 1 && method.Parameters.Length == 2);
    }

    internal static bool IsEligible(
        ParameterSyntax parameter,
        IParameterSymbol parameterSymbol,
        bool allowExpressionBody = false
    )
    {
        if (
            parameter.Parent?.Parent is not BaseMethodDeclarationSyntax declaration
            || (declaration.Body is null && !allowExpressionBody)
            || parameterSymbol.RefKind == RefKind.Out
            || IsExplicitlyNullable(parameter, parameterSymbol)
        )
        {
            return false;
        }

        return parameterSymbol.Type switch
        {
            ITypeParameterSymbol typeParameter => !typeParameter.HasValueTypeConstraint,
            _ => parameterSymbol.Type.IsReferenceType,
        };
    }

    internal static bool IsEligible(PropertyGuardTarget target) =>
        PropertyGuardSupport.IsEligible(target);

    internal static INamedTypeSymbol? FindArgsGuardType(Compilation compilation)
    {
        return compilation
            .GetTypeByMetadataName("CheckAndThrow.Check")
            ?.GetTypeMembers("Args")
            .SingleOrDefault();
    }

    internal static bool HasExistingGuard(
        BlockSyntax body,
        IParameterSymbol parameter,
        IMethodSymbol guard,
        INamedTypeSymbol? argsGuardType,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    )
    {
        foreach (var statement in body.Statements)
        {
            var invocation = GetTopLevelGuardInvocation(statement);
            if (
                invocation is null
                && statement is LocalDeclarationStatementSyntax local
                && local.Declaration.Variables.Count == 1
                && local.Declaration.Variables[0].Initializer?.Value
                    is MemberAccessExpressionSyntax
                    {
                        Expression: InvocationExpressionSyntax inlineGuard
                    }
            )
            {
                invocation = inlineGuard;
            }

            if (invocation is null)
            {
                continue;
            }

            var method =
                semanticModel.GetSymbolInfo(invocation, cancellationToken).Symbol as IMethodSymbol;
            if (
                method is not null
                && (
                    SymbolEqualityComparer.Default.Equals(
                        method.OriginalDefinition,
                        guard.OriginalDefinition
                    )
                    || method.Name is "NotNullOrEmpty" or "NotNullOrWhiteSpace"
                        && method.Parameters[0].Type.SpecialType == SpecialType.System_String
                        && SymbolEqualityComparer.Default.Equals(
                            method.ContainingType,
                            guard.ContainingType
                        )
                )
                && HasValueArgument(invocation, parameter, semanticModel, cancellationToken)
            )
            {
                return true;
            }

            if (
                argsGuardType is not null
                && method?.Name == "NotNull"
                && SymbolEqualityComparer.Default.Equals(method.ContainingType, argsGuardType)
                && invocation.ArgumentList.Arguments.Any(argument =>
                    SymbolEqualityComparer.Default.Equals(
                        semanticModel.GetSymbolInfo(argument.Expression, cancellationToken).Symbol,
                        parameter
                    )
                )
            )
            {
                return true;
            }
        }

        return false;
    }

    static bool HasValueArgument(
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
        {
            return false;
        }

        var value = operation.Arguments.FirstOrDefault(argument =>
            argument.Parameter?.Ordinal == 0
        );
        return value is not null
            && SymbolEqualityComparer.Default.Equals(
                semanticModel.GetSymbolInfo(value.Value.Syntax, cancellationToken).Symbol,
                parameter
            );
    }

    internal static InvocationExpressionSyntax? GetTopLevelGuardInvocation(
        StatementSyntax statement
    )
    {
        if (
            statement is ExpressionStatementSyntax
            {
                Expression: InvocationExpressionSyntax invocation
            }
        )
        {
            return invocation;
        }

        if (
            statement is ExpressionStatementSyntax
            {
                Expression: AssignmentExpressionSyntax
                {
                    Right: InvocationExpressionSyntax assignmentInvocation
                },
            }
        )
        {
            return assignmentInvocation;
        }

        return
            statement is LocalDeclarationStatementSyntax local
            && local.Declaration.Variables.Count == 1
            ? local.Declaration.Variables[0].Initializer?.Value as InvocationExpressionSyntax
            : null;
    }

    internal static bool HasExistingGuard(
        PropertyGuardTarget target,
        IMethodSymbol guard,
        INamedTypeSymbol? argsGuardType,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    ) =>
        HasExistingGuard(
            PropertyGuardSupport.GetTopLevelGuardInvocations(target.Setter),
            target.ValueParameter,
            guard,
            argsGuardType,
            semanticModel,
            cancellationToken
        );

    static bool HasExistingGuard(
        IEnumerable<InvocationExpressionSyntax> invocations,
        IParameterSymbol parameter,
        IMethodSymbol guard,
        INamedTypeSymbol? argsGuardType,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    )
    {
        foreach (var invocation in invocations)
        {
            var method =
                semanticModel.GetSymbolInfo(invocation, cancellationToken).Symbol as IMethodSymbol;
            if (
                method is not null
                && (
                    SymbolEqualityComparer.Default.Equals(
                        method.OriginalDefinition,
                        guard.OriginalDefinition
                    )
                    || method.Name is "NotNullOrEmpty" or "NotNullOrWhiteSpace"
                        && method.Parameters[0].Type.SpecialType == SpecialType.System_String
                        && SymbolEqualityComparer.Default.Equals(
                            method.ContainingType,
                            guard.ContainingType
                        )
                )
                && HasValueArgument(invocation, parameter, semanticModel, cancellationToken)
            )
            {
                return true;
            }

            if (
                argsGuardType is not null
                && method?.Name == "NotNull"
                && SymbolEqualityComparer.Default.Equals(method.ContainingType, argsGuardType)
                && invocation.ArgumentList.Arguments.Any(argument =>
                    SymbolEqualityComparer.Default.Equals(
                        semanticModel.GetSymbolInfo(argument.Expression, cancellationToken).Symbol,
                        parameter
                    )
                )
            )
            {
                return true;
            }
        }

        return false;
    }

    static void AnalyzeNode(
        SyntaxNodeAnalysisContext context,
        IMethodSymbol guard,
        INamedTypeSymbol? argsGuardType
    )
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
                || HasExistingGuard(
                    target,
                    guard,
                    argsGuardType,
                    context.SemanticModel,
                    context.CancellationToken
                )
            )
            {
                return;
            }

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
        var parameterSymbol = context.SemanticModel.GetDeclaredSymbol(
            parameter,
            context.CancellationToken
        );
        if (
            parameterSymbol is null
            || !IsEligible(parameter, parameterSymbol)
            || parameter.Parent?.Parent is not BaseMethodDeclarationSyntax { Body: { } body }
            || HasExistingGuard(
                body,
                parameterSymbol,
                guard,
                argsGuardType,
                context.SemanticModel,
                context.CancellationToken
            )
        )
        {
            return;
        }

        context.ReportDiagnostic(
            Diagnostic.Create(Rule, parameter.Identifier.GetLocation(), parameterSymbol.Name)
        );
    }

    static bool IsExplicitlyNullable(ParameterSyntax parameter, IParameterSymbol parameterSymbol)
    {
        return parameter.Type is NullableTypeSyntax
            || parameterSymbol.NullableAnnotation == NullableAnnotation.Annotated;
    }
}
