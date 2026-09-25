using System.Numerics;
using static CheckAndThrow.Throw.Arg;

namespace CheckAndThrow;

public static partial class Check
{
    public static partial class Arg
    {
        /// <summary>
        /// Checks if the argument is positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not positive.</exception>
        public static int Positive(
            int value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value <= 0)
                NotPositive(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not positive.</exception>
        public static double Positive(
            double value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (double.IsNaN(value) || double.IsPositiveInfinity(value) || value <= 0)
                NotPositive(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not positive.</exception>
        public static long Positive(
            long value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value <= 0)
                NotPositive(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not positive.</exception>
        public static float Positive(
            float value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (float.IsNaN(value) || float.IsPositiveInfinity(value) || value <= 0)
                NotPositive(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not positive.</exception>
        public static uint Positive(
            uint value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value <= 0)
                NotPositive(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not positive.</exception>
        public static ulong Positive(
            ulong value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value <= 0)
                NotPositive(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not positive.</exception>
        public static decimal Positive(
            decimal value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value <= 0)
                NotPositive(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not positive.</exception>
        public static byte Positive(
            byte value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value <= 0)
                NotPositive(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not positive.</exception>
        public static sbyte Positive(
            sbyte value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value <= 0)
                NotPositive(value, paramName);

            return value;
        }

        /// <summary>
        /// Checks if the argument is positive.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not positive.</exception>
        public static nint Positive(
            nint value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (value <= 0)
                NotPositive(value, paramName);

            return value;
        }

        /// <summary>Checks if the argument is positive or positive infinity.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is positive or positive infinity.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not positive or positive infinity.</exception>
        public static double PositiveOrInfinity(
            double value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (double.IsNaN(value) || value <= 0)
                NotPositive(value, paramName);

            return value;
        }

        /// <summary>Checks if the argument is positive or positive infinity.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is positive or positive infinity.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not positive or positive infinity.</exception>
        public static float PositiveOrInfinity(
            float value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (float.IsNaN(value) || value <= 0)
                NotPositive(value, paramName);

            return value;
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Checks if the argument is positive.
        /// </summary>
        /// <typeparam name="TNumber">The type of the number value.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is positive.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not positive.</exception>
        public static TNumber Positive<TNumber>(
            TNumber value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
            where TNumber : INumberBase<TNumber>
        {
            if (
                TNumber.IsNaN(value)
                || TNumber.IsPositiveInfinity(value)
                || TNumber.IsZero(value)
                || !TNumber.IsPositive(value)
            )
                NotPositive(value, paramName);

            return value;
        }

        /// <summary>Checks if the argument is positive or positive infinity.</summary>
        /// <typeparam name="TNumber">The type of the number value.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is positive or positive infinity.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not positive or positive infinity.</exception>
        public static TNumber PositiveOrInfinity<TNumber>(
            TNumber value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
            where TNumber : INumberBase<TNumber>
        {
            if (
                TNumber.IsNaN(value)
                || TNumber.IsZero(value)
                || (!TNumber.IsPositive(value) && !TNumber.IsPositiveInfinity(value))
            )
                NotPositive(value, paramName);

            return value;
        }
#endif
    }
}
