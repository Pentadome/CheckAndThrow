namespace CheckAndThrow;

public static partial class Throw
{
    public static partial class Arg
    {
        const string NotInPastMessage = "Argument must be in the past.";
        const string NotInFutureMessage = "Argument must be in the future.";
        const string NotInPastWithInfoMessage =
            "Argument must be in the past, but was {0}. Current time is {1}.";
        const string NotInFutureWithInfoMessage =
            "Argument must be in the future, but was {0}. Current time is {1}.";
        const string NotLaterThanMessage = "Argument must be later than the comparison value.";
        const string NotEarlierThanMessage = "Argument must be earlier than the comparison value.";
        const string NotLaterThanWithInfoMessage = "Argument must be later than {0}, but was {1}.";
        const string NotEarlierThanWithInfoMessage =
            "Argument must be earlier than {0}, but was {1}.";

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the DateTime argument is not in the past.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the DateTime is not in the past.</exception>
        [DoesNotReturn]
        public static void IsNotInPast([InvokerParameterName] string paramName) =>
            throw new ArgumentException(NotInPastMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the DateTime argument is not in the past.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the DateTime is not in the past.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsNotInPast<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(NotInPastMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the DateTime argument is not in the past.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="actual">The actual value that was checked.</param>
        /// <param name="currentTime">The current time at the moment of the check.</param>
        /// <exception cref="ArgumentException">Thrown because the DateTime is not in the past.</exception>
        [DoesNotReturn]
        public static void IsNotInPast(
            [InvokerParameterName] string paramName,
            object actual,
            object currentTime
        ) =>
            throw new ArgumentException(
                string.Format(NotInPastWithInfoMessage, actual, currentTime),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the DateTime argument is not in the past.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="actual">The actual value that was checked.</param>
        /// <param name="currentTime">The current time at the moment of the check.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the DateTime is not in the past.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsNotInPast<TFakeReturn>(
            [InvokerParameterName] string paramName,
            object actual,
            object currentTime
        ) =>
            throw new ArgumentException(
                string.Format(NotInPastWithInfoMessage, actual, currentTime),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the DateTime argument is not in the future.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the DateTime is not in the future.</exception>
        [DoesNotReturn]
        public static void IsNotInFuture([InvokerParameterName] string paramName) =>
            throw new ArgumentException(NotInFutureMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the DateTime argument is not in the future.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the DateTime is not in the future.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsNotInFuture<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(NotInFutureMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the DateTime argument is not in the future.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="actual">The actual value that was checked.</param>
        /// <param name="currentTime">The current time at the moment of the check.</param>
        /// <exception cref="ArgumentException">Thrown because the DateTime is not in the future.</exception>
        [DoesNotReturn]
        public static void IsNotInFuture(
            [InvokerParameterName] string paramName,
            object actual,
            object currentTime
        ) =>
            throw new ArgumentException(
                string.Format(NotInFutureWithInfoMessage, actual, currentTime),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the DateTime argument is not in the future.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="actual">The actual value that was checked.</param>
        /// <param name="currentTime">The current time at the moment of the check.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the DateTime is not in the future.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsNotInFuture<TFakeReturn>(
            [InvokerParameterName] string paramName,
            object actual,
            object currentTime
        ) =>
            throw new ArgumentException(
                string.Format(NotInFutureWithInfoMessage, actual, currentTime),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the DateTime argument is not later than the comparison value.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the DateTime is not later than the comparison value.</exception>
        [DoesNotReturn]
        public static void IsNotLaterThan([InvokerParameterName] string paramName) =>
            throw new ArgumentException(NotLaterThanMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the DateTime argument is not later than the comparison value.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the DateTime is not later than the comparison value.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsNotLaterThan<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(NotLaterThanMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the DateTime argument is not later than the comparison value.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="comparison">The comparison value that was used.</param>
        /// <param name="actual">The actual value that was checked.</param>
        /// <exception cref="ArgumentException">Thrown because the DateTime is not later than the comparison value.</exception>
        [DoesNotReturn]
        public static void IsNotLaterThan(
            [InvokerParameterName] string paramName,
            object comparison,
            object actual
        ) =>
            throw new ArgumentException(
                string.Format(NotLaterThanWithInfoMessage, comparison, actual),
                paramName
            );

        // ... existing code ...
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the DateTime argument is not later than the comparison value.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="comparison">The comparison value that was used.</param>
        /// <param name="actual">The actual value that was checked.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the DateTime is not later than the comparison value.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsNotLaterThan<TFakeReturn>(
            [InvokerParameterName] string paramName,
            object? comparison,
            object? actual
        ) =>
            throw new ArgumentException(
                string.Format(NotLaterThanWithInfoMessage, comparison, actual),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the DateTime argument is not earlier than the comparison value.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the DateTime is not earlier than the comparison value.</exception>
        [DoesNotReturn]
        public static void IsNotEarlierThan([InvokerParameterName] string paramName) =>
            throw new ArgumentException(NotEarlierThanMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the DateTime argument is not earlier than the comparison value.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the DateTime is not earlier than the comparison value.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsNotEarlierThan<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(NotEarlierThanMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the DateTime argument is not earlier than the comparison value.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="comparison">The comparison value that was used.</param>
        /// <param name="actual">The actual value that was checked.</param>
        /// <exception cref="ArgumentException">Thrown because the DateTime is not earlier than the comparison value.</exception>
        [DoesNotReturn]
        public static void IsNotEarlierThan(
            [InvokerParameterName] string paramName,
            object? comparison,
            object? actual
        ) =>
            throw new ArgumentException(
                string.Format(NotEarlierThanWithInfoMessage, comparison, actual),
                paramName
            );

        // ... existing code ...
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the DateTime argument is not earlier than the comparison value.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="comparison">The comparison value that was used.</param>
        /// <param name="actual">The actual value that was checked.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the DateTime is not earlier than the comparison value.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsNotEarlierThan<TFakeReturn>(
            [InvokerParameterName] string paramName,
            object? comparison,
            object? actual
        ) =>
            throw new ArgumentException(
                string.Format(NotEarlierThanWithInfoMessage, comparison, actual),
                paramName
            );
    }
}
