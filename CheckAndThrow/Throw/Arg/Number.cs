namespace CheckAndThrow;

public static partial class Throw
{
    public static partial class Arg
    {
        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> when an argument is NaN.</summary>
        /// <param name="paramName">The name of the NaN parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        public static void IsNaN([InvokerParameterName] string paramName) =>
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must not be NaN.");

        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> when an argument is NaN.</summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="paramName">The name of the NaN parameter.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsNaN<TFakeReturn>([InvokerParameterName] string paramName) =>
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must not be NaN.");

        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> when an argument is infinite.</summary>
        /// <param name="paramName">The name of the infinite parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        public static void IsInfinity([InvokerParameterName] string paramName) =>
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must not be infinite.");

        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> when an argument is infinite.</summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="paramName">The name of the infinite parameter.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsInfinity<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentOutOfRangeException(paramName, $"{paramName} must not be infinite.");
    }
}
