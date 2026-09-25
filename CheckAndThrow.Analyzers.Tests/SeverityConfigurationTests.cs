using System.Collections.Immutable;
using CheckAndThrow.Analyzers.Diagnostics.AddNotNullGuard;
using CheckAndThrow.Analyzers.Diagnostics.AddNotNullGuards;
using CheckAndThrow.Analyzers.Diagnostics.AddRangeGuard;
using CheckAndThrow.Analyzers.Diagnostics.AddStringGuard;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

namespace CheckAndThrow.Analyzers.Tests;

public class SeverityConfigurationTests
{
    [Test]
    public async Task GlobalSettingsSeparatePublicAndNonPublicMethods()
    {
        var diagnostics = await AnalyzeAsync(
            "public class C { public void Visible(object visible) {} private void Hidden(object hidden) {} protected void Protected(object protectedValue) {} }",
            "checkandthrow_analyzers.public_severity = warning\ncheckandthrow_analyzers.non_public_severity = error",
            new AddNotNullGuardAnalyzer()
        );

        await Assert.That(diagnostics).Count().IsEqualTo(3);
        await Assert
            .That(diagnostics.Single(d => d.GetMessage().Contains("'visible'")).Severity)
            .IsEqualTo(DiagnosticSeverity.Warning);
        await Assert
            .That(diagnostics.Single(d => d.GetMessage().Contains("'hidden'")).Severity)
            .IsEqualTo(DiagnosticSeverity.Error);
        await Assert
            .That(diagnostics.Single(d => d.GetMessage().Contains("'protectedValue'")).Severity)
            .IsEqualTo(DiagnosticSeverity.Error);
    }

    [Test]
    public async Task GlobalNoneSuppressesOnlyItsVisibility()
    {
        var diagnostics = await AnalyzeAsync(
            "public class C { public void Visible(object visible) {} private void Hidden(object hidden) {} }",
            "checkandthrow_analyzers.public_severity = warning\ncheckandthrow_analyzers.non_public_severity = none",
            new AddNotNullGuardAnalyzer()
        );

        await Assert.That(diagnostics).Count().IsEqualTo(1);
        await Assert.That(diagnostics.Single().Severity).IsEqualTo(DiagnosticSeverity.Warning);
    }

    [Test]
    public async Task IndividualSettingsOverrideGlobalSettingsForEachVisibility()
    {
        const string settings = """
            checkandthrow_analyzers.public_severity = error
            checkandthrow_analyzers.non_public_severity = warning
            dotnet_diagnostic.CAT0001.public_severity = warning
            dotnet_diagnostic.CAT0001.non_public_severity = error
            dotnet_diagnostic.CAT0002.public_severity = suggestion
            dotnet_diagnostic.CAT0002.non_public_severity = silent
            dotnet_diagnostic.CAT0003.public_severity = silent
            dotnet_diagnostic.CAT0003.non_public_severity = none
            dotnet_diagnostic.CAT0004.public_severity = none
            dotnet_diagnostic.CAT0004.non_public_severity = suggestion
            """;
        DiagnosticAnalyzer[] analyzers =
        [
            new AddNotNullGuardAnalyzer(),
            new AddNotNullGuardsAnalyzer(),
            new AddRangeGuardAnalyzer(),
            new AddStringGuardAnalyzer(),
        ];

        var publicDiagnostics = await AnalyzeAsync(
            "public class C { public void M(string text, object item, int count) {} }",
            settings,
            analyzers
        );
        var privateDiagnostics = await AnalyzeAsync(
            "public class C { private void M(string text, object item, int count) {} }",
            settings,
            analyzers
        );

        await Assert
            .That(
                publicDiagnostics.Count(d =>
                    d.Id == "CAT0001" && d.Severity == DiagnosticSeverity.Warning
                )
            )
            .IsEqualTo(2);
        await Assert
            .That(publicDiagnostics.Single(d => d.Id == "CAT0002").Severity)
            .IsEqualTo(DiagnosticSeverity.Info);
        await Assert
            .That(publicDiagnostics.Single(d => d.Id == "CAT0003").Severity)
            .IsEqualTo(DiagnosticSeverity.Hidden);
        await Assert.That(publicDiagnostics).Count().IsEqualTo(4);
        await Assert
            .That(
                privateDiagnostics.Count(d =>
                    d.Id == "CAT0001" && d.Severity == DiagnosticSeverity.Error
                )
            )
            .IsEqualTo(2);
        await Assert
            .That(privateDiagnostics.Single(d => d.Id == "CAT0002").Severity)
            .IsEqualTo(DiagnosticSeverity.Hidden);
        await Assert
            .That(privateDiagnostics.Single(d => d.Id == "CAT0004").Severity)
            .IsEqualTo(DiagnosticSeverity.Info);
        await Assert.That(privateDiagnostics).Count().IsEqualTo(4);
    }

    [Test]
    public async Task ConstructorsPropertiesAndContainingTypesUseMemberVisibility()
    {
        const string source = """
            public class C
            {
                public C(object publicCtor) {}
                private C(object privateCtor, int unused) {}
                public string Name { get; private set; }
                private string Hidden { get; set; }
            }
            internal class Internal { public void M(object internalValue) {} }
            """;
        var diagnostics = await AnalyzeAsync(
            source,
            "checkandthrow_analyzers.public_severity = warning\ncheckandthrow_analyzers.non_public_severity = error",
            new AddNotNullGuardAnalyzer()
        );

        await Assert.That(diagnostics).Count().IsEqualTo(5);
        foreach (var name in new[] { "publicCtor", "Name" })
            await Assert
                .That(diagnostics.Single(d => d.GetMessage().Contains($"'{name}'")).Severity)
                .IsEqualTo(DiagnosticSeverity.Warning);
        foreach (var name in new[] { "privateCtor", "Hidden", "internalValue" })
            await Assert
                .That(diagnostics.Single(d => d.GetMessage().Contains($"'{name}'")).Severity)
                .IsEqualTo(DiagnosticSeverity.Error);
    }

    [Test]
    public async Task BulkGuardUsesConstructorVisibility()
    {
        const string source = """
            public class C
            {
                public C(object first, object second) {}
                private C(object left, object right, int unused) {}
            }
            """;
        var diagnostics = await AnalyzeAsync(
            source,
            "checkandthrow_analyzers.public_severity = warning\ncheckandthrow_analyzers.non_public_severity = error",
            new AddNotNullGuardsAnalyzer()
        );

        await Assert.That(diagnostics).Count().IsEqualTo(2);
        await Assert
            .That(
                diagnostics
                    .Single(d =>
                        source
                            .Substring(d.Location.SourceSpan.Start, d.Location.SourceSpan.Length)
                            .Contains("first")
                    )
                    .Severity
            )
            .IsEqualTo(DiagnosticSeverity.Warning);
        await Assert
            .That(
                diagnostics
                    .Single(d =>
                        source
                            .Substring(d.Location.SourceSpan.Start, d.Location.SourceSpan.Length)
                            .Contains("left")
                    )
                    .Severity
            )
            .IsEqualTo(DiagnosticSeverity.Error);
    }

    [Test]
    public async Task InvalidSettingsFallBackAndAbsentSettingsKeepHiddenDefault()
    {
        var source = "public class C { public void M(object value) {} }";
        var fallback = await AnalyzeAsync(
            source,
            "checkandthrow_analyzers.public_severity = warning\ndotnet_diagnostic.CAT0001.public_severity = invalid",
            new AddNotNullGuardAnalyzer()
        );
        var unchanged = await AnalyzeAsync(source, "", new AddNotNullGuardAnalyzer());

        await Assert.That(fallback.Single().Severity).IsEqualTo(DiagnosticSeverity.Warning);
        await Assert.That(unchanged.Single().Severity).IsEqualTo(DiagnosticSeverity.Hidden);
    }

    [Test]
    public async Task StandardDiagnosticSuppressionStillWins()
    {
        var diagnostics = await AnalyzeAsync(
            "public class C { public void M(object value) {} }",
            "checkandthrow_analyzers.public_severity = error\ndotnet_diagnostic.CAT0001.severity = none",
            new AddNotNullGuardAnalyzer()
        );

        await Assert.That(diagnostics).IsEmpty();
    }

    static async Task<ImmutableArray<Diagnostic>> AnalyzeAsync(
        string source,
        string settings,
        params DiagnosticAnalyzer[] analyzers
    )
    {
        using var workspace = new AdhocWorkspace();
        var project = workspace
            .AddProject("Test", LanguageNames.CSharp)
            .WithParseOptions(new CSharpParseOptions(LanguageVersion.Preview))
            .WithCompilationOptions(
                new CSharpCompilationOptions(
                    OutputKind.DynamicallyLinkedLibrary,
                    nullableContextOptions: NullableContextOptions.Enable
                )
            );

        foreach (
            var path in ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!).Split(
                Path.PathSeparator
            )
        )
            project = project.AddMetadataReference(MetadataReference.CreateFromFile(path));
        project = project.AddMetadataReference(
            MetadataReference.CreateFromFile(typeof(CheckAndThrow.Check).Assembly.Location)
        );

        var directory = Path.Combine(Path.GetTempPath(), "CheckAndThrowSeverityTests");
        project = project
            .AddAnalyzerConfigDocument(
                ".editorconfig",
                SourceText.From("root = true\n\n[*.cs]\n" + settings),
                filePath: Path.Combine(directory, ".editorconfig")
            )
            .Project;
        var document = project.AddDocument(
            "Test.cs",
            SourceText.From(source),
            filePath: Path.Combine(directory, "Test.cs")
        );
        var compilation = await document.Project.GetCompilationAsync();
        return await compilation!
            .WithAnalyzers(ImmutableArray.Create(analyzers), document.Project.AnalyzerOptions)
            .GetAnalyzerDiagnosticsAsync();
    }
}
