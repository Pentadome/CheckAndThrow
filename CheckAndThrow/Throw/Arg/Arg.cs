namespace CheckAndThrow;

public static partial class Throw
{
    /// <summary>
    /// Provides methods to throw exceptions for single arguments.
    /// </summary>
    public static partial class Arg
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> for the specified parameter.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">The exception that is thrown.</exception>
        [DoesNotReturn]
        public static void Exception([InvokerParameterName] string paramName)
        {
            throw new ArgumentException("Argument is invalid.", paramName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> for the specified parameter.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">The exception that is thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn Exception<TFakeReturn>([InvokerParameterName] string paramName)
        {
            throw new ArgumentException("Argument is invalid.", paramName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> for the specified parameter.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="message">The message for the exception.</param>
        /// <exception cref="ArgumentException">The exception that is thrown.</exception>
        [DoesNotReturn]
        public static void Exception([InvokerParameterName] string paramName, string message)
        {
            throw new ArgumentException(message, paramName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> for the specified parameter.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="message">The message for the exception.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">The exception that is thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn Exception<TFakeReturn>(
            [InvokerParameterName] string paramName,
            string message
        )
        {
            throw new ArgumentException(message, paramName);
        }
    }
}
