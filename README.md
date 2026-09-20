# CheckAndThrow

A high-performance .NET library for defensive argument validation and exception handling. Provides two complementary APIs: **`Check`** for performance-critical validation and **`Throw`** for detailed exception reporting.

```bash
dotnet add package CheckAndThrow
```

## Overview

**CheckAndThrow** implements a philosophy that separates concerns when validating inputs and handling errors:

- **`Check.*`** methods prioritize **performance**. They validate conditions as efficiently as possible with minimal overhead, making them suitable for hot paths and performance-sensitive code.
- **`Throw.*`** methods prioritize **detailed diagnostics**. They sacrifice speed for comprehensive error information, generating rich exception messages that help developers diagnose issues quickly.

This design reflects the principle that **exceptions should be exceptional**. Since exceptions are not expected to be thrown frequently in production code, the extra overhead of detailed error reporting is justified, your code's hot paths stay fast through `Check`, while debugging becomes effortless through `Throw`.

## Features

### Check API

Fast argument validation with minimal overhead:

- `Check.Arg.*` — Validate single arguments (null checks, type checks, range validation, collection checks)
- `Check.Args.*` — Validate multiple arguments together
- `Check.State.*` — Validate application state conditions
- `Check.Expression.*` — Evaluate expressions for state validation

Perfect for:

- Entry points of hot methods
- Loops and frequently-called code
- Performance-critical paths

### Throw API

Detailed exception reporting for when things go wrong:

- `Throw.Arg.*` — Throw rich ArgumentExceptions with context
- `Throw.State.*` — Throw InvalidOperationExceptions with diagnostic info
- `Throw.Unreachable()`, `Throw.NotImplemented()`, `Throw.NotSupported()` — Handle unreachable code paths

Perfect for:

- Helper methods and validation logic
- Clear separation of error reporting from validation
- Comprehensive debugging information in exceptions

## Design Philosophy

**Key insight:** `Check` methods call `Throw` methods underneath when validation fails.

This means:

- **Success path** → Fast: `Check` validates with minimal overhead
- **Failure path** → Detailed: `Check` delegates to `Throw` for rich error reporting

```csharp
// Check validates efficiently; throws only when validation fails
public void ProcessData(int[] data)
{
    Check.Arg.NotNull(data); // Returns immediately if valid
    // ... process data ...
}

// You can also use Throw directly for explicit error handling
public User GetUser(UserType userType)
{
    return userType switch
    {
        UserType.Admin => GetAdmin(),
        UserType.Customer => GetCustomer(),
        // Generic return type allows usage in switch expressions
        _ => Throw.Arg.InvalidEnumValue<User>(typeof(UserType), userType)
    };
}
```

**Why this design?** Since exceptions interrupt execution and are handled at a higher level anyway, the performance cost of detailed error messages is negligible. This architecture ensures:

- Your application remains responsive in the common case (success)
- Rich debugging information available in the exceptional case (failure)
- Clean, readable validation code everywhere

## Breaking API change

`Is` prefixes were removed from guard and throw helpers (for example, `Check.Arg.IsNotNullOrEmpty` is now `Check.Arg.NotNullOrEmpty`, and `Throw.Arg.IsNull` is now `Throw.Arg.Null`). This is a source and binary breaking change: rename calls and recompile consumers. Compatibility aliases are not provided.

## Installation

Add CheckAndThrow to your project via NuGet:

```bash
dotnet add package CheckAndThrow
```

## IDE guard actions

The package includes Roslyn Quick Actions by default. Place the caret on a supported parameter and open your IDE's Quick Actions menu (for example, **Ctrl+.** in Visual Studio or **Alt+Enter** in Rider). Choose **Add Check.Arg.NotNull guard** for a reference parameter, or expand **Add range guard clause** for a numeric parameter.

The action supports block-bodied methods and constructors whose parameters are non-nullable reference types or unannotated generic type parameters. It does not appear for explicitly nullable parameters such as `string?` or `T?`, value types, `out` parameters, expression-bodied members, lambdas, local functions, or primary constructors. It inserts a single `global::CheckAndThrow.Check.Arg.NotNull(parameter);` statement and does not report build warnings.

For declarations with 2–16 eligible parameters, **Add Check.Args.NotNull guard** adds one `global::CheckAndThrow.Check.Args.NotNull(...)` guard for all eligible parameters. It safely converts replaceable top-level `Check.Arg.NotNull` and `Check.Args.NotNull` guards; when a prior individual guard's return value was assigned or declared, its value is preserved through the returned tuple. Explicitly nullable and other excluded parameters are not included. The bulk action is unavailable when fewer than two or more than 16 parameters are eligible, or when an existing guard has an unsupported result-consuming shape.

Disable all bundled CheckAndThrow IDE actions for a project or repository without removing the package:

```xml
<PropertyGroup>
  <CheckAndThrowEnableAnalyzers>false</CheckAndThrowEnableAnalyzers>
</PropertyGroup>
```

For numeric parameters, **Add range guard clause** is hidden by default and offers existing `Check.Arg` sign checks: reject negative (`ZeroOrPositive`), reject negative and zero (`Positive`), reject positive (`ZeroOrNegative`), or reject positive and zero (`Negative`). For floating-point and generic numeric types, it also offers the corresponding infinity-permitting checks when the referenced CheckAndThrow API supports them. Existing `Negative`, `Positive`, `ZeroOrNegative`, `ZeroOrPositive`, infinity, and `InRange` guards suppress the action. The action supports methods and constructors with executable bodies, inserts a top-level guard when needed, and uses the guard's returned value for a safe direct first use.

To disable only an action, set `dotnet_diagnostic.CAT0001.severity = none` or `dotnet_diagnostic.CAT0003.severity = none` in `.editorconfig`. Roslyn analyzer/code-fix support must be enabled in your IDE; supported hosts include Visual Studio, JetBrains Rider, and VS Code with the Microsoft C# extension.

## API Documentation

[Full API can be found here](https://pentadome.github.io/CheckAndThrow/)

## DotNet Targets

- DotNet standard 2.0
- DotNet standard 2.1
- DotNet 8.0
- DotNet 10.0

## Contributing

Contributions are welcome! Please:

1. Open an issue to discuss your idea
2. Submit pull requests with clear descriptions and appropriate tests
3. Ensure all public members have XML documentation comments

## License

Licensed under the MIT License. See LICENSE file for details.
