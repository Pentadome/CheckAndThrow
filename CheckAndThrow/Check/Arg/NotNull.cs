namespace CheckAndThrow;

public static partial class Check
{
    public static partial class Arg
    {
        /// <summary>
        /// Ensures that the specified argument is not null.
        /// </summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="argument">The argument to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original argument if it is not null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is null.</exception>
        [return: NotNull]
        public static T NotNull<T>(
            [NotNull, NoEnumeration] T argument,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            return argument ?? throw new ArgumentNullException(paramName);
        }
    }
}
