using System.Collections.Immutable;
using CheckAndThrow.Analyzers.Diagnostics.AddNotNullGuard;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Text;

namespace CheckAndThrow.Analyzers.Tests.Diagnostics.AddNotNullGuard;

public class AddNotNullGuardTests
{
    [Test]
    public async Task ReportsNonNullableReferencesAndUnconstrainedGenerics()
    {
        var diagnostics = await AnalyzeAsync(
            """
            class C
            {
                C(object value) { }
                void Reference(string value) { }
                void Generic<T>(T value) { }
            }
            """
        );

        await Assert
            .That(diagnostics.Select(diagnostic => diagnostic.Location.SourceSpan.Start))
            .Count()
            .IsEqualTo(3);
    }

    [Test]
    public async Task ExcludesNullableAndValueParameters()
    {
        var diagnostics = await AnalyzeAsync(
            """
            #nullable enable
            class C
            {
                void Nullable(string? value) { }
                void NullableGeneric<T>(T? value) { }
                void Number(int value) { }
                void Struct<T>(T value) where T : struct { }
                void Out(out string value) { value = ""; }
                int Expression(string value) => value.Length;
            }
            """
        );

        await Assert.That(diagnostics).IsEmpty();
    }

    [Test]
    public async Task DoesNotReportAnExistingTopLevelGuard()
    {
        var diagnostics = await AnalyzeAsync(
            """
            using CheckAndThrow;

            class C
            {
                void M(string value)
                {
                    Check.Arg.NotNull(value);
                }
            }
            """
        );

        await Assert.That(diagnostics).IsEmpty();
    }

    [Test]
    public async Task DoesNotReportAnExistingTopLevelGuardWithAssignment()
    {
        var diagnostics = await AnalyzeAsync(
            """
            using CheckAndThrow;

            class C
            {
                void M(string value)
                {
                    var valueCopy = Check.Arg.NotNull(value);
                }
            }
            """
        );

        await Assert.That(diagnostics).IsEmpty();
    }

    [Test]
    public async Task AddsOneFullyQualifiedGuard()
    {
        /*language=csharp*/
        const string source = """
            class C
            {
                void M(string value)
                {
                    System.Console.WriteLine(value);
                }
            }
            """;

        var (document, diagnostics) = await AnalyzeDocumentAsync(source);
        var actions = new List<CodeAction>();
        var provider = new AddNotNullGuardCodeFixProvider();
        var context = new CodeFixContext(
            document,
            diagnostics.Single(),
            (action, _) => actions.Add(action),
            CancellationToken.None
        );

        await provider.RegisterCodeFixesAsync(context);
        var operation = (ApplyChangesOperation)
            (await actions.Single().GetOperationsAsync(CancellationToken.None)).Single();
        var changed = operation.ChangedSolution.GetDocument(document.Id)!;
        var text = (await changed.GetTextAsync()).ToString();

        await Assert.That(text).Contains("global::CheckAndThrow.Check.Arg.NotNull(value);");
        await Assert.That((await AnalyzeAsync(text)).Length).IsEqualTo(0);
    }

    [Test]
    public async Task AddsOneFullyQualifiedGuardWithDirectMemberUsage()
    {
        /*language=csharp*/
        const string source = """
            class C
            {
                void M(string value)
                {
                    var length = value.Length;
                }
            }
            """;

        var (document, diagnostics) = await AnalyzeDocumentAsync(source);
        var actions = new List<CodeAction>();
        var provider = new AddNotNullGuardCodeFixProvider();
        var context = new CodeFixContext(
            document,
            diagnostics.Single(),
            (action, _) => actions.Add(action),
            CancellationToken.None
        );

        await provider.RegisterCodeFixesAsync(context);
        var operation = (ApplyChangesOperation)
            (await actions.Single().GetOperationsAsync(CancellationToken.None)).Single();
        var changed = operation.ChangedSolution.GetDocument(document.Id)!;
        var text = (await changed.GetTextAsync()).ToString();

        await Assert
            .That(text)
            .Contains(" var length = global::CheckAndThrow.Check.Arg.NotNull(value).Length;");
        await Assert.That((await AnalyzeAsync(text)).Length).IsEqualTo(0);
        var compilation = await changed.Project.GetCompilationAsync();
        await Assert
            .That(
                compilation!
                    .GetDiagnostics()
                    .Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
            )
            .IsEmpty();
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
        {
            project = project.AddMetadataReference(MetadataReference.CreateFromFile(path));
        }

        project = project.AddMetadataReference(
            MetadataReference.CreateFromFile(typeof(CheckAndThrow.Check).Assembly.Location)
        );
        workspace.TryApplyChanges(project.Solution);
        var document = workspace.AddDocument(project.Id, "Test.cs", SourceText.From(source));
        var compilation = await document.Project.GetCompilationAsync();
        var diagnostics = await compilation!
            .WithAnalyzers(ImmutableArray.Create<DiagnosticAnalyzer>(new AddNotNullGuardAnalyzer()))
            .GetAnalyzerDiagnosticsAsync();

        return (document, diagnostics);
    }
}
