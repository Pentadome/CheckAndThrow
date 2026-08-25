namespace CheckAndThrow;

public static partial class Throw
{
    public static partial class Arg
    {
        const string GuidEmptyMessage = "Argument cannot be an empty Guid.";

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the Guid argument is empty.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the Guid is empty.</exception>
        [DoesNotReturn]
        public static void IsGuidEmpty([InvokerParameterName] string paramName) =>
            throw new ArgumentException(GuidEmptyMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the Guid argument is empty.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the Guid is empty.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsGuidEmpty<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(GuidEmptyMessage, paramName);
    }
}
