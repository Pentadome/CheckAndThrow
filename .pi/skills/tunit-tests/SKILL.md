---
name: tunit-tests
description: Write, update, and review C# tests using TUnit. Use when asked to add TUnit tests, cover a bug or feature in a TUnit project, convert tests to TUnit, or fix TUnit test failures. Follow the installed version, repository conventions, and actual test runner.
---

# TUnit tests

Write the smallest readable tests that prove the requested behavior. This skill provides guidance, not a separate agent or permission to change unrelated code.

## Inspect before writing

1. Read repository instructions, the code under test, and nearby tests. Trace relevant callers when covering a bug so the regression exercises the actual failing path.
2. Inspect the test project, `Directory.Packages.props`, `Directory.Build.props`/`.targets`, `global.json`, global usings, and CI/test scripts where present. Establish the installed TUnit version, target framework, assertion library, and runner command.
3. Reuse the existing test project, naming style, helpers, data sources, and dependencies. Do not create a new test project or upgrade packages when the existing setup suffices. If no test project exists, follow repository conventions and confirm version compatibility before adding the minimum setup.
4. For unfamiliar or version-sensitive APIs, consult installed symbols/source or version-matched official documentation at https://tunit.dev and https://github.com/thomhurst/TUnit. Do not guess assertion, lifecycle, data-source, cancellation, or runner APIs from another framework or a newer release.

## Choose meaningful cases

- Test observable behavior through the public contract, not private implementation details.
- Cover the requested happy path, relevant boundaries, and meaningful invalid/error cases; avoid mechanically testing every possible input.
- For regressions, reproduce the reported failure. Run the test against the unfixed code when feasible and verify that it fails for the intended reason, not a build/setup problem.
- Derive expected values independently of the implementation. Never change expectations merely to match a failure.
- Use descriptive behavior-based names and clear arrange/act/assert structure. Prefer one behavior per test; multiple assertions are fine when they jointly establish that behavior.

## TUnit conventions

- Use TUnit's `[Test]` attribute, not xUnit `[Fact]` or NUnit fixtures.
- TUnit assertions are awaitable: await every assertion. Use `async Task` for async tests, never `async void`, `.Wait()`, or `.Result`.
- Prefer `await Assert.That(actual)...` with matchers supported by the installed version. Follow an existing alternative assertion library when the project deliberately uses one; do not add another.
- Use `[Arguments(...)]` for small sets of compile-time cases that share the same behavior. Use existing/version-supported data sources for complex data; do not build a data-generation framework for a few cases.
- For exceptions, assert the expected type and meaningful contract details such as `ParamName`. Avoid brittle full-message comparisons unless the message itself is the contract. Match sync/async exception APIs to the installed assertion library and await async operations.
- Keep setup local until reuse justifies a fixture or hook. Use TUnit lifecycle hooks only when needed and verify their signatures for the installed version; do not copy NUnit/xUnit lifecycle patterns.

## Isolation and reliability

- Assume tests may execute in parallel. Avoid mutable static fixtures and shared files, ports, environment variables, or database records.
- Give each test its own resources and dispose/clean them up even on failure. Reuse repository fixture patterns when they safely provide isolation.
- Use existing clock/dependency seams, deterministic inputs, and explicit synchronization rather than sleeps, live network calls, or timing guesses. Do not add production abstractions solely for a trivial test.
- Use an existing mocking library only at genuine external boundaries; prefer simple real values and objects otherwise.
- If a shared resource genuinely cannot be isolated, use the narrowest supported TUnit serialization constraint. Do not disable parallelism for the entire suite to hide a race.
- Do not use retries, ignored tests, swallowed exceptions, or weaker assertions to make failures disappear.

## Verify and report

1. Run the repository's established command or IDE run configuration for the changed tests. TUnit uses Microsoft.Testing.Platform; support for `dotnet test`, invocation syntax, and filters depends on the SDK and project configuration. Do not assume VSTest filters or add adapters/packages to force an unverified command to work. Inspect scripts or runner help first when needed.
2. Confirm the intended tests were discovered and executed: zero tests or a build-only success is not a passing test run.
3. Run the affected test project or relevant broader suite after the focused tests pass, as practical. Inspect changed files for compile/analyzer errors.
4. Fix failures introduced by the change. Report unrelated failures or missing SDK/package/runner prerequisites explicitly; do not claim verification that did not run.
5. Finish with changed file paths, the behavior covered, the exact command/configuration used, and pass/fail counts or a concise blocker. Distinguish focused verification from a full suite run.

Keep the diff focused. No unrequested framework migrations, package upgrades, fixture hierarchies, mocks, or coverage tooling.
