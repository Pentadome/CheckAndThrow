using static CheckAndThrow.Throw.Arg;

namespace CheckAndThrow;

public static partial class Check
{
    public static partial class Arg
    {
        /// <summary>
        /// Checks if the argument is negative.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is negative.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not negative.</exception>
        public static int Negative(
            int value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value >= 0)
                NotNegative(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is negative.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is negative.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not negative.</exception>
        public static double Negative(
            double value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value >= 0)
                NotNegative(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is negative.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is negative.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not negative.</exception>
        public static long Negative(
            long value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value >= 0)
                NotNegative(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is negative.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is negative.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not negative.</exception>
        public static float Negative(
            float value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value >= 0)
                NotNegative(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is negative.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is negative.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not negative.</exception>
        public static decimal Negative(
            decimal value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value >= 0)
                NotNegative(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is negative.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is negative.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not negative.</exception>
        public static sbyte Negative(
            sbyte value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value >= 0)
                NotNegative(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is negative.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is negative.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not negative.</exception>
        public static nint Negative(
            nint value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value >= 0)
                NotNegative(value, paramName);

            return value;
        }
    }
}
