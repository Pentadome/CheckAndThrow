# Multi-parameter Check.Args.NotNull action

## Context

- Add a second hidden Roslyn diagnostic/code fix that offers one `Check.Args.NotNull(...)` guard when a block-bodied method or constructor has multiple eligible parameters.
- Keep the existing per-parameter `CAT0001` diagnostic and `Add Check.Arg.NotNull guard` action intact for parameters that are not bulk-guarded. Extend only its existing-guard detection: when a top-level `Check.Args.NotNull` invocation semantically passes a parameter, `CAT0001` must not be reported for that parameter. The new bulk action owns `CAT0002` and does not replace `CAT0001`'s title, eligibility, or standalone behavior.

## Approach

- User decisions: include **all eligible parameters** (the same non-nullable/unannotated-generic eligibility used by `CAT0001`), require at least two, and expand the runtime `Check.Args.NotNull` API from arity 8 through **16**. Do **not** offer the bulk code fix when more than 16 eligible parameters exist.
- Treat existing standalone, top-level individual `Check.Arg.NotNull` guards and existing top-level `Check.Args.NotNull` guards as conversion input: the bulk action inserts one normalized `Check.Args.NotNull(...)` call for every eligible parameter, then removes individual guards and existing bulk guards that it replaces. After application, the covered parameters must no longer receive individual `CAT0001` actions.
- Never drop a consumed guard result. Support top-level assignment statements (`target = Check.Arg.NotNull(parameter);`, including local/field/property targets) and local declarations (`var local = Check.Arg.NotNull(parameter);`) as conversion input. When the shapes are homogeneous, preserve targets with one tuple assignment/deconstruction (for example, `(fieldA, fieldB) = Check.Args.NotNull(a, b);` or `var (localA, localB) = Check.Args.NotNull(a, b);`); use discards for parameters whose original standalone guard discarded its return.
- For mixed assignment/declaration groups, declare a collision-free local for the `Check.Args.NotNull(...)` tuple and rewrite each original assignment/declaration to its corresponding tuple member, preserving source-order targets. Unsupported result-consuming shapes (returns, arguments, conditional/nested expressions, non-top-level uses) are a safety boundary: the bulk action is unavailable rather than deleting or duplicating their checks.
- Preserve any existing `Check.Args.NotNull` invocation that includes a non-candidate argument (for example an explicitly nullable parameter or arbitrary expression), because deleting it would silently remove a validation. Only remove/normalize a bulk call when every checked argument semantically resolves to an eligible parameter in the declaration.
- Discover the accessible `CheckAndThrow.Check.Args.NotNull` generic overload semantically by its arity, report hidden `CAT0002` on the method/constructor identifier, and offer `Add Check.Args.NotNull guard`. The action inserts one fully qualified `global::CheckAndThrow.Check.Args.NotNull(...)` statement at the start of its block. The action is unavailable above arity 16 rather than splitting into multiple batches.
- Eligibility matches `CAT0001`: ordinary block-bodied methods/constructors only; non-nullable/oblivious reference types or unannotated non-value-constrained type parameters; exclude explicit nullable annotations, values/`struct`/`unmanaged` generics, `out`, generated/bodyless/expression-bodied declarations, local functions, lambdas, and primary constructors. Require 2–16 eligible parameters in declaration order.
- Locate each requested runtime overload with its semantic method symbol and verify exactly its 2–16 value parameters plus the matching optional caller-name parameters. Do not report if the required overload is absent/inaccessible. Keep analyzer work compilation-start scoped, concurrent, and cancellation-aware.
- During application, re-evaluate syntax/semantics, create the one bulk statement using original identifier tokens (including escaped names), remove or rewrite only replaceable top-level guard statements, and preserve constructor initializers, comments, directives, mixed guards, target assignment order, and all other statements. Retain full qualification rather than adding imports or risking `Check` collisions.

## Files to modify

- `CheckAndThrow/Check/Args/NotNull.cs` — add arity 9–16 overloads following the existing API pattern.
- `CheckAndThrow.Tests/Check/Args/NotNullTests.cs` — add focused tests for the new runtime overloads.
- `CheckAndThrow.Analyzers/diagnostics/AddNotNullGuards/` — new bulk analyzer/code-fix directory.
- `CheckAndThrow.Analyzers/diagnostics/AddNotNullGuard/` — recognize top-level `Check.Args.NotNull` calls as guards for individual `CAT0001` eligibility.
- `CheckAndThrow.Analyzers.Tests/diagnostics/AddNotNullGuards/AddNotNullGuardsTests.cs` — focused in-memory bulk analyzer/code-fix tests, reusing the existing test harness.
- `CheckAndThrow.Analyzers.Tests/diagnostics/AddNotNullGuard/AddNotNullGuardTests.cs` — add regression coverage that a bulk guard suppresses individual actions.
- `README.md` — bulk-action and expanded API documentation.

## Reuse

- `CheckAndThrow.Analyzers/diagnostics/AddNotNullGuard/` — hidden diagnostic, semantic API lookup, duplicate handling, and code-fix patterns.
- `CheckAndThrow/Check/Args/NotNull.cs` — existing caller-argument-expression, tuple-return, priority, and sequential-null-check pattern to extend.
- `CheckAndThrow.Tests/Check/Args/NotNullTests.cs` — existing runtime test conventions to extend.
- `CheckAndThrow.Analyzers.Tests/diagnostics/AddNotNullGuard/AddNotNullGuardTests.cs` — `AdhocWorkspace` test harness.

## Steps

- [x] Confirm bulk-action behavior and eligible-parameter rules.
- [x] Inspect `Check.Args.NotNull` overload limits and existing tests.
- [x] Add arity 9–16 `Check.Args.NotNull` overloads, retaining every existing API convention, and focused runtime tests for return values, first null failure, caller names, and trailing-string calls.
- [x] Add `CAT0002` under `AddNotNullGuards`, normalize replaceable existing guards with return-preserving tuple assignment/deconstruction or tuple-local projections, and update `CAT0001` duplicate detection for `Check.Args.NotNull`.
- [x] Add focused analyzer/code-fix tests plus `CAT0001` bulk-suppression regression coverage.
- [x] Document the bulk action, 2–16 range, conversion behavior, and excluded/over-limit cases.

## Verification

- Assert the bulk action appears only for 2–16 eligible parameters, inserts one expected `Check.Args.NotNull(...)` call, converts/removes replaceable individual/bulk guard statements, preserves assignments through the bulk tuple return, preserves mixed existing bulk calls, and prevents follow-up individual actions for covered parameters.
- Cover every new runtime arity (9–16): tuple return, null failure at every position with its inferred name, first-failure order, trailing-string overload resolution, and caller-name inference.
- Analyzer/code-fix coverage: 2, 8, 9, and 16 candidates; 1 and 17 candidates; explicit nullable/value/excluded declarations; absent runtime overload; existing individual guards; replaceable existing bulk guards; mixed bulk guards; standalone guards; tuple assignment to local/field/property targets; local declarations; mixed declaration/assignment tuple-local projections; escaped identifiers; unsupported result uses; and post-fix absence of both `CAT0002` and relevant `CAT0001` diagnostics.
- Build the solution and run the analyzer and runtime test executables for `net8.0` and `net10.0` after approval. Pack the runtime package and inspect that the existing bundled analyzer delivery remains intact.
