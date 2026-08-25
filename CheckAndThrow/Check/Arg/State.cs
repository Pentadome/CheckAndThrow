namespace CheckAndThrow;

public static partial class Check
{
    public static partial class Arg
    {
        /// <summary>
        /// Ensures that the specified argument satisfies the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="argument">The argument to check.</param>
        /// <param name="predicate">The predicate to satisfy.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="predicateString">The string representation of the predicate.</param>
        /// <returns>The original argument if it satisfies the predicate.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> does not satisfy the <paramref name="predicate"/>.</exception>
        public static T HasValidState<T>(
            [NoEnumeration] T argument,
            [InstantHandle] Func<T, bool> predicate,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName]
                string paramName = "",
            [CallerArgumentExpression(nameof(predicate))] string predicateString = ""
        )
        {
            if (!predicate(argument))
            {
                throw new ArgumentException(
                    $"The argument \"{paramName}\" is in an unexpected state.\nFailed predicate: \"{predicateString}\"",
                    paramName
                );
            }
            return argument;
        }

        /// <summary>
        /// Ensures that the specified argument satisfies the specified predicate with the provided additional argument.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument to check.</typeparam>
        /// <typeparam name="TPredicateArg">The type of the additional argument for the predicate.</typeparam>
        /// <param name="argument">The argument to check.</param>
        /// <param name="predicateArg">The additional argument to pass to the predicate.</param>
        /// <param name="predicate">The predicate to satisfy.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="predicateString">The string representation of the predicate.</param>
        /// <returns>The original argument if it satisfies the predicate.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> does not satisfy the <paramref name="predicate"/>.</exception>
        public static TArg HasValidState<TArg, TPredicateArg>(
            [NoEnumeration] TArg argument,
            [NoEnumeration] TPredicateArg predicateArg,
            [InstantHandle, RequireStaticDelegate] Func<TArg, TPredicateArg, bool> predicate,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName]
                string paramName = "",
            [CallerArgumentExpression(nameof(predicate))] string predicateString = ""
        )
        {
            if (!predicate(argument, predicateArg))
            {
                throw new ArgumentException(
                    $"The argument \"{paramName}\" is in an unexpected state.\nFailed predicate: \"{predicateString}\"",
                    paramName
                );
            }
            return argument;
        }
    }
}
