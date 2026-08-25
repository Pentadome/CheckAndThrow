namespace CheckAndThrow;

public static partial class Throw
{
    public static partial class Arg
    {
        [StackTraceHidden]
        static ArgumentOutOfRangeException CreateNotNegativeException(
            object value,
            string paramName
        ) => new(paramName, value, $"{paramName} should have a negative value, but was ${value}.");

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> indicating that the argument was not negative.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        public static void NotNegative(
            object value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        ) => throw CreateNotNegativeException(value, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> indicating that the argument was not negative.
        /// </summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn NotNegative<TFakeReturn>(
            object value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        ) => throw CreateNotNegativeException(value, paramName);
    }
}
