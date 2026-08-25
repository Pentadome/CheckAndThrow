namespace CheckAndThrow;

/// <summary>
/// Provides methods to throw exceptions.
/// </summary>
[DebuggerStepThrough]
public static partial class Throw
{
    /// <summary>
    /// Throws an <see cref="UnreachableException"/>.
    /// </summary>
    /// <exception cref="UnreachableException">Always thrown.</exception>
    [DoesNotReturn]
    public static void Unreachable() => throw new UnreachableException();

    /// <summary>
    /// Throws an <see cref="UnreachableException"/> with the specified message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <exception cref="UnreachableException">Always thrown.</exception>
    [DoesNotReturn]
    public static void Unreachable(string message) => throw new UnreachableException(message);

    /// <summary>
    /// Throws an <see cref="UnreachableException"/>.
    /// </summary>
    /// <typeparam name="TFakeReturn">The type to return, which is never actually returned.</typeparam>
    /// <returns>This method never returns.</returns>
    /// <exception cref="UnreachableException">Always thrown.</exception>
    [DoesNotReturn]
    public static TFakeReturn Unreachable<TFakeReturn>() => throw new UnreachableException();

    /// <summary>
    /// Throws an <see cref="UnreachableException"/> with the specified message.
    /// </summary>
    /// <typeparam name="TFakeReturn">The type to return, which is never actually returned.</typeparam>
    /// <param name="message">The message that describes the error.</param>
    /// <returns>This method never returns.</returns>
    /// <exception cref="UnreachableException">Always thrown.</exception>
    [DoesNotReturn]
    public static TFakeReturn Unreachable<TFakeReturn>(string message) =>
        throw new UnreachableException(message);

    /// <summary>
    /// Throws a <see cref="NotImplementedException"/>.
    /// </summary>
    /// <exception cref="NotImplementedException">Always thrown.</exception>
    [DoesNotReturn]
    public static void NotImplemented() => throw new NotImplementedException();

    /// <summary>
    /// Throws a <see cref="NotImplementedException"/> with the specified message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <exception cref="NotImplementedException">Always thrown.</exception>
    [DoesNotReturn]
    public static void NotImplemented(string message) => throw new NotImplementedException(message);

    /// <summary>
    /// Throws a <see cref="NotImplementedException"/>.
    /// </summary>
    /// <typeparam name="TFakeReturn">The type to return, which is never actually returned.</typeparam>
    /// <returns>This method never returns.</returns>
    /// <exception cref="NotImplementedException">Always thrown.</exception>
    [DoesNotReturn]
    public static TFakeReturn NotImplemented<TFakeReturn>() => throw new NotImplementedException();

    /// <summary>
    /// Throws a <see cref="NotImplementedException"/> with the specified message.
    /// </summary>
    /// <typeparam name="TFakeReturn">The type to return, which is never actually returned.</typeparam>
    /// <param name="message">The message that describes the error.</param>
    /// <returns>This method never returns.</returns>
    /// <exception cref="NotImplementedException">Always thrown.</exception>
    [DoesNotReturn]
    public static TFakeReturn NotImplemented<TFakeReturn>(string message) =>
        throw new NotImplementedException(message);

    /// <summary>
    /// Throws a <see cref="NotSupportedException"/>.
    /// </summary>
    /// <exception cref="NotSupportedException">Always thrown.</exception>
    [DoesNotReturn]
    public static void NotSupported() => throw new NotSupportedException();

    /// <summary>
    /// Throws a <see cref="NotSupportedException"/> with the specified message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <exception cref="NotSupportedException">Always thrown.</exception>
    [DoesNotReturn]
    public static void NotSupported(string message) => throw new NotSupportedException(message);

    /// <summary>
    /// Throws a <see cref="NotSupportedException"/>.
    /// </summary>
    /// <typeparam name="TFakeReturn">The type to return, which is never actually returned.</typeparam>
    /// <returns>This method never returns.</returns>
    /// <exception cref="NotSupportedException">Always thrown.</exception>
    [DoesNotReturn]
    public static TFakeReturn NotSupported<TFakeReturn>() => throw new NotSupportedException();

    /// <summary>
    /// Throws a <see cref="NotSupportedException"/> with the specified message.
    /// </summary>
    /// <typeparam name="TFakeReturn">The type to return, which is never actually returned.</typeparam>
    /// <param name="message">The message that describes the error.</param>
    /// <returns>This method never returns.</returns>
    /// <exception cref="NotSupportedException">Always thrown.</exception>
    [DoesNotReturn]
    public static TFakeReturn NotSupported<TFakeReturn>(string message) =>
        throw new NotSupportedException(message);

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Always thrown.</exception>
    [DoesNotReturn]
    public static void InvalidOperation() => throw new InvalidOperationException();

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> with the specified message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <exception cref="InvalidOperationException">Always thrown.</exception>
    [DoesNotReturn]
    public static void InvalidOperation(string message) =>
        throw new InvalidOperationException(message);

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <typeparam name="TFakeReturn">The type to return, which is never actually returned.</typeparam>
    /// <returns>This method never returns.</returns>
    /// <exception cref="InvalidOperationException">Always thrown.</exception>
    [DoesNotReturn]
    public static TFakeReturn InvalidOperation<TFakeReturn>() =>
        throw new InvalidOperationException();

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> with the specified message.
    /// </summary>
    /// <typeparam name="TFakeReturn">The type to return, which is never actually returned.</typeparam>
    /// <param name="message">The message that describes the error.</param>
    /// <returns>This method never returns.</returns>
    /// <exception cref="InvalidOperationException">Always thrown.</exception>
    [DoesNotReturn]
    public static TFakeReturn InvalidOperation<TFakeReturn>(string message) =>
        throw new InvalidOperationException(message);
}
