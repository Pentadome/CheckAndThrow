using CheckAndThrow.Analyzers.Diagnostics.AddNotNullGuard;
using CheckAndThrow.Analyzers.Diagnostics.AddNotNullGuards;
using CheckAndThrow.Analyzers.Diagnostics.AddRangeGuard;
using CheckAndThrow.Analyzers.Diagnostics.AddStringGuard;

namespace CheckAndThrow.Analyzers.Tests;

public class DiagnosticCategoryTests
{
    [Test]
    public async Task GuardDiagnosticsAreCodeStyleDiagnostics()
    {
        await Assert
            .That(new AddNotNullGuardAnalyzer().SupportedDiagnostics.Single().Category)
            .IsEqualTo("Style");
        await Assert
            .That(new AddNotNullGuardsAnalyzer().SupportedDiagnostics.Single().Category)
            .IsEqualTo("Style");
        await Assert
            .That(new AddRangeGuardAnalyzer().SupportedDiagnostics.Single().Category)
            .IsEqualTo("Style");
        await Assert
            .That(new AddStringGuardAnalyzer().SupportedDiagnostics.Single().Category)
            .IsEqualTo("Style");
    }
}
