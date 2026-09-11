using System.Numerics;
using static CheckAndThrow.Throw.Arg;

namespace CheckAndThrow;

public static partial class Check
{
    public static partial class Arg
    {
        /// <summary>
        /// Checks if the argument is zero or positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is zero or positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not zero or positive.</exception>
        public static int ZeroOrPositive(
            int value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value < 0)
                NotZeroOrPositive(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is zero or positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is zero or positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not zero or positive.</exception>
        public static double ZeroOrPositive(
            double value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (double.IsNaN(value) || double.IsPositiveInfinity(value) || value < 0)
                NotZeroOrPositive(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is zero or positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is zero or positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not zero or positive.</exception>
        public static long ZeroOrPositive(
            long value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value < 0)
                NotZeroOrPositive(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is zero or positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is zero or positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not zero or positive.</exception>
        public static float ZeroOrPositive(
            float value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (float.IsNaN(value) || float.IsPositiveInfinity(value) || value < 0)
                NotZeroOrPositive(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is zero or positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is zero or positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not zero or positive.</exception>
        public static decimal ZeroOrPositive(
            decimal value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value < 0)
                NotZeroOrPositive(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is zero or positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is zero or positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not zero or positive.</exception>
        public static sbyte ZeroOrPositive(
            sbyte value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value < 0)
                NotZeroOrPositive(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is zero or positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is zero or positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not zero or positive.</exception>
        public static nint ZeroOrPositive(
            nint value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value < 0)
                NotZeroOrPositive(value, paramName);

            return value;
        }
        #if NET7_0_OR_GREATER
        /// <summary>
        /// Checks if the argument is zero or positive.
        /// </summary>
        /// <typeparam name="TNumber">The type of the number value.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is zero or positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not zero or positive.</exception>
        public static TNumber ZeroOrPositive<TNumber>(
            TNumber value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
            where TNumber : INumberBase<TNumber>
        {
            if (TNumber.IsNaN(value) || TNumber.IsPositiveInfinity(value) || (!TNumber.IsZero(value) && !TNumber.IsPositive(value)))
                NotZeroOrPositive(value, paramName);

            return value;
        }
        #endif
    }
}
