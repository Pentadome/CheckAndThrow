using System.Numerics;
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
            if (double.IsNaN(value) || double.IsNegativeInfinity(value) || value >= 0)
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
            if (float.IsNaN(value) || float.IsNegativeInfinity(value) || value >= 0)
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

        /// <summary>Checks if the argument is negative or negative infinity.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is negative or negative infinity.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not negative or negative infinity.</exception>
        public static double NegativeOrNegativeInfinity(
            double value,
            [CallerArgumentExpression(nameof(value))] string paramName = ""
        )
        {
            if (double.IsNaN(value) || value >= 0)
                NotNegative(value, paramName);

            return value;
        }

        /// <summary>Checks if the argument is negative or negative infinity.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is negative or negative infinity.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not negative or negative infinity.</exception>
        public static float NegativeOrNegativeInfinity(
            float value,
            [CallerArgumentExpression(nameof(value))] string paramName = ""
        )
        {
            if (float.IsNaN(value) || value >= 0)
                NotNegative(value, paramName);

            return value;
        }

        #if NET7_0_OR_GREATER
        /// <summary>
        /// Checks if the argument is negative.
        /// </summary>
        /// <typeparam name="TNumber">The type of the number value.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is negative.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not negative.</exception>
        public static TNumber Negative<TNumber>(TNumber value, [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = "")
            where TNumber: INumberBase<TNumber>
        {
               if (TNumber.IsNaN(value) || TNumber.IsNegativeInfinity(value) || TNumber.IsZero(value) || !TNumber.IsNegative(value))
               {
                   NotNegative(value, paramName);
               }
               
               return value;
        }

        /// <summary>Checks if the argument is negative or negative infinity.</summary>
        /// <typeparam name="TNumber">The type of the number value.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value if it is negative or negative infinity.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not negative or negative infinity.</exception>
        public static TNumber NegativeOrNegativeInfinity<TNumber>(TNumber value, [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = "")
            where TNumber: INumberBase<TNumber>
        {
               if (TNumber.IsNaN(value) || TNumber.IsZero(value) || (!TNumber.IsNegative(value) && !TNumber.IsNegativeInfinity(value)))
               {
                   NotNegative(value, paramName);
               }

               return value;
        }
        #endif
    }
}
