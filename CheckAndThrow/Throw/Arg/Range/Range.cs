namespace CheckAndThrow;

public static partial class Throw
{
    public static partial class Arg
    {
        [StackTraceHidden]
        static ArgumentOutOfRangeException CreateOutOfRangeException(
            object value,
            object? min,
            object? max,
            string paramName,
            string minArgumentExpression,
            string maxArgumentExpression
        )
        {
            var minExpr =
                string.IsNullOrWhiteSpace(minArgumentExpression)
                || min?.ToString() == minArgumentExpression
                    ? ""
                    : $"({minArgumentExpression})";

            var maxExpr =
                string.IsNullOrWhiteSpace(maxArgumentExpression)
                || max?.ToString() == maxArgumentExpression
                    ? ""
                    : $"({maxArgumentExpression})";

            var message =
                $"{paramName} must be {min}{minExpr}, {max}{maxExpr} or any value in between, but was {value}.";

            return new(paramName, value, message)
            {
                Data =
                {
                    { nameof(value), value },
                    { nameof(min), min },
                    { nameof(max), max },
                    { nameof(minArgumentExpression), minArgumentExpression },
                    { nameof(maxArgumentExpression), maxArgumentExpression },
                },
            };
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> indicating that the argument was outside the specified range.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="minArgumentExpression">The string representation of the minimum value expression.</param>
        /// <param name="maxArgumentExpression">The string representation of the maximum value expression.</param>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        public static void OutOfRange(
            object value,
            object min,
            object max,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = "",
            [CallerArgumentExpression(nameof(min))] string minArgumentExpression = "",
            [CallerArgumentExpression(nameof(max))] string maxArgumentExpression = ""
        ) =>
            throw CreateOutOfRangeException(
                value,
                min,
                max,
                paramName,
                minArgumentExpression,
                maxArgumentExpression
            );

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> indicating that the argument was outside the specified range.
        /// </summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="minArgumentExpression">The string representation of the minimum value expression.</param>
        /// <param name="maxArgumentExpression">The string representation of the maximum value expression.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn OutOfRange<TFakeReturn>(
            object value,
            object min,
            object max,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = "",
            [CallerArgumentExpression(nameof(min))] string minArgumentExpression = "",
            [CallerArgumentExpression(nameof(max))] string maxArgumentExpression = ""
        ) =>
            throw CreateOutOfRangeException(
                value,
                min,
                max,
                paramName,
                minArgumentExpression,
                maxArgumentExpression
            );
    }
}
