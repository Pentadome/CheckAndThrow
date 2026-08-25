namespace CheckAndThrow;

/// <summary>
/// Provides methods to check conditions and arguments.
/// </summary>
[DebuggerStepThrough]
public static partial class Check
{
    /// <summary>
    /// Gets or sets the default <see cref="TimeProvider"/> used for time-based checks.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when setting a <see langword="null"/> value.</exception>
    public static TimeProvider DefaultTimeProvider
    {
        get;
        set => field = Arg.NotNull(value);
    } = TimeProvider.System;
}
