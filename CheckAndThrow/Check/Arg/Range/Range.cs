using static CheckAndThrow.Throw.Arg;

namespace CheckAndThrow;

public static partial class Check
{
    public static partial class Arg
    {
        /// <summary>
        /// Checks if the argument is within a range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="minArgumentExpression">The expression of the minimum value.</param>
        /// <param name="maxArgumentExpression">The expression of the maximum value.</param>
        /// <returns>The value if it is within the range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is out of range.</exception>
        public static int InRange(
            int value,
            int min = int.MinValue,
            int max = int.MaxValue,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = "",
            [CallerArgumentExpression(nameof(min))] string minArgumentExpression = "",
            [CallerArgumentExpression(nameof(max))] string maxArgumentExpression = ""
        )
        {
            if (value < min || value > max)
                OutOfRange(
                    value,
                    min,
                    max,
                    paramName,
                    minArgumentExpression,
                    maxArgumentExpression
                );

            return value;
        }

        /// <summary>
        /// Checks if the argument is within a range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="minArgumentExpression">The expression of the minimum value.</param>
        /// <param name="maxArgumentExpression">The expression of the maximum value.</param>
        /// <returns>The value if it is within the range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is out of range.</exception>
        public static double InRange(
            double value,
            double min = double.MinValue,
            double max = double.MaxValue,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = "",
            [CallerArgumentExpression(nameof(min))] string minArgumentExpression = "",
            [CallerArgumentExpression(nameof(max))] string maxArgumentExpression = ""
        )
        {
            if (value < min || value > max)
                OutOfRange(
                    value,
                    min,
                    max,
                    paramName,
                    minArgumentExpression,
                    maxArgumentExpression
                );

            return value;
        }

        /// <summary>
        /// Checks if the argument is within a range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="minArgumentExpression">The expression of the minimum value.</param>
        /// <param name="maxArgumentExpression">The expression of the maximum value.</param>
        /// <returns>The value if it is within the range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is out of range.</exception>
        public static long InRange(
            long value,
            long min = long.MinValue,
            long max = long.MaxValue,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = "",
            [CallerArgumentExpression(nameof(min))] string minArgumentExpression = "",
            [CallerArgumentExpression(nameof(max))] string maxArgumentExpression = ""
        )
        {
            if (value < min || value > max)
                OutOfRange(
                    value,
                    min,
                    max,
                    paramName,
                    minArgumentExpression,
                    maxArgumentExpression
                );

            return value;
        }

        /// <summary>
        /// Checks if the argument is within a range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="minArgumentExpression">The expression of the minimum value.</param>
        /// <param name="maxArgumentExpression">The expression of the maximum value.</param>
        /// <returns>The value if it is within the range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is out of range.</exception>
        public static float InRange(
            float value,
            float min = float.MinValue,
            float max = float.MaxValue,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = "",
            [CallerArgumentExpression(nameof(min))] string minArgumentExpression = "",
            [CallerArgumentExpression(nameof(max))] string maxArgumentExpression = ""
        )
        {
            if (value < min || value > max)
                OutOfRange(
                    value,
                    min,
                    max,
                    paramName,
                    minArgumentExpression,
                    maxArgumentExpression
                );

            return value;
        }

        /// <summary>
        /// Checks if the argument is within a range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="minArgumentExpression">The expression of the minimum value.</param>
        /// <param name="maxArgumentExpression">The expression of the maximum value.</param>
        /// <returns>The value if it is within the range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is out of range.</exception>
        public static uint InRange(
            uint value,
            uint min = uint.MinValue,
            uint max = uint.MaxValue,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = "",
            [CallerArgumentExpression(nameof(min))] string minArgumentExpression = "",
            [CallerArgumentExpression(nameof(max))] string maxArgumentExpression = ""
        )
        {
            if (value < min || value > max)
                OutOfRange(
                    value,
                    min,
                    max,
                    paramName,
                    minArgumentExpression,
                    maxArgumentExpression
                );

            return value;
        }

        /// <summary>
        /// Checks if the argument is within a range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="minArgumentExpression">The expression of the minimum value.</param>
        /// <param name="maxArgumentExpression">The expression of the maximum value.</param>
        /// <returns>The value if it is within the range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is out of range.</exception>
        public static ulong InRange(
            ulong value,
            ulong min = ulong.MinValue,
            ulong max = ulong.MaxValue,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = "",
            [CallerArgumentExpression(nameof(min))] string minArgumentExpression = "",
            [CallerArgumentExpression(nameof(max))] string maxArgumentExpression = ""
        )
        {
            if (value < min || value > max)
                OutOfRange(
                    value,
                    min,
                    max,
                    paramName,
                    minArgumentExpression,
                    maxArgumentExpression
                );

            return value;
        }

        /// <summary>
        /// Checks if the argument is within a range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="minArgumentExpression">The expression of the minimum value.</param>
        /// <param name="maxArgumentExpression">The expression of the maximum value.</param>
        /// <returns>The value if it is within the range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is out of range.</exception>
        public static decimal InRange(
            decimal value,
            decimal min = decimal.MinValue,
            decimal max = decimal.MaxValue,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = "",
            [CallerArgumentExpression(nameof(min))] string minArgumentExpression = "",
            [CallerArgumentExpression(nameof(max))] string maxArgumentExpression = ""
        )
        {
            if (value < min || value > max)
                OutOfRange(
                    value,
                    min,
                    max,
                    paramName,
                    minArgumentExpression,
                    maxArgumentExpression
                );

            return value;
        }

        /// <summary>
        /// Checks if the argument is within a range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="minArgumentExpression">The expression of the minimum value.</param>
        /// <param name="maxArgumentExpression">The expression of the maximum value.</param>
        /// <returns>The value if it is within the range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is out of range.</exception>
        public static byte InRange(
            byte value,
            byte min = byte.MinValue,
            byte max = byte.MaxValue,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = "",
            [CallerArgumentExpression(nameof(min))] string minArgumentExpression = "",
            [CallerArgumentExpression(nameof(max))] string maxArgumentExpression = ""
        )
        {
            if (value < min || value > max)
                OutOfRange(
                    value,
                    min,
                    max,
                    paramName,
                    minArgumentExpression,
                    maxArgumentExpression
                );

            return value;
        }

        /// <summary>
        /// Checks if the argument is within a range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="minArgumentExpression">The expression of the minimum value.</param>
        /// <param name="maxArgumentExpression">The expression of the maximum value.</param>
        /// <returns>The value if it is within the range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is out of range.</exception>
        public static sbyte InRange(
            sbyte value,
            sbyte min = sbyte.MinValue,
            sbyte max = sbyte.MaxValue,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = "",
            [CallerArgumentExpression(nameof(min))] string minArgumentExpression = "",
            [CallerArgumentExpression(nameof(max))] string maxArgumentExpression = ""
        )
        {
            if (value < min || value > max)
                OutOfRange(
                    value,
                    min,
                    max,
                    paramName,
                    minArgumentExpression,
                    maxArgumentExpression
                );

            return value;
        }

        /// <summary>
        /// Checks if the argument is within a range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="minArgumentExpression">The expression of the minimum value.</param>
        /// <param name="maxArgumentExpression">The expression of the maximum value.</param>
        /// <returns>The value if it is within the range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is out of range.</exception>
        public static nint InRange(
            nint value,
            nint min,
            nint max,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = "",
            [CallerArgumentExpression(nameof(min))] string minArgumentExpression = "",
            [CallerArgumentExpression(nameof(max))] string maxArgumentExpression = ""
        )
        {
            if (value < min || value > max)
                OutOfRange(
                    value,
                    min,
                    max,
                    paramName,
                    minArgumentExpression,
                    maxArgumentExpression
                );

            return value;
        }
    }
}
