# Analyzer demo

Open `CheckAndThrow.sln` in Rider or Visual Studio. Open `GuardExamples.cs`, put caret on parameter or `Count`, then invoke Quick Actions (Rider: Alt+Enter; Visual Studio: Ctrl+.). Keep examples unfixed so actions remain available.

| Example | Expected action |
| --- | --- |
| `RequireObject` | CAT0001 — Add `Check.Arg.NotNull` guard |
| `RequireBoth` | CAT0002 — Add `Check.Args.NotNull` guard for both parameters |
| `RequireText` | CAT0004 — Add string guard clause (`NotNullOrEmpty` or `NotNullOrWhiteSpace`) |
| `RequirePositive`, `Count` | CAT0003 — Add range guard clause (choose sign rule) |

CAT0001 also appears on each unguarded reference parameter in `RequireBoth` and on `RequireText`: actions intentionally overlap. `Count` demonstrates property-setter fix. Diagnostics are hidden IDE suggestions, **not build warnings**. Project references local runtime and local analyzer directly; no published package required.

Run `dotnet build CheckAndThrow.Analyzers.Demo/CheckAndThrow.Analyzers.Demo.csproj` from worktree root to verify source compiles. Try actions in IDE, then undo changes to restore examples.
