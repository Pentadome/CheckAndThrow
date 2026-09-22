using System.Collections.Immutable;
using CheckAndThrow.Analyzers.Diagnostics.AddStringGuard;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

namespace CheckAndThrow.Analyzers.Tests.Diagnostics.AddStringGuard;

public class AddStringGuardTests
{
    [Test]
    public async Task ReportsOnlyEligibleStringParameters()
    {
        var diagnostics = await AnalyzeAsync(
            """
            #nullable enable
            class C
            {
                C(string value) { }
                void String(string value) { }
                void Nullable(string? value) { }
                void Object(object value) { }
                void Out(out string value) { value = ""; }
                int Expression(string value) => value.Length;
            }
            """
        );

        await Assert.That(diagnostics).Count().IsEqualTo(2);
        await Assert
            .That(diagnostics.Select(diagnostic => diagnostic.Id).Distinct())
            .IsEquivalentTo([AddStringGuardAnalyzer.DiagnosticId]);
    }

    [Test]
    public async Task RegistersStringGuardActionGroup()
    {
        var (document, diagnostics) = await AnalyzeDocumentAsync(
            "class C { void M(string value) { } }"
        );
        var actions = new List<CodeAction>();
        var context = new CodeFixContext(
            document,
            diagnostics.Single(),
            (action, _) => actions.Add(action),
            CancellationToken.None
        );

        await new AddStringGuardCodeFixProvider().RegisterCodeFixesAsync(context);

        await Assert.That(actions).Count().IsEqualTo(1);
        await Assert.That(actions.Single().Title).IsEqualTo("Add string guard clause");
    }

    [Test]
    [Arguments("NotNullOrEmpty")]
    [Arguments("NotNullOrWhiteSpace")]
    public async Task AddsEachGuardUsingTheFirstDirectMemberUse(string methodName)
    {
        var (document, diagnostics) = await AnalyzeDocumentAsync(
            "class C { int M(string value) { var length = value.Length; return length; } }"
        );
        var guard = AddStringGuardAnalyzer.Guards.Single(item => item.MethodName == methodName);

        var changed = await AddStringGuardCodeFixProvider.AddGuardAsync(
            document,
            diagnostics.Single(),
            guard,
            CancellationToken.None
        );
        var text = (await changed.GetTextAsync()).ToString();

        await Assert
            .That(text)
            .Contains($"global::CheckAndThrow.Check.Arg.{methodName}(value).Length");
        await Assert.That(await CompilerErrorsAsync(changed)).IsEmpty();
        await Assert.That(await AnalyzeAsync(text)).IsEmpty();
    }

    [Test]
    public async Task AddsStatementForEscapedIdentifier()
    {
        var (document, diagnostics) = await AnalyzeDocumentAsync(
            "class C { void M(string @event) { } }"
        );
        var guard = AddStringGuardAnalyzer.Guards.Single(item =>
            item.MethodName == "NotNullOrEmpty"
        );

        var changed = await AddStringGuardCodeFixProvider.AddGuardAsync(
            document,
            diagnostics.Single(),
            guard,
            CancellationToken.None
        );
        var text = (await changed.GetTextAsync()).ToString();

        await Assert.That(text).Contains("global::CheckAndThrow.Check.Arg.NotNullOrEmpty(@event);");
        await Assert.That(await CompilerErrorsAsync(changed)).IsEmpty();
    }

    [Test]
    [Arguments("NotNullOrEmpty")]
    [Arguments("NotNullOrWhiteSpace")]
    public async Task ReplacesExistingNotNullAndPreservesArguments(string methodName)
    {
        var (document, diagnostics) = await AnalyzeDocumentAsync(
            """
            using CheckAndThrow;
            class C
            {
                void M(string value)
                {
                    // validate
                    Check.Arg.NotNull<string>(paramName: "input", argument: value);
                }
            }
            """
        );
        var guard = AddStringGuardAnalyzer.Guards.Single(item => item.MethodName == methodName);

        var changed = await AddStringGuardCodeFixProvider.AddGuardAsync(
            document,
            diagnostics.Single(),
            guard,
            CancellationToken.None
        );
        var text = (await changed.GetTextAsync()).ToString();

        await Assert
            .That(text)
            .Contains(
                $"global::CheckAndThrow.Check.Arg.{methodName}(paramName: \"input\", argument: value)"
            );
        await Assert.That(text).Contains("// validate");
        await Assert.That(text).DoesNotContain("NotNull<string>");
        await Assert.That(await CompilerErrorsAsync(changed)).IsEmpty();
    }

    [Test]
    [Arguments("NotNullOrEmpty")]
    [Arguments("NotNullOrWhiteSpace")]
    public async Task ExistingStringGuardSuppressesTheAction(string methodName)
    {
        var diagnostics = await AnalyzeAsync(
            $"using CheckAndThrow; class C {{ void M(string value) {{ Check.Arg.{methodName}(value); }} }}"
        );

        await Assert.That(diagnostics).IsEmpty();
    }

    [Test]
    public async Task DoesNotTreatUnrelatedOtherParameterOrConditionalCallsAsGuards()
    {
        var diagnostics = await AnalyzeAsync(
            """
            #nullable enable
            using CheckAndThrow;
            static class Other
            {
                public static void NotNullOrEmpty(string? value) { }
            }
            class C
            {
                void Unrelated(string value) { Other.NotNullOrEmpty(value); }
                void OtherParameter(string value, string other)
                {
                    Check.Arg.NotNullOrEmpty(argument: other, paramName: nameof(value));
                }
                void Conditional(string value)
                {
                    if (value.Length > 0) Check.Arg.NotNullOrWhiteSpace(value);
                }
            }
            """
        );

        await Assert.That(diagnostics).Count().IsEqualTo(3);
        await Assert
            .That(diagnostics.Select(diagnostic => diagnostic.GetMessage()))
            .All(message => message.Contains("'value'"));
    }

    [Test]
    [Arguments("NotNullOrEmpty")]
    [Arguments("NotNullOrWhiteSpace")]
    public async Task AddsGuardToAutoPropertyWithBackingField(string methodName)
    {
        var (document, diagnostics) = await AnalyzeDocumentAsync(
            """
            #nullable enable
            class C { public string Name { get; set; } }
            """,
            LanguageVersion.CSharp12
        );
        var guard = AddStringGuardAnalyzer.Guards.Single(item => item.MethodName == methodName);

        await Assert.That(diagnostics).Count().IsEqualTo(1);
        var changed = await AddStringGuardCodeFixProvider.AddGuardAsync(
            document,
            diagnostics.Single(),
            guard,
            CancellationToken.None
        );
        var text = (await changed.GetTextAsync()).ToString();

        await Assert.That(text).Contains($"private string _name;");
        await Assert
            .That(text)
            .Contains($"set => _name = global::CheckAndThrow.Check.Arg.{methodName}(value);");
        await Assert.That(text).DoesNotContain("field =");
        await Assert.That(await CompilerErrorsAsync(changed)).IsEmpty();
        await Assert.That(await AnalyzeAsync(text, LanguageVersion.CSharp12)).IsEmpty();
    }

    [Test]
    public async Task ExistingStringGuardSuppressesPropertyAction()
    {
        var diagnostics = await AnalyzeAsync(
            "using CheckAndThrow; class C { public string Name { get; set => field = Check.Arg.NotNullOrEmpty(value); } }",
            LanguageVersion.Preview
        );

        await Assert.That(diagnostics).IsEmpty();
    }

    static async Task<ImmutableArray<Diagnostic>> AnalyzeAsync(
        string source,
        LanguageVersion languageVersion = LanguageVersion.Preview
    )
    {
        var (_, diagnostics) = await AnalyzeDocumentAsync(source, languageVersion);
        return diagnostics;
    }

    static async Task<(
        Document Document,
        ImmutableArray<Diagnostic> Diagnostics
    )> AnalyzeDocumentAsync(
        string source,
        LanguageVersion languageVersion = LanguageVersion.Preview
    )
    {
        var workspace = new AdhocWorkspace();
        var project = workspace
            .AddProject("Test", LanguageNames.CSharp)
            .WithParseOptions(new CSharpParseOptions(languageVersion))
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
        workspace.TryApplyChanges(project.Solution);
        var document = workspace.AddDocument(project.Id, "Test.cs", SourceText.From(source));
        var diagnostics = await (await document.Project.GetCompilationAsync())!
            .WithAnalyzers(ImmutableArray.Create<DiagnosticAnalyzer>(new AddStringGuardAnalyzer()))
            .GetAnalyzerDiagnosticsAsync();
        return (document, diagnostics);
    }

    static async Task<IEnumerable<Diagnostic>> CompilerErrorsAsync(Document document) =>
        (await document.Project.GetCompilationAsync())!
            .GetDiagnostics()
            .Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error);
}
