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
// Check validates efficiently; delegates to Throw on failure
public void ProcessData(int[] data)
{
    Check.Arg.NotNull(data);  // Returns immediately if valid
                               // Calls Throw.Arg.IsNull internally if null
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
        _ => Throw.Arg.IsNotDefined<User>(typeof(UserType), userType)
    };
}
```

**Why this design?** Since exceptions interrupt execution and are handled at a higher level anyway, the performance cost of detailed error messages is negligible. This architecture ensures:

- Your application remains responsive in the common case (success)
- Rich debugging information available in the exceptional case (failure)
- Clean, readable validation code everywhere

## Installation

Add CheckAndThrow to your project via NuGet:

```bash
dotnet add package CheckAndThrow
```

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
