using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CheckAndThrow.Analyzers.Diagnostics;

internal static class GuardSeverity
{
    internal static void Report(
        SyntaxNodeAnalysisContext context,
        DiagnosticDescriptor rule,
        Location location,
        ISymbol? member,
        params object[] messageArgs
    )
    {
        var visibility = IsPublic(member) ? "public" : "non_public";
        var options = context.Options.AnalyzerConfigOptionsProvider.GetOptions(
            context.Node.SyntaxTree
        );
        if (
            !TryGetSeverity(
                options,
                $"dotnet_diagnostic.{rule.Id}.{visibility}_severity",
                out var severity
            )
            && !TryGetSeverity(
                options,
                $"checkandthrow_analyzers.{visibility}_severity",
                out severity
            )
        )
        {
            context.ReportDiagnostic(Diagnostic.Create(rule, location, messageArgs));
            return;
        }

        if (severity is { } configuredSeverity)
            context.ReportDiagnostic(
                Diagnostic.Create(rule, location, configuredSeverity, null, null, messageArgs)
            );
    }

    static bool IsPublic(ISymbol? member)
    {
        if (member?.DeclaredAccessibility != Accessibility.Public)
            return false;

        for (var type = member.ContainingType; type is not null; type = type.ContainingType)
            if (type.DeclaredAccessibility != Accessibility.Public)
                return false;

        return true;
    }

    static bool TryGetSeverity(
        AnalyzerConfigOptions options,
        string key,
        out DiagnosticSeverity? severity
    )
    {
        severity = null;
        if (!options.TryGetValue(key, out var value))
            return false;

        switch (value.Trim().ToLowerInvariant())
        {
            case "none":
                return true;
            case "silent":
            case "hint":
                severity = DiagnosticSeverity.Hidden;
                return true;
            case "suggestion":
                severity = DiagnosticSeverity.Info;
                return true;
            case "warning":
                severity = DiagnosticSeverity.Warning;
                return true;
            case "error":
                severity = DiagnosticSeverity.Error;
                return true;
            default:
                return false;
        }
    }
}
