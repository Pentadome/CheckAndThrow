namespace CheckAndThrow;

public static partial class Check
{
    public static partial class Arg
    {
        /// <summary>Ensures that the specified value is not NaN.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original value if it is not NaN.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is NaN.</exception>
        public static double NotNaN(
            double value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (double.IsNaN(value))
                Throw.Arg.NaN(paramName);

            return value;
        }

        /// <summary>Ensures that the specified value is not NaN.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original value if it is not NaN.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is NaN.</exception>
        public static float NotNaN(
            float value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (float.IsNaN(value))
                Throw.Arg.NaN(paramName);

            return value;
        }

        /// <summary>Ensures that the specified value is not infinite.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original value if it is not infinite.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is infinite.</exception>
        public static double NotInfinity(
            double value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (double.IsInfinity(value))
                Throw.Arg.Infinity(paramName);

            return value;
        }

        /// <summary>Ensures that the specified value is not infinite.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original value if it is not infinite.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is infinite.</exception>
        public static float NotInfinity(
            float value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (float.IsInfinity(value))
                Throw.Arg.Infinity(paramName);

            return value;
        }

        /// <summary>Ensures that the specified value is finite.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original value if it is finite.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is NaN or infinite.</exception>
        public static double RealNumber(
            double value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (double.IsNaN(value))
                Throw.Arg.NaN(paramName);
            if (double.IsInfinity(value))
                Throw.Arg.Infinity(paramName);

            return value;
        }

        /// <summary>Ensures that the specified value is finite.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original value if it is finite.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is NaN or infinite.</exception>
        public static float RealNumber(
            float value,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
        {
            if (float.IsNaN(value))
                Throw.Arg.NaN(paramName);
            if (float.IsInfinity(value))
                Throw.Arg.Infinity(paramName);

            return value;
        }
    }
}
