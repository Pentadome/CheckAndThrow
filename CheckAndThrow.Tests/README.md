# CheckAndThrow.Tests

TUnit 1.66.27, targeting .NET 8 and .NET 10. Requires a .NET 10 SDK and both runtimes.
Run from the repository root:

```sh
dotnet build CheckAndThrow.Tests/CheckAndThrow.Tests.csproj -c Release
dotnet run --project CheckAndThrow.Tests/CheckAndThrow.Tests.csproj -c Release -f net8.0 --no-build
dotnet run --project CheckAndThrow.Tests/CheckAndThrow.Tests.csproj -c Release -f net10.0 --no-build
```

TUnit supplies its own executable entry point; no VSTest/coverlet package is needed.
Tests inject fixed clocks; the test changing `DefaultTimeProvider` is nonparallel and restores it in `finally`.
To rerun that test, append `-- --treenode-filter '/*/*/CheckTests/*'` to a run command.

## Validation

The current baseline builds without warnings or errors. Each target runs **473 tests: 473 passed, 0 failed, 0 skipped**.

## Public API coverage checklist

Every source path below maps to the same test path with a `Tests.cs` suffix.
Directly typed calls cover **233 public methods/overloads plus both property accessors (235 signatures)** on both targets. A compiled metadata audit compared library method signatures with test-assembly member references (including generic arity and parameter types): **235 referenced, 0 missing** for each target. This is an overload-presence check, not a claim of exhaustive input or branch coverage.

Numeric tests explicitly cast to each concrete type and explicitly select generic overloads. Throw test names record generic/non-generic and parameter types. Tuple tests select all three generic arities. Each check has accepted/rejected cases; all throw overloads assert their exact exception types and relevant details.

| Source path | Public signatures | Methods checked (overload counts) |
| --- | ---: | --- |
| `Check/Arg/Collection.cs` | 8 | `NotNullOrEmpty`, `NotNullAndHasNoNulls`, `HasCount`, `HasMinCount`, `HasMaxCount`, `Contains`, `HasValidIndex` x2 |
| `Check/Arg/DateTime.cs` | 10 | `InPast` x2, `InPastUtc`, `InFuture` x2, `InFutureUtc`, `LaterThan` x2, `EarlierThan` x2 |
| `Check/Arg/Enum.cs` | 5 | `ValidEnumValue`, `ValidEnumValueName`, `ValidEnumValueNameIgnoreCase`, `HasAllFlags`, `HasAnyOfFlags` |
| `Check/Arg/Guid.cs` | 1 | `NotGuidEmpty` |
| `Check/Arg/NotNull.cs` | 1 | `NotNull` |
| `Check/Arg/Range/Negative.cs` | 8 | `Negative` x8 |
| `Check/Arg/Range/Positive.cs` | 11 | `Positive` x11 |
| `Check/Arg/Range/Range.cs` | 11 | `InRange` x11 |
| `Check/Arg/Range/ZeroOrNegative.cs` | 8 | `ZeroOrNegative` x8 |
| `Check/Arg/Range/ZeroOrPositive.cs` | 8 | `ZeroOrPositive` x8 |
| `Check/Arg/State.cs` | 2 | `HasValidState` x2 |
| `Check/Arg/String.cs` | 7 | `NotNullOrEmpty`, `NotNullOrWhiteSpace`, `Matches`, `HasLength`, `HasMinLength`, `HasMaxLength`, `Email` |
| `Check/Arg/Type.cs` | 4 | `AssignableTo`, `AssignableFrom`, `HasTheAttribute` x2 |
| `Check/Args/NotNull.cs` | 3 | `NotNull` x3 |
| `Check/Check.cs` | 2 accessors | `DefaultTimeProvider` get/set |
| `Check/Expression.cs` | 5 | `NotNull`, `NotEqualTo`, `EqualTo`, `True`, `False` |
| `Check/State.cs` | 12 | `NotDisposed` x4, `Initialized` x4, `Mutable` x4 |
| `Throw/Arg/Arg.cs` | 4 | `Exception` x4 |
| `Throw/Arg/Collection.cs` | 21 | `Empty` x2, `HasNullValue` x2, `InvalidCount` x4, `TooFewItems` x4, `TooManyItems` x4, `DoesNotContain`, `InvalidIndex` x4 |
| `Throw/Arg/DateTime.cs` | 16 | `NotInPast` x4, `NotInFuture` x4, `NotLaterThan` x4, `NotEarlierThan` x4 |
| `Throw/Arg/Enum.cs` | 10 | `InvalidEnumValue` x6, `MissesAnyOfTheFlags` x2, `MissesAllFlags` x2 |
| `Throw/Arg/Guid.cs` | 2 | `GuidEmpty` x2 |
| `Throw/Arg/NotNull.cs` | 2 | `Null` x2 |
| `Throw/Arg/Range/Negative.cs` | 2 | `NotNegative` x2 |
| `Throw/Arg/Range/Positive.cs` | 2 | `NotPositive` x2 |
| `Throw/Arg/Range/Range.cs` | 2 | `OutOfRange` x2 |
| `Throw/Arg/Range/ZeroOrNegative.cs` | 2 | `NotZeroOrNegative` x2 |
| `Throw/Arg/Range/ZeroOrPositive.cs` | 2 | `NotZeroOrPositive` x2 |
| `Throw/Arg/String.cs` | 24 | `NullOrEmpty` x2, `NullOrWhiteSpace` x2, `DoesNotMatch` x4, `InvalidLength` x4, `TooShort` x4, `TooLong` x4, `InvalidEmail` x4 |
| `Throw/Arg/Type.cs` | 12 | `NotAssignableTo` x4, `NotAssignableFrom` x4, `DoesNotHaveAttribute` x4 |
| `Throw/State.cs` | 12 | `Disposed` x4, `NotInitialized` x4, `NotMutable` x4 |
| `Throw/Throw.cs` | 16 | `Unreachable` x4, `NotImplemented` x4, `NotSupported` x4, `InvalidOperation` x4 |

Declaration-only partial classes, internal annotation/cache files, generated Polyfill code, and global usings have no direct tests. The .NET Standard fallback implementations are outside the approved runtime scope.
