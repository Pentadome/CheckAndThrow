namespace CheckAndThrow;

public static partial class Throw
{
    public static partial class Arg
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> because the argument is null.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown because the argument is null.</exception>
        [DoesNotReturn]
        public static void IsNull([InvokerParameterName] string paramName) =>
            throw new ArgumentNullException(paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> because the argument is null.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentNullException">Thrown because the argument is null.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsNull<TFakeReturn>([InvokerParameterName] string paramName) =>
            throw new ArgumentNullException(paramName);
    }
}
