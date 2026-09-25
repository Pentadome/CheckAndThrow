namespace CheckAndThrow;

public static partial class Throw
{
    public static partial class Arg
    {
        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> when an argument is NaN.</summary>
        /// <param name="paramName">The name of the NaN parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        public static void NaN([InvokerParameterName] string paramName) =>
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must not be NaN.");

        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> when an argument is NaN.</summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="paramName">The name of the NaN parameter.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn NaN<TFakeReturn>([InvokerParameterName] string paramName) =>
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must not be NaN.");

        /// <summary>Throws when the specified argument is NaN.</summary>
        /// <param name="value">The invalid value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        public static void NaN(object value, [InvokerParameterName] string paramName) =>
            throw new ArgumentOutOfRangeException(
                paramName,
                value,
                $"{paramName} must not be NaN."
            );

        /// <summary>Throws when the specified argument is NaN.</summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="value">The invalid value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn NaN<TFakeReturn>(
            object value,
            [InvokerParameterName] string paramName
        ) =>
            throw new ArgumentOutOfRangeException(
                paramName,
                value,
                $"{paramName} must not be NaN."
            );

        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> when an argument is infinite.</summary>
        /// <param name="paramName">The name of the infinite parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        public static void Infinity([InvokerParameterName] string paramName) =>
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must not be infinite.");

        /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> when an argument is infinite.</summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="paramName">The name of the infinite parameter.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn Infinity<TFakeReturn>([InvokerParameterName] string paramName) =>
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must not be infinite.");

        /// <summary>Throws when the specified argument is infinite.</summary>
        /// <param name="value">The invalid value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        public static void Infinity(object value, [InvokerParameterName] string paramName) =>
            throw new ArgumentOutOfRangeException(
                paramName,
                value,
                $"{paramName} must not be infinite."
            );

        /// <summary>Throws when the specified argument is infinite.</summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="value">The invalid value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn Infinity<TFakeReturn>(
            object value,
            [InvokerParameterName] string paramName
        ) =>
            throw new ArgumentOutOfRangeException(
                paramName,
                value,
                $"{paramName} must not be infinite."
            );
    }
}
