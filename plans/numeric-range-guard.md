# Numeric range-guard Quick Action

## Context

Add a hidden-by-default diagnostic on numeric parameters without a recognized CheckAndThrow range guard. The Quick Actions menu should offer **Add range guard clause** with nested choices such as **Guard against negative** and **Guard against negative and zero**.

## Approach

- Introduce `CAT0003` and a nested Roslyn code fix, following the existing null-guard analyzers.
- Bind against methods actually available in the consumer's referenced `CheckAndThrow.Check.Arg`; do not assume generic math exists on older targets.
- Reuse existing range methods: `Negative`, `Positive`, `ZeroOrNegative`, `ZeroOrPositive`, `InRange`, and the positive/negative infinity variants.
- Recognize standalone guards and guards whose returned value is used; generate a value-preserving inline guard where safe, otherwise a top-level statement.
- Keep the analyzer on `netstandard2.0` / Roslyn **4.8.0**. Discover consumer symbols rather than referencing generic-math types from the analyzer itself.
- No changes to numeric runtime guard behavior, existing null-guard behavior, or dependencies. Existing analyzer packaging and `CheckAndThrowEnableAnalyzers` opt-out already cover new providers.

### Diagnostic and numeric eligibility

- `CAT0003`, category `Usage`, severity `Hidden`, enabled by default; report on each eligible parameter identifier only when an applicable action exists and no recognized guard exists.
- Include built-in integer/floating/decimal types (including native integers), concrete types implementing `INumberBase<TSelf>`, and self-constrained generic parameters (including `INumber<T>` and derived numeric interfaces). Follow indirect constraints semantically.
- Exclude nullable value/reference parameters, `out`, `bool`, `char`, enums, dynamic/object, pointers, unconstrained generics, and types constrained only by `IComparable<T>`. Allow readable `ref`/`in` parameters, but never rewrite a by-reference use inline.
- Restrict declarations to methods and ordinary constructors with executable bodies. Exclude abstract/extern/interface signatures, primary constructors, operators, local functions, and lambdas.
- Resolve fully qualified candidate calls semantically against the consumer compilation, including overload resolution and constraints. Do not infer support from a target-framework string or type spelling. Do not offer unbindable or ambiguous actions.

### Nested menu

Register one `CodeAction.Create("Add range guard clause", children, isInlinable: false)` group. Each child has a stable, distinct equivalence key. Like existing providers, leave Fix All unsupported; selecting a sign constraint is a per-parameter decision.

| Child title | Existing method |
| --- | --- |
| Guard against negative | `Check.Arg.ZeroOrPositive(value)` |
| Guard against negative and zero | `Check.Arg.Positive(value)` |
| Guard against positive | `Check.Arg.ZeroOrNegative(value)` |
| Guard against positive and zero | `Check.Arg.Negative(value)` |
| Guard against negative and zero (allow positive infinity) | `Check.Arg.PositiveOrInfinity(value)` |
| Guard against positive and zero (allow negative infinity) | `Check.Arg.NegativeOrNegativeInfinity(value)` |

Offer infinity variants for floating-point/generic numeric types where meaningful and supported, not ordinary integral/decimal types where they duplicate the finite checks. For known unsigned types, omit always-passing negative rejection and impossible negative-only checks; retain useful checks such as rejecting zero or positive values when an overload applies. Do not infer signedness for arbitrary generic constraints/custom types.

### Recognizing existing guards

- Match the actual `CheckAndThrow.Check.Arg` method symbols for the six menu methods plus `InRange`; aliases, qualification, static imports, and explicit generic arguments must work.
- Use the bound invocation's `value` argument and parameter symbol, not argument text/order. Handle named/reordered arguments and implicit numeric conversions; checking another parameter, a bound (`min`/`max`), or an expression such as `value + 1` does not count.
- Recognize an invocation as a top-level statement or as an unconditionally evaluated value in a top-level local initializer, simple assignment, return, member/call chain, or tuple assignment; also recognize corresponding expression-bodied forms and direct constructor-initializer arguments.
- Do not count guards hidden inside lambdas/local functions, conditional branches, short-circuit operands, loops, or other nested control flow. Traverse only the supported unconditional expression paths, not arbitrary descendants.
- This is guard-presence detection, consistent with the existing actions—not a full control-flow proof that validation precedes every use. A guard's chosen sign/bounds are not inferred or changed.

### Code-fix placement and safety

- Revalidate the parameter, available overload, and absence of an existing guard when applying the action. Generate `global::CheckAndThrow.Check.Arg.<Method>(parameter)` with the escaped identifier preserved.
- Prefer replacing the parameter's direct read in the first executable statement: a single local initializer, simple local/parameter/`this`-field assignment, direct return, or direct member receiver. Inline only if no earlier expression can mutate the input or produce side effects and the guard returns the original parameter type.
- Support simple expression-bodied equivalents, including constructor field assignments. Preserve a simple constructor tuple assignment inline only when its targets and preceding RHS values are side-effect-free direct field/parameter reads; otherwise use the standalone fallback.
- For wider-returning overloads (for example, a small integer binding to an `int` overload on an older API), keep the original use untouched and insert a standalone guard. Do not change inferred `var` types, overload selection, ref semantics, or evaluate the original expression twice.
- Otherwise prepend one guard to the block. Convert an expression body to a block when needed, preserving constructor/void statements, value/ref returns, async/await, throw expressions, and trivia. If a form cannot be safely transformed, do not report an action with no valid fix.
- Preserve constructor initializers and their execution order; body guards execute after `base(...)`/`this(...)`. Recognize an existing direct initializer guard but do not relocate initialization. No tuple-local machinery is needed for a single numeric guard.

## Agreed scope

- Offer sign and infinity guards. Recognize existing `InRange` calls, but do not generate custom bounds or a bounds-entry UI.
- Support methods and constructors with block or expression bodies. Exclude local functions, lambdas, and declarations without executable bodies.
- Prefer using the returned value in a safe, unconditional first use; otherwise insert a top-level guard. No separate placement submenu.
- Count guards from the existing CheckAndThrow range API, not handwritten comparisons or unrelated libraries. This action offers a choice, not an inferred business constraint.

## Files to modify

- New `CheckAndThrow.Analyzers/diagnostics/AddRangeGuard/AddRangeGuardAnalyzer.cs`.
- New `CheckAndThrow.Analyzers/diagnostics/AddRangeGuard/AddRangeGuardCodeFixProvider.cs`.
- New `CheckAndThrow.Analyzers.Tests/diagnostics/AddRangeGuard/AddRangeGuardTests.cs`.
- `README.md` for the diagnostic and menu documentation.

## Reuse

- `CheckAndThrow/Check/Arg/Range/*.cs`: existing guard contracts. Sign generic overloads constrain `INumberBase<T>`; `InRange<T>` constrains `IComparable<T>` (numeric eligibility must not include every comparable type).
- `CheckAndThrow.Analyzers/diagnostics/AddNotNullGuard/*`: diagnostic registration, parameter locations, semantic symbol checks, top-level/inline guard patterns.
- `CheckAndThrow.Analyzers/diagnostics/AddNotNullGuards/AddNotNullGuardsCodeFixProvider.cs`: semantic revalidation, fully qualified calls, preserving return-value uses and evaluation order.
- Existing analyzer TUnit tests: in-memory Roslyn workspace, code action execution, compilation checks.

## Steps

- [x] Settle menu behavior: sign/infinity choices, methods/constructors, safe inline placement with standalone fallback.
- [x] Add the analyzer with compilation-scoped symbol discovery, numeric eligibility, applicable guard choices, and existing-guard detection. Keep shared range-specific helpers internal to the analyzer; do not introduce a general guard framework.
- [x] Add the nested code-fix provider, reusing the range analyzer's symbol/eligibility helpers and the established syntax/formatting patterns.
- [x] Add TUnit regressions exercising each child action and the recognition/placement boundaries below.
- [x] Document `CAT0003`, its menu-to-method mappings, supported scope, finite/infinity behavior, and `.editorconfig` opt-out in `README.md`.
- [x] Run automated checks and verify the nested menu in Rider.

## Verification

Use the existing **TUnit 1.66.27** executable test project and its `AdhocWorkspace` / `ApplyChangesOperation` pattern. Select nested child actions rather than attempting to execute the parent group. Keep helpers local to the new test file; no test-framework migration or shared harness refactor.

- Assert hidden severity, parameter location, one non-inlineable parent, exact available child titles, and independent equivalence keys. Multiple numeric parameters get independent diagnostics; fixing one must not clear the others.
- Compile source and fixed output; assert no compiler errors, the selected guard's bound method, correct returned/inferred types, and disappearance of the selected diagnostic. A second pass must add no duplicate guard.
- Parameterize representative signed/unsigned primitives, small/native integers, float/double/decimal, `Half`/`BigInteger`, `INumber<T>`/`INumberBase<T>` and indirect constraints. Cover exclusions and absent/inapplicable APIs. Use a minimal in-memory legacy API fixture without generic overloads to check older-surface fallback without new runtime targets or packages.
- Cover all seven existing guard methods, standalone and returned-value shapes, aliases/static imports, reordered named arguments, unrelated methods, wrong parameters, and `InRange` bounds mistaken for the guarded value. Branch/lambda-only checks must not suppress the diagnostic.
- Exercise standalone fallback, local/field assignments, returns, direct-member use, expression-bodied methods/constructors, simple tuple assignments, ref/async/void cases, escaped identifiers, comments, and constructor initializers. Assert unsafe conditional/side-effecting uses are not wrapped and preexisting expressions are preserved.
- Confirm existing null-guard tests still pass and runtime guard behavior is untouched. Inspect changed C# files for fresh compiler/analyzer errors during implementation.

After approval, run from the repository root:

```sh
dotnet build CheckAndThrow.Analyzers.Tests/CheckAndThrow.Analyzers.Tests.csproj -c Release
dotnet build CheckAndThrow.Tests/CheckAndThrow.Tests.csproj -c Release
dotnet run --project CheckAndThrow.Analyzers.Tests/CheckAndThrow.Analyzers.Tests.csproj -c Release -f net8.0 --no-build
dotnet run --project CheckAndThrow.Analyzers.Tests/CheckAndThrow.Analyzers.Tests.csproj -c Release -f net10.0 --no-build
dotnet run --project CheckAndThrow.Tests/CheckAndThrow.Tests.csproj -c Release -f net8.0 --no-build
dotnet run --project CheckAndThrow.Tests/CheckAndThrow.Tests.csproj -c Release -f net10.0 --no-build
```

Manually place the caret on an unguarded `int`/`double` parameter in Rider, open Quick Actions, expand **Add range guard clause**, apply a child, and verify the source edit and disappearing action. Check the existing project-wide analyzer opt-out. The IDE controls context-menu presentation; this is not an arbitrary custom right-click command. If manual IDE verification is unavailable, report it explicitly.

No builds, tests, or source changes run during this planning phase.

## Initial findings

- Working tree was clean before this planning file.
- `CAT0001` and `CAT0002` are taken; `CAT0003` is available.
- Primitive overload coverage differs by guard. Bound each offered child to an actually applicable overload; avoid invalid fixes or type-widening inline replacements.
- Existing bulk-null fix supports methods/constructors with block bodies and a narrowly defined expression-bodied constructor tuple case; do not assume it handles arbitrary expression bodies.
