using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

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

            startContext.RegisterSyntaxNodeAction(
                context => AnalyzeParameter(context, guard),
                SyntaxKind.Parameter
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

    internal static bool IsEligible(ParameterSyntax parameter, IParameterSymbol parameterSymbol)
    {
        if (
            parameter.Parent?.Parent is not BaseMethodDeclarationSyntax declaration
            || declaration.Body is null
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

    internal static bool HasExistingGuard(
        BlockSyntax body,
        IParameterSymbol parameter,
        IMethodSymbol guard,
        SemanticModel semanticModel,
        CancellationToken cancellationToken
    )
    {
        foreach (var statement in body.Statements.OfType<ExpressionStatementSyntax>())
        {
            if (
                statement.Expression is not InvocationExpressionSyntax invocation
                || !SymbolEqualityComparer.Default.Equals(
                    semanticModel
                        .GetSymbolInfo(invocation, cancellationToken)
                        .Symbol?.OriginalDefinition,
                    guard.OriginalDefinition
                )
            )
            {
                continue;
            }

            var argument = invocation.ArgumentList.Arguments.FirstOrDefault();
            if (argument?.Expression is null)
            {
                continue;
            }

            if (
                SymbolEqualityComparer.Default.Equals(
                    semanticModel.GetSymbolInfo(argument.Expression, cancellationToken).Symbol,
                    parameter
                )
            )
            {
                return true;
            }
        }

        return false;
    }

    private static void AnalyzeParameter(SyntaxNodeAnalysisContext context, IMethodSymbol guard)
    {
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

    private static bool IsExplicitlyNullable(
        ParameterSyntax parameter,
        IParameterSymbol parameterSymbol
    )
    {
        return parameter.Type is NullableTypeSyntax
            || parameterSymbol.NullableAnnotation == NullableAnnotation.Annotated;
    }
}
