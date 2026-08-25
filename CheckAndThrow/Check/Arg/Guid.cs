namespace CheckAndThrow;

public static partial class Check
{
    public static partial class Arg
    {
        /// <summary>
        /// Ensures that the specified Guid is not empty.
        /// </summary>
        /// <param name="argument">The Guid to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original Guid if it is not empty.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is an empty Guid.</exception>
        public static Guid IsNotGuidEmpty(
            Guid argument,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            if (argument == Guid.Empty)
            {
                Throw.Arg.IsGuidEmpty(paramName);
            }
            return argument;
        }
    }
}
