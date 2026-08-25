namespace CheckAndThrow;

public static partial class Check
{
    public static partial class Arg
    {
        /// <summary>
        /// Ensures that the specified DateTime is in the past.
        /// </summary>
        /// <param name="argument">The DateTime to check.</param>
        /// <param name="timeProvider">The <see cref="TimeProvider"/> to use. Uses <see cref="Check.DefaultTimeProvider"/> by default.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original DateTime if it is in the past.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is not in the past.</exception>
        public static DateTime IsInPast(
            DateTime argument,
            TimeProvider? timeProvider = null,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            timeProvider ??= DefaultTimeProvider;

            var now = timeProvider.GetLocalNow();
            if (argument >= timeProvider.GetLocalNow())
            {
                Throw.Arg.IsNotInPast(paramName, argument, now);
            }
            return argument;
        }

        /// <summary>
        /// Ensures that the specified DateTimeOffset is in the past.
        /// </summary>
        /// <param name="argument">The DateTimeOffset to check.</param>
        /// <param name="timeProvider">The <see cref="TimeProvider"/> to use. Uses <see cref="Check.DefaultTimeProvider"/> by default.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original DateTimeOffset if it is in the past.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is not in the past.</exception>
        public static DateTimeOffset IsInPast(
            DateTimeOffset argument,
            TimeProvider? timeProvider = null,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            timeProvider ??= DefaultTimeProvider;
            var now = timeProvider.GetLocalNow();
            if (argument >= now)
            {
                Throw.Arg.IsNotInPast(paramName, argument, now);
            }
            return argument;
        }

        /// <summary>
        /// Ensures that the specified DateTime is in the past (Utc).
        /// </summary>
        /// <param name="argument">The DateTime to check.</param>
        /// <param name="timeProvider">The <see cref="TimeProvider"/> to use. Uses <see cref="Check.DefaultTimeProvider"/> by default.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original DateTime if it is in the past.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is not in the past.</exception>
        public static DateTime IsInPastUtc(
            DateTime argument,
            TimeProvider? timeProvider = null,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            timeProvider ??= DefaultTimeProvider;
            var now = timeProvider.GetUtcNow();
            if (argument >= now)
            {
                Throw.Arg.IsNotInPast(paramName, argument, now);
            }
            return argument;
        }

        /// <summary>
        /// Ensures that the specified DateTime is in the future.
        /// </summary>
        /// <param name="argument">The DateTime to check.</param>
        /// <param name="timeProvider">The <see cref="TimeProvider"/> to use. Uses <see cref="Check.DefaultTimeProvider"/> by default.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original DateTime if it is in the future.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is not in the future.</exception>
        public static DateTime IsInFuture(
            DateTime argument,
            TimeProvider? timeProvider = null,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            timeProvider ??= DefaultTimeProvider;
            var now = timeProvider.GetLocalNow();
            if (argument <= now)
            {
                Throw.Arg.IsNotInFuture(paramName, argument, now);
            }
            return argument;
        }

        /// <summary>
        /// Ensures that the specified DateTimeOffset is in the future.
        /// </summary>
        /// <param name="argument">The DateTimeOffset to check.</param>
        /// <param name="timeProvider">The <see cref="TimeProvider"/> to use. Uses <see cref="Check.DefaultTimeProvider"/> by default.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original DateTimeOffset if it is in the future.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is not in the future.</exception>
        public static DateTimeOffset IsInFuture(
            DateTimeOffset argument,
            TimeProvider? timeProvider = null,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            timeProvider ??= DefaultTimeProvider;
            var now = timeProvider.GetLocalNow();
            if (argument <= now)
            {
                Throw.Arg.IsNotInFuture(paramName, argument, now);
            }
            return argument;
        }

        /// <summary>
        /// Ensures that the specified DateTime is in the future (Utc).
        /// </summary>
        /// <param name="argument">The DateTime to check.</param>
        /// <param name="timeProvider">The <see cref="TimeProvider"/> to use. Uses <see cref="Check.DefaultTimeProvider"/> by default.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original DateTime if it is in the future.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is not in the future.</exception>
        public static DateTime IsInFutureUtc(
            DateTime argument,
            TimeProvider? timeProvider = null,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            timeProvider ??= DefaultTimeProvider;
            var now = timeProvider.GetUtcNow();
            if (argument <= now)
            {
                Throw.Arg.IsNotInFuture(paramName, argument, now);
            }
            return argument;
        }

        /// <summary>
        /// Ensures that the specified DateTime is later than the comparison DateTime.
        /// </summary>
        /// <param name="argument">The DateTime to check.</param>
        /// <param name="comparison">The DateTime to compare against.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original DateTime if it is later than the comparison value.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is not later than <paramref name="comparison"/>.</exception>
        public static DateTime IsLaterThan(
            DateTime argument,
            DateTime comparison,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            if (argument <= comparison)
            {
                Throw.Arg.IsNotLaterThan(paramName, comparison, argument);
            }
            return argument;
        }

        /// <summary>
        /// Ensures that the specified DateTimeOffset is later than the comparison DateTimeOffset.
        /// </summary>
        /// <param name="argument">The DateTimeOffset to check.</param>
        /// <param name="comparison">The DateTimeOffset to compare against.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original DateTimeOffset if it is later than the comparison value.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is not later than <paramref name="comparison"/>.</exception>
        public static DateTimeOffset IsLaterThan(
            DateTimeOffset argument,
            DateTimeOffset comparison,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            if (argument <= comparison)
            {
                Throw.Arg.IsNotLaterThan(paramName, comparison, argument);
            }
            return argument;
        }

        /// <summary>
        /// Ensures that the specified DateTime is earlier than the comparison DateTime.
        /// </summary>
        /// <param name="argument">The DateTime to check.</param>
        /// <param name="comparison">The DateTime to compare against.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original DateTime if it is earlier than the comparison value.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is not earlier than <paramref name="comparison"/>.</exception>
        public static DateTime IsEarlierThan(
            DateTime argument,
            DateTime comparison,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            if (argument >= comparison)
            {
                Throw.Arg.IsNotEarlierThan(paramName, comparison, argument);
            }
            return argument;
        }

        /// <summary>
        /// Ensures that the specified DateTimeOffset is earlier than the comparison DateTimeOffset.
        /// </summary>
        /// <param name="argument">The DateTimeOffset to check.</param>
        /// <param name="comparison">The DateTimeOffset to compare against.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original DateTimeOffset if it is earlier than the comparison value.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is not earlier than <paramref name="comparison"/>.</exception>
        public static DateTimeOffset IsEarlierThan(
            DateTimeOffset argument,
            DateTimeOffset comparison,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            if (argument >= comparison)
            {
                Throw.Arg.IsNotEarlierThan(paramName, comparison, argument);
            }
            return argument;
        }
    }
}
