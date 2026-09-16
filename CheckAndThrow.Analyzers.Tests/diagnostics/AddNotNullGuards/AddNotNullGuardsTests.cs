using System.Collections.Immutable;
using CheckAndThrow.Analyzers.Diagnostics.AddNotNullGuard;
using CheckAndThrow.Analyzers.Diagnostics.AddNotNullGuards;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Text;

namespace CheckAndThrow.Analyzers.Tests.Diagnostics.AddNotNullGuards;

public class AddNotNullGuardsTests
{
    [Test]
    public async Task ReportsOnlyTwoToSixteenEligibleParameters()
    {
        var two = await AnalyzeAsync(MethodWithParameters(2));
        var one = await AnalyzeAsync(MethodWithParameters(1));
        var eight = await AnalyzeAsync(MethodWithParameters(8));
        var nine = await AnalyzeAsync(MethodWithParameters(9));
        var sixteen = await AnalyzeAsync(MethodWithParameters(16));
        var seventeen = await AnalyzeAsync(MethodWithParameters(17));

        await Assert.That(two.Single().Id).IsEqualTo(AddNotNullGuardsAnalyzer.DiagnosticId);
        await Assert.That(eight.Single().Id).IsEqualTo(AddNotNullGuardsAnalyzer.DiagnosticId);
        await Assert.That(nine.Single().Id).IsEqualTo(AddNotNullGuardsAnalyzer.DiagnosticId);
        await Assert.That(sixteen.Single().Id).IsEqualTo(AddNotNullGuardsAnalyzer.DiagnosticId);
        await Assert.That(one).IsEmpty();
        await Assert.That(seventeen).IsEmpty();
    }

    [Test]
    public async Task ConvertsAssignmentsAndLocalDeclarationsUsingTheTupleResult()
    {
        /*language=csharp*/
        const string source = """
            using CheckAndThrow;

            class C
            {
                private object _first = null!;

                void M(object first, object second)
                {
                    _first = Check.Arg.NotNull(first);
                    var copy = Check.Arg.NotNull(second);
                }
            }
            """;

        var text = await ApplyFixAsync(source);

        await Assert
            .That(text)
            .Contains(
                "(_first, var copy) = global::CheckAndThrow.Check.Args.NotNull(first, second);"
            );
        await Assert.That((await AnalyzeAllAsync(text)).Length).IsEqualTo(0);
    }

    [Test]
    public async Task HandlesLambdaConstructorsCorrectlyWithTuple()
    {
        /*language=csharp*/
        const string source = """
            using CheckAndThrow;

            class C
            {
                private readonly object _first;
                private readonly object _second;

                C(object first, object second) => (_first, _second) = (first, second); 
            }
            """;

        var text = await ApplyFixAsync(source);

        await Assert
            .That(text)
            .Contains(
                "C(object first, object second) => (_first, _second) = global::CheckAndThrow.Check.Args.NotNull(first, second);"
            );
        await Assert.That((await AnalyzeAllAsync(text)).Length).IsEqualTo(0);
    }

    [Test]
    public async Task HandlesLambdaConstructorsCorrectlyWithTupleWithPreExistingCheck()
    {
        /*language=csharp*/
        const string source = """
            using CheckAndThrow;

            class C
            {
                private readonly object _first;
                private readonly object _second;

                C(object first, object second) => (_first, _second) = (first, global::CheckAndThrow.Check.Arg.NotNull(second)); 
            }
            """;

        var text = await ApplyFixAsync(source);

        await Assert
            .That(text)
            .Contains(
                "C(object first, object second) => (_first, _second) = global::CheckAndThrow.Check.Args.NotNull(first, second);"
            );
        await Assert.That((await AnalyzeAllAsync(text)).Length).IsEqualTo(0);
    }

    [Test]
    public async Task ReplacesPartialExistingBulkGuard()
    {
        /*language=csharp*/
        const string source = """
            using CheckAndThrow;

            class C
            {
                void M(object first, object second, object third)
                {
                    Check.Args.NotNull(first, second);
                }
            }
            """;

        var text = await ApplyFixAsync(source);

        await Assert
            .That(text)
            .Contains("global::CheckAndThrow.Check.Args.NotNull(first, second, third);");
        await Assert.That(text).DoesNotContain("Check.Args.NotNull(first, second);");
    }

    [Test]
    public async Task KeepsTupleLocalWhenGuardAssignmentsAreInterleaved()
    {
        /*language=csharp*/
        const string source = """
            using CheckAndThrow;
            class C
            {
                object _first = null!;
                void M(object first, object second)
                {
                    _first = Check.Arg.NotNull(first);
                    System.Console.WriteLine(_first);
                    var copy = Check.Arg.NotNull(second);
                }
            }
            """;
        var text = await ApplyFixAsync(source);
        await Assert
            .That(text)
            .Contains(
                "var checkAndThrowArgs = global::CheckAndThrow.Check.Args.NotNull(first, second);"
            );
        await Assert.That(text).Contains("var copy = checkAndThrowArgs.arg2;");
        await Assert.That(await AnalyzeAllAsync(text)).IsEmpty();
    }

    [Test]
    public async Task DoesNotRewriteReorderedExpressionBodiedConstructorTuple()
    {
        /*language=csharp*/
        const string source = """
            class C
            {
                readonly object _first, _second;
                C(object first, object second) => (_first, _second) = (second, first);
            }
            """;
        await Assert.That(await AnalyzeAsync(source)).IsEmpty();
    }

    static string MethodWithParameters(int count)
    {
        return "class C { void M("
            + string.Join(", ", Enumerable.Range(1, count).Select(index => $"object value{index}"))
            + ") { } }";
    }

    static async Task<string> ApplyFixAsync(string source)
    {
        var (document, diagnostics) = await AnalyzeDocumentAsync(
            source,
            new AddNotNullGuardsAnalyzer()
        );
        var actions = new List<CodeAction>();
        var provider = new AddNotNullGuardsCodeFixProvider();
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
        var compilation = await changed.Project.GetCompilationAsync();
        await Assert
            .That(
                compilation!
                    .GetDiagnostics()
                    .Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
            )
            .IsEmpty();
        return (await changed.GetTextAsync()).ToString();
    }

    static async Task<ImmutableArray<Diagnostic>> AnalyzeAsync(string source)
    {
        var (_, diagnostics) = await AnalyzeDocumentAsync(source, new AddNotNullGuardsAnalyzer());
        return diagnostics;
    }

    static async Task<ImmutableArray<Diagnostic>> AnalyzeAllAsync(string source)
    {
        var (_, diagnostics) = await AnalyzeDocumentAsync(
            source,
            new AddNotNullGuardsAnalyzer(),
            new AddNotNullGuardAnalyzer()
        );
        return diagnostics;
    }

    static async Task<(
        Document Document,
        ImmutableArray<Diagnostic> Diagnostics
    )> AnalyzeDocumentAsync(string source, params DiagnosticAnalyzer[] analyzers)
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
            .WithAnalyzers(ImmutableArray.Create(analyzers))
            .GetAnalyzerDiagnosticsAsync();

        return (document, diagnostics);
    }
}
