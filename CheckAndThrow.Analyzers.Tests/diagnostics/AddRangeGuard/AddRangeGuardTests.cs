using System.Collections.Immutable;
using CheckAndThrow.Analyzers.Diagnostics.AddRangeGuard;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

namespace CheckAndThrow.Analyzers.Tests.Diagnostics.AddRangeGuard;

public class AddRangeGuardTests
{
    [Test]
    public async Task ReportsNumericParametersWithHiddenDiagnostic()
    {
        var diagnostics = await AnalyzeAsync("class C { void M(int count, double ratio) { } }");

        await Assert.That(diagnostics).Count().IsEqualTo(2);
        await Assert
            .That(diagnostics.Select(diagnostic => diagnostic.Id).Distinct())
            .IsEquivalentTo([AddRangeGuardAnalyzer.DiagnosticId]);
        await Assert
            .That(diagnostics.Select(diagnostic => diagnostic.Severity).Distinct())
            .IsEquivalentTo([DiagnosticSeverity.Hidden]);
    }

    [Test]
    public async Task ExcludesNonNumericAndNullableParameters()
    {
        var diagnostics = await AnalyzeAsync(
            "#nullable enable class C { void M(string name, bool enabled, int? count, object value, T generic<T>(T value) => value; }"
        );

        await Assert.That(diagnostics).IsEmpty();
    }

    [Test]
    public async Task ExistingRangeGuardSuppressesDiagnostic()
    {
        var diagnostics = await AnalyzeAsync(
            "using CheckAndThrow; class C { void M(int count) { var valid = Check.Arg.Positive(count); } }"
        );

        await Assert.That(diagnostics).IsEmpty();
    }

    [Test]
    public async Task UsesReturnedGuardValueForDirectMemberUsage()
    {
        var (document, diagnostics) = await AnalyzeDocumentAsync(
            "class C { int M(int count) { var text = count.ToString(); return text.Length; } }"
        );

        var changed = await AddRangeGuardCodeFixProvider.AddGuardAsync(
            document,
            diagnostics.Single(),
            AddRangeGuardAnalyzer.Guards.Single(guard => guard.MethodName == "Positive"),
            CancellationToken.None
        );
        var text = (await changed.GetTextAsync()).ToString();
        var compilation = await changed.Project.GetCompilationAsync();

        await Assert
            .That(text)
            .Contains("global::CheckAndThrow.Check.Arg.Positive(count).ToString()");
        await Assert
            .That(
                compilation!
                    .GetDiagnostics()
                    .Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
            )
            .IsEmpty();
        await Assert.That(await AnalyzeAsync(text)).IsEmpty();
    }

    [Test]
    public async Task RegistersRangeGuardActionGroup()
    {
        var (document, diagnostics) = await AnalyzeDocumentAsync(
            "class C { void M(int count) { } }"
        );
        var actions = new List<CodeAction>();
        var context = new CodeFixContext(
            document,
            diagnostics.Single(),
            (action, _) => actions.Add(action),
            CancellationToken.None
        );

        await new AddRangeGuardCodeFixProvider().RegisterCodeFixesAsync(context);

        await Assert.That(actions).Count().IsEqualTo(1);
        await Assert.That(actions.Single().Title).IsEqualTo("Add range guard clause");
    }

    static async Task<ImmutableArray<Diagnostic>> AnalyzeAsync(string source)
    {
        var (_, diagnostics) = await AnalyzeDocumentAsync(source);
        return diagnostics;
    }

    static async Task<(
        Document Document,
        ImmutableArray<Diagnostic> Diagnostics
    )> AnalyzeDocumentAsync(string source)
    {
        var workspace = new AdhocWorkspace();
        var project = workspace
            .AddProject("Test", LanguageNames.CSharp)
            .WithParseOptions(new CSharpParseOptions(LanguageVersion.Preview))
            .WithCompilationOptions(
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
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
        workspace.TryApplyChanges(project.Solution);
        var document = workspace.AddDocument(project.Id, "Test.cs", SourceText.From(source));
        var diagnostics = await (await document.Project.GetCompilationAsync())!
            .WithAnalyzers(ImmutableArray.Create<DiagnosticAnalyzer>(new AddRangeGuardAnalyzer()))
            .GetAnalyzerDiagnosticsAsync();
        return (document, diagnostics);
    }
}
