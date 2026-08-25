namespace CheckAndThrow;

public static partial class Check
{
    public static partial class Arg
    {
        /// <summary>
        /// Ensures that the specified string is not null or empty.
        /// </summary>
        /// <param name="argument">The string to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original string if it is not null or empty.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is empty.</exception>
        public static string IsNotNullOrEmpty(
            string argument,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            if (NotNull(argument, paramName) == "")
            {
                Throw.Arg.IsNullOrEmpty(paramName);
            }
            return argument;
        }

        /// <summary>
        /// Ensures that the specified string is not null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="argument">The string to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original string if it is not null, empty, or whitespace.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is empty or whitespace.</exception>
        public static string IsNotNullOrWhiteSpace(
            string argument,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            if (string.IsNullOrWhiteSpace(NotNull(argument, paramName)))
            {
                Throw.Arg.IsNullOrWhiteSpace(paramName);
            }
            return argument;
        }

        /// <summary>
        /// Ensures that the specified string matches the required pattern.
        /// </summary>
        /// <param name="argument">The string to check.</param>
        /// <param name="pattern">The regular expression pattern.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original string if it matches the pattern.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> or <paramref name="pattern"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> does not match the <paramref name="pattern"/>.</exception>
        public static string Matches(
            string argument,
            string pattern,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            NotNull(argument, paramName);
            NotNull(pattern);

            if (!Regex.IsMatch(argument, pattern))
            {
                Throw.Arg.DoesNotMatch(paramName, pattern, argument);
            }

            return argument;
        }

        /// <summary>
        /// Ensures that the specified string has the required length.
        /// </summary>
        /// <param name="argument">The string to check.</param>
        /// <param name="length">The required length.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original string if it has the required length.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> does not have the required length.</exception>
        public static string HasLength(
            string argument,
            int length,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            if (NotNull(argument, paramName).Length != length)
            {
                Throw.Arg.InvalidLength(paramName, length, argument.Length);
            }

            return argument;
        }

        /// <summary>
        /// Ensures that the specified string has at least the required minimum length.
        /// </summary>
        /// <param name="argument">The string to check.</param>
        /// <param name="minLength">The minimum required length.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original string if it has at least the required minimum length.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is shorter than <paramref name="minLength"/>.</exception>
        public static string HasMinLength(
            string argument,
            int minLength,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            if (NotNull(argument, paramName).Length < minLength)
            {
                Throw.Arg.TooShort(paramName, minLength, argument.Length);
            }

            return argument;
        }

        /// <summary>
        /// Ensures that the specified string has at most the required maximum length.
        /// </summary>
        /// <param name="argument">The string to check.</param>
        /// <param name="maxLength">The maximum required length.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original string if it has at most the required maximum length.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is longer than <paramref name="maxLength"/>.</exception>
        public static string HasMaxLength(
            string argument,
            int maxLength,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            if (NotNull(argument, paramName).Length > maxLength)
            {
                Throw.Arg.TooLong(paramName, maxLength, argument.Length);
            }

            return argument;
        }

        /// <summary>
        /// Ensures that the specified string is a valid email address.
        /// </summary>
        /// <param name="argument">The string to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original string if it is a valid email address.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is not a valid email address.</exception>
        public static string IsEmail(
            string argument,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            NotNull(argument, paramName);

            var emailRegex =
#if NET7_0_OR_GREATER
            EmailRegex();
#else
            EmailRegex;
#endif

            // Simple email regex
            if (!emailRegex.IsMatch(argument))
            {
                Throw.Arg.InvalidEmail(paramName, argument);
            }

            return argument;
        }

#if NET7_0_OR_GREATER
        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        private static partial Regex EmailRegex();
#else
        static readonly Regex EmailRegex = new(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled
        );
#endif
    }
}
