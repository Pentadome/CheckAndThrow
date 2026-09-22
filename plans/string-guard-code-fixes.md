# String guard code fixes

## Context
- Add quick fixes for string parameters using the existing `Check.Arg.NotNullOrEmpty` and `Check.Arg.NotNullOrWhiteSpace` APIs.
- Once either guard is present for a parameter, do not suggest an additional null-only guard.
- Planning only: no source changes or test runs yet.

## Approach
- Add hidden diagnostic `CAT0004` on eligible string parameter identifiers, with an **Add string guard clause** action group containing **Add Check.Arg.NotNullOrEmpty guard** and **Add Check.Arg.NotNullOrWhiteSpace guard**. Use distinct equivalence keys and no Fix All, matching current providers.
- Scope: non-nullable `System.String` parameters of block-bodied methods/constructors, following existing null-guard eligibility. Exclude `string?`, `out`, non-string types, members without block bodies, lambdas, local functions, and primary constructors. Do not expand expression-body support in this change.
- Offer only methods actually available on the referenced `CheckAndThrow.Check.Arg` type; resolve the non-generic string overloads, not the collection `NotNullOrEmpty` overload. Revalidate eligibility, method availability, and existing string guards when registering/applying fixes. Neither runtime API nor package dependencies need changing.
- Extend shared existing-guard detection so the single-parameter null-only analyzer and code fix recognize either string guard semantically, for the same parameter only.
- Discovery: method-wide `CAT0002` does **not** call `HasExistingGuard`; it normalizes all eligible parameters into `Check.Args.NotNull`.
- User decision: exclude parameters with either string check from bulk-null eligibility. Keep the bulk action for the remaining parameters only when 2–16 remain; filter in `GetEligibleParameters`, which both analysis and fix application already use. Preserve stronger checks and existing bulk calls that span excluded parameters; do not broaden bulk tuple rewriting or expression-bodied-constructor support.
- User decision: keep both string-check actions available when a single-parameter `Check.Arg.NotNull` already exists, and replace that call with the selected stronger guard instead of adding a duplicate.
- Replace a recognized existing null call in place, preserving its arguments (including an explicit `paramName`), trivia, assignment/local/member-use context, and order. Remove explicit generic type arguments because the string guards are non-generic. Generate a fully qualified target so aliases and static imports remain safe.
- Without an existing individual null call, follow the null provider: inline the guard for a direct first local initializer such as `var length = value.Length`, otherwise prepend a fully qualified guard statement. Do not split existing `Check.Args.NotNull` calls or change their tuple results.
- Either existing string guard suppresses both string actions and the individual null-only action for that parameter. Match the actual method/type and bound first API parameter (`argument`), including reordered named arguments; unrelated lookalikes and `paramName` references must not suppress actions.
- Recognize the existing unconditional top-level statement/assignment/local and direct inline-member forms. Do not treat checks inside branches, loops, lambdas, or local functions as unconditional validation. Use a small reusable matcher in the new analyzer; do not widen `GetTopLevelGuardInvocation` in ways that alter bulk-rewrite safety.

## Files to modify
- New `CheckAndThrow.Analyzers/diagnostics/AddStringGuard/AddStringGuardAnalyzer.cs`.
- New `CheckAndThrow.Analyzers/diagnostics/AddStringGuard/AddStringGuardCodeFixProvider.cs`.
- `CheckAndThrow.Analyzers/diagnostics/AddNotNullGuard/AddNotNullGuardAnalyzer.cs`.
- New `CheckAndThrow.Analyzers.Tests/diagnostics/AddStringGuard/AddStringGuardTests.cs`.
- `CheckAndThrow.Analyzers.Tests/diagnostics/AddNotNullGuard/AddNotNullGuardTests.cs`.
- `CheckAndThrow.Analyzers/diagnostics/AddNotNullGuards/AddNotNullGuardsAnalyzer.cs`: filter bulk eligibility; its provider already consumes this helper.
- `CheckAndThrow.Analyzers.Tests/diagnostics/AddNotNullGuards/AddNotNullGuardsTests.cs`.
- `README.md`: describe string actions and both suppression rules.

## Reuse
- `CheckAndThrow/Check/Arg/String.cs`: both runtime APIs already exist and return the validated string; no runtime API additions needed.
- `CheckAndThrow.Analyzers/diagnostics/AddNotNullGuard/AddNotNullGuardAnalyzer.cs`: `IsEligible`, `HasExistingGuard`, `GetTopLevelGuardInvocation`.
- `CheckAndThrow.Analyzers/diagnostics/AddNotNullGuard/AddNotNullGuardCodeFixProvider.cs`: semantic revalidation, fully qualified calls, insertion and first-local-initializer inline handling.
- `CheckAndThrow.Analyzers/diagnostics/AddRangeGuard/`: existing hidden diagnostic (`CAT0003`), grouped multi-action provider, no Fix All, and apply-time revalidation patterns.
- `CheckAndThrow.Analyzers/diagnostics/AddNotNullGuards/`: `CAT0002` normalizes standalone and consumed null guards; do not weaken or delete stronger checks when touching this path.
- `CheckAndThrow.Analyzers.Tests/diagnostics/`: existing TUnit test files mirror diagnostic folders; reuse their Roslyn workspace/action helpers rather than adding dependencies.

## Steps
- [x] Confirm action behavior for already-null-checked strings: replace the existing single-parameter null check.
- [x] Trace method-wide null checks, multi-action fixes, test helpers, packaging, and documentation conventions. Existing analyzer assembly packaging discovers the new exports automatically.
- [x] Establish analyzer-suite baseline after approval, then add focused regression tests for the agreed behavior.
- [x] Implement string diagnostic/provider with two actions, safe null-call replacement, and duplicate suppression.
- [x] Update shared null-guard detection to recognize both string checks; exclude string-guarded parameters from bulk eligibility without removing their validation.
- [x] Update the README's IDE actions and diagnostic-disable instructions for `CAT0004` and the suppression behavior.
- [x] Run changed-file diagnostics and the analyzer suite on both frameworks; compare against baseline and review the final diff.

## Verification
Use existing TUnit `1.66.27` and Roslyn `4.8.0`, with the local `AdhocWorkspace`/`CodeFixContext`/`ApplyChangesOperation` test patterns. Parameterize the two guard names where appropriate; assert compiler errors are absent and re-run analyzers on fixed documents. Do not add a testing framework or refactor unrelated test helpers.

| Area | Required checks |
| --- | --- |
| Actions | Hidden `CAT0004`; both child titles and distinct keys; apply each registered child action to methods and constructors; fully qualified statement and direct first-local-member insertion; escaped identifiers. |
| Eligibility | No actions for nullable/out/non-string parameters, unsupported declaration shapes, or absent library APIs; only the available action when one string API is missing. Use valid C# snippets. |
| Upgrade | Replace `NotNull` in standalone, assignment, local initializer, and inline-member forms; preserve comments and explicit `paramName`; cover explicit generics, aliases/static imports, and reordered named arguments. No new standalone duplicate. |
| Suppression | Either string guard removes `CAT0004` and `CAT0001` only for its bound value parameter; test both pre-existing guards and newly applied fixes. Same-named unrelated methods, another parameter, `paramName`-only references, and conditional/deferred guards must not suppress it. |
| Bulk | With one guarded string and two other eligible parameters, `CAT0002` adds only those two and preserves the string guard. With only one remaining parameter, no bulk action but its individual null action remains. Apply the 2–16 bounds after filtering (including 17 total/16 remaining). Preserve existing mixed bulk calls and consumed values. |
| Safety | Reapplication/stale diagnostics do not add duplicate checks; no runtime-library or dependency changes; all existing null/bulk behavior remains covered. |

After approval, build and run the executable TUnit analyzer-test project on both targets (Microsoft.Testing.Platform is selected by `global.json`):

```sh
dotnet build CheckAndThrow.sln --configuration Release
dotnet run --project CheckAndThrow.Analyzers.Tests/CheckAndThrow.Analyzers.Tests.csproj --framework net8.0 --configuration Release --no-build
dotnet run --project CheckAndThrow.Analyzers.Tests/CheckAndThrow.Analyzers.Tests.csproj --framework net10.0 --configuration Release --no-build
```

- Probe changed C# files with active LSP diagnostics (or Rider file analysis if LSP coverage is unavailable) before the final build; record discovered/executed test counts, not just build success.
- CI's test step is commented out, so a CI build alone is insufficient. Existing range test `ReportsNumericParametersWithHiddenDiagnostic` expects `Info` while its descriptor is `Hidden`: establish the baseline and report any unrelated failure without expanding this feature to fix it.
- Manually check Quick Actions on a string parameter: choose either action, confirm `NotNull` is replaced when present, then confirm the individual null suggestion disappears and bulk suggestions omit that parameter.
- No builds/tests have been run during planning; only this markdown plan has been written.
