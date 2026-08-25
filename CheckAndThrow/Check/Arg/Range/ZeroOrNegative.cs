using static CheckAndThrow.Throw.Arg;

namespace CheckAndThrow;

public static partial class Check
{
    public static partial class Arg
    {
        /// <summary>
        /// Checks if the argument is zero or negative.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is zero or negative.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not zero or negative.</exception>
        public static int ZeroOrNegative(
            int value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value > 0)
                NotZeroOrNegative(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is zero or negative.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is zero or negative.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not zero or negative.</exception>
        public static double ZeroOrNegative(
            double value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value > 0)
                NotZeroOrNegative(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is zero or negative.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is zero or negative.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not zero or negative.</exception>
        public static long ZeroOrNegative(
            long value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value > 0)
                NotZeroOrNegative(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is zero or negative.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is zero or negative.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not zero or negative.</exception>
        public static float ZeroOrNegative(
            float value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value > 0)
                NotZeroOrNegative(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is zero or negative.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is zero or negative.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not zero or negative.</exception>
        public static decimal ZeroOrNegative(
            decimal value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value > 0)
                NotZeroOrNegative(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is zero or negative.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is zero or negative.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not zero or negative.</exception>
        public static sbyte ZeroOrNegative(
            sbyte value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value > 0)
                NotZeroOrNegative(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is zero or negative.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is zero or negative.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not zero or negative.</exception>
        public static nint ZeroOrNegative(
            nint value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value > 0)
                NotZeroOrNegative(value, paramName);

            return value;
        }
    }
}
