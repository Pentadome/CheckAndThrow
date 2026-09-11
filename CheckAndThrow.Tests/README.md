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

## Validation and known defects

Both targets build without warnings or errors. Each currently runs **466 tests: 464 passed, 2 failed, 0 skipped**.
The two failing contract tests intentionally remain enabled; production changes were not authorized:

- `Check/Arg/EnumTests.HasAnyOfFlags_UnsignedHighBit_IsValid`: `HasAnyOfFlags` converts unsigned enum values through `Int32`, overflowing for a valid `ulong` high flag (`CheckAndThrow/Check/Arg/Enum.cs:138`).
- `Check/Arg/Range/NegativeTests.Negative_Generic_NegativeZeroIsNotNegative`: the generic overload accepts `-0.0`, although strict negativity excludes zero and the concrete overload rejects it (`CheckAndThrow/Check/Arg/Range/Negative.cs:149`).

Concrete floating-point guards currently accept NaN, while most generic counterparts reject it. Tests named `NaN_CurrentBehavior` characterize the existing concrete behavior, not a new guarantee; a unified NaN policy needs a separate decision.

## Public API coverage checklist

Every source path below maps to the same test path with a `Tests.cs` suffix.
Directly typed calls cover **233 public methods/overloads plus both property accessors (235 signatures)** on both targets. A compiled metadata audit compared library method signatures with test-assembly member references (including generic arity and parameter types): **235 referenced, 0 missing** for each target. This is an overload-presence check, not a claim of exhaustive input or branch coverage.

Numeric tests explicitly cast to each concrete type and explicitly select generic overloads. Throw test names record generic/non-generic and parameter types. Tuple tests select all three generic arities. Each check has accepted/rejected cases; all throw overloads assert their exact exception types and relevant details.

| Source path | Public signatures | Methods checked (overload counts) |
| --- | ---: | --- |
| `Check/Arg/Collection.cs` | 8 | `IsNotNullOrEmpty`, `IsNotNullAndHasNoNulls`, `HasCount`, `HasMinCount`, `HasMaxCount`, `Contains`, `HasValidIndex` x2 |
| `Check/Arg/DateTime.cs` | 10 | `IsInPast` x2, `IsInPastUtc`, `IsInFuture` x2, `IsInFutureUtc`, `IsLaterThan` x2, `IsEarlierThan` x2 |
| `Check/Arg/Enum.cs` | 5 | `IsValidEnumValue`, `IsValidEnumValueName`, `IsValidEnumValueNameIgnoreCase`, `HasAllFlags`, `HasAnyOfFlags` |
| `Check/Arg/Guid.cs` | 1 | `IsNotGuidEmpty` |
| `Check/Arg/NotNull.cs` | 1 | `NotNull` |
| `Check/Arg/Range/Negative.cs` | 8 | `Negative` x8 |
| `Check/Arg/Range/Positive.cs` | 11 | `Positive` x11 |
| `Check/Arg/Range/Range.cs` | 11 | `InRange` x11 |
| `Check/Arg/Range/ZeroOrNegative.cs` | 8 | `ZeroOrNegative` x8 |
| `Check/Arg/Range/ZeroOrPositive.cs` | 8 | `ZeroOrPositive` x8 |
| `Check/Arg/State.cs` | 2 | `HasValidState` x2 |
| `Check/Arg/String.cs` | 7 | `IsNotNullOrEmpty`, `IsNotNullOrWhiteSpace`, `Matches`, `HasLength`, `HasMinLength`, `HasMaxLength`, `IsEmail` |
| `Check/Arg/Type.cs` | 4 | `IsAssignableTo`, `IsAssignableFrom`, `HasTheAttribute` x2 |
| `Check/Args/NotNull.cs` | 3 | `NotNull` x3 |
| `Check/Check.cs` | 2 accessors | `DefaultTimeProvider` get/set |
| `Check/Expression.cs` | 5 | `NotNull`, `NotEqualTo`, `EqualTo`, `IsTrue`, `IsFalse` |
| `Check/State.cs` | 12 | `NotDisposed` x4, `IsInitialized` x4, `IsMutable` x4 |
| `Throw/Arg/Arg.cs` | 4 | `Exception` x4 |
| `Throw/Arg/Collection.cs` | 21 | `IsEmpty` x2, `HasNullValue` x2, `InvalidCount` x4, `TooFewItems` x4, `TooManyItems` x4, `DoesNotContain`, `IsInvalidIndex` x4 |
| `Throw/Arg/DateTime.cs` | 16 | `IsNotInPast` x4, `IsNotInFuture` x4, `IsNotLaterThan` x4, `IsNotEarlierThan` x4 |
| `Throw/Arg/Enum.cs` | 10 | `IsInvalidEnumValue` x6, `MissesAnyOfTheFlags` x2, `MissesAllFlags` x2 |
| `Throw/Arg/Guid.cs` | 2 | `IsGuidEmpty` x2 |
| `Throw/Arg/NotNull.cs` | 2 | `IsNull` x2 |
| `Throw/Arg/Range/Negative.cs` | 2 | `NotNegative` x2 |
| `Throw/Arg/Range/Positive.cs` | 2 | `NotPositive` x2 |
| `Throw/Arg/Range/Range.cs` | 2 | `OutOfRange` x2 |
| `Throw/Arg/Range/ZeroOrNegative.cs` | 2 | `NotZeroOrNegative` x2 |
| `Throw/Arg/Range/ZeroOrPositive.cs` | 2 | `NotZeroOrPositive` x2 |
| `Throw/Arg/String.cs` | 24 | `IsNullOrEmpty` x2, `IsNullOrWhiteSpace` x2, `DoesNotMatch` x4, `InvalidLength` x4, `TooShort` x4, `TooLong` x4, `InvalidEmail` x4 |
| `Throw/Arg/Type.cs` | 12 | `IsNotAssignableTo` x4, `IsNotAssignableFrom` x4, `DoesNotHaveAttribute` x4 |
| `Throw/State.cs` | 12 | `IsDisposed` x4, `IsNotInitialized` x4, `IsNotMutable` x4 |
| `Throw/Throw.cs` | 16 | `Unreachable` x4, `NotImplemented` x4, `NotSupported` x4, `InvalidOperation` x4 |

Declaration-only partial classes, internal annotation/cache files, generated Polyfill code, and global usings have no direct tests. The .NET Standard fallback implementations are outside the approved runtime scope.
