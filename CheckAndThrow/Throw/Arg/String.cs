namespace CheckAndThrow;

public static partial class Throw
{
    public static partial class Arg
    {
        const string NotNullOrEmptyMessage = "Argument cannot be null or empty.";

        const string NotNullOrWhiteSpaceMessage =
            "Argument cannot be null or empty and can not only contain white space characters.";

        const string DoesNotMatchMessage = "Argument does not match the required pattern.";
        const string DoesNotMatchMessageWithInfo =
            "Argument does not match the required pattern. Pattern: \"{0}\". Value: \"{1}\".";
        const string InvalidLengthMessage = "Argument has an invalid length.";
        const string InvalidLengthMessageWithInfo =
            "Argument has an invalid length. Expected length: {0}. Actual length: {1}";
        const string TooShortMessage = "Argument is too short.";
        const string TooShortMessageWithInfo =
            "Argument is too short. Minimum length: {0}. Actual length: {1}";
        const string TooLongMessage = "Argument is too long.";
        const string TooLongMessageWithInfo =
            "Argument is too long. Maximum length: {0}. Actual length: {1}";
        const string InvalidEmailMessage = "Argument is not a valid email address.";
        const string InvalidEmailMessageWithInfo =
            "Argument is not a valid email address. Value: \"{0}\".";

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument is null or empty.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the string is null or empty.</exception>
        [DoesNotReturn]
        public static void IsNullOrEmpty([InvokerParameterName] string paramName) =>
            throw new ArgumentException(NotNullOrEmptyMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument is null or empty.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the string is null or empty.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsNullOrEmpty<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(NotNullOrEmptyMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument is null or white space.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the string is null or white space.</exception>
        [DoesNotReturn]
        public static void IsNullOrWhiteSpace([InvokerParameterName] string paramName) =>
            throw new ArgumentException(NotNullOrWhiteSpaceMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument is null or white space.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the string is null or white space.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsNullOrWhiteSpace<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(NotNullOrWhiteSpaceMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument does not match the required pattern.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the string does not match the pattern.</exception>
        [DoesNotReturn]
        public static void DoesNotMatch([InvokerParameterName] string paramName) =>
            throw new ArgumentException(DoesNotMatchMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument does not match the required pattern.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the string does not match the pattern.</exception>
        [DoesNotReturn]
        public static TFakeReturn DoesNotMatch<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(DoesNotMatchMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument does not match the required pattern.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="pattern">The required pattern.</param>
        /// <param name="value">The string value.</param>
        /// <exception cref="ArgumentException">Thrown because the string does not match the pattern.</exception>
        [DoesNotReturn]
        public static void DoesNotMatch(
            [InvokerParameterName] string paramName,
            string pattern,
            string value
        ) =>
            throw new ArgumentException(
                string.Format(DoesNotMatchMessageWithInfo, pattern, value),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument does not match the required pattern.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="pattern">The required pattern.</param>
        /// <param name="value">The string value.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the string does not match the pattern.</exception>
        [DoesNotReturn]
        public static TFakeReturn DoesNotMatch<TFakeReturn>(
            [InvokerParameterName] string paramName,
            string pattern,
            string value
        ) =>
            throw new ArgumentException(
                string.Format(DoesNotMatchMessageWithInfo, pattern, value),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument has an invalid length.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the string has an invalid length.</exception>
        [DoesNotReturn]
        public static void InvalidLength([InvokerParameterName] string paramName) =>
            throw new ArgumentException(InvalidLengthMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument has an invalid length.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the string has an invalid length.</exception>
        [DoesNotReturn]
        public static TFakeReturn InvalidLength<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(InvalidLengthMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument has an invalid length.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="expectedLength">The expected length of the string.</param>
        /// <param name="actualLength">The actual length of the string.</param>
        /// <exception cref="ArgumentException">Thrown because the string has an invalid length.</exception>
        [DoesNotReturn]
        public static void InvalidLength(
            [InvokerParameterName] string paramName,
            object expectedLength,
            object actualLength
        ) =>
            throw new ArgumentException(
                string.Format(InvalidLengthMessageWithInfo, expectedLength, actualLength),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument has an invalid length.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="expectedLength">The expected length of the string.</param>
        /// <param name="actualLength">The actual length of the string.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the string has an invalid length.</exception>
        [DoesNotReturn]
        public static TFakeReturn InvalidLength<TFakeReturn>(
            [InvokerParameterName] string paramName,
            object expectedLength,
            object actualLength
        ) =>
            throw new ArgumentException(
                string.Format(InvalidLengthMessageWithInfo, expectedLength, actualLength),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument is too short.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the string is too short.</exception>
        [DoesNotReturn]
        public static void TooShort([InvokerParameterName] string paramName) =>
            throw new ArgumentException(TooShortMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument is too short.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the string is too short.</exception>
        [DoesNotReturn]
        public static TFakeReturn TooShort<TFakeReturn>([InvokerParameterName] string paramName) =>
            throw new ArgumentException(TooShortMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument is too short.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="minimumLength">The minimum allowed length.</param>
        /// <param name="expectedLength">The expected length.</param>
        /// <exception cref="ArgumentException">Thrown because the string is too short.</exception>
        [DoesNotReturn]
        public static void TooShort(
            [InvokerParameterName] string paramName,
            object minimumLength,
            object expectedLength
        ) =>
            throw new ArgumentException(
                string.Format(TooShortMessageWithInfo, minimumLength, expectedLength),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument is too short.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="minimumLength">The minimum allowed length.</param>
        /// <param name="expectedLength">The expected length.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the string is too short.</exception>
        [DoesNotReturn]
        public static TFakeReturn TooShort<TFakeReturn>(
            [InvokerParameterName] string paramName,
            object minimumLength,
            object expectedLength
        ) =>
            throw new ArgumentException(
                string.Format(TooShortMessageWithInfo, minimumLength, expectedLength),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument is too long.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the string is too long.</exception>
        [DoesNotReturn]
        public static void TooLong([InvokerParameterName] string paramName) =>
            throw new ArgumentException(TooLongMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument is too long.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the string is too long.</exception>
        [DoesNotReturn]
        public static TFakeReturn TooLong<TFakeReturn>([InvokerParameterName] string paramName) =>
            throw new ArgumentException(TooLongMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument is too long.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="maximumLength">The maximum allowed length.</param>
        /// <param name="actualLength">The actual length of the string.</param>
        /// <exception cref="ArgumentException">Thrown because the string is too long.</exception>
        [DoesNotReturn]
        public static void TooLong(
            [InvokerParameterName] string paramName,
            object maximumLength,
            object actualLength
        ) =>
            throw new ArgumentException(
                string.Format(TooLongMessageWithInfo, maximumLength, actualLength),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument is too long.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="maximumLength">The maximum allowed length.</param>
        /// <param name="actualLength">The actual length of the string.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the string is too long.</exception>
        [DoesNotReturn]
        public static TFakeReturn TooLong<TFakeReturn>(
            [InvokerParameterName] string paramName,
            object maximumLength,
            object actualLength
        ) =>
            throw new ArgumentException(
                string.Format(TooLongMessageWithInfo, maximumLength, actualLength),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument is not a valid email address.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the string is not a valid email address.</exception>
        [DoesNotReturn]
        public static void InvalidEmail([InvokerParameterName] string paramName) =>
            throw new ArgumentException(InvalidEmailMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument is not a valid email address.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the string is not a valid email address.</exception>
        [DoesNotReturn]
        public static TFakeReturn InvalidEmail<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(InvalidEmailMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument is not a valid email address.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="value">The invalid email address.</param>
        /// <exception cref="ArgumentException">Thrown because the string is not a valid email address.</exception>
        [DoesNotReturn]
        public static void InvalidEmail([InvokerParameterName] string paramName, string value) =>
            throw new ArgumentException(
                string.Format(InvalidEmailMessageWithInfo, value),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the string argument is not a valid email address.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="value">The invalid email address.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the string is not a valid email address.</exception>
        [DoesNotReturn]
        public static TFakeReturn InvalidEmail<TFakeReturn>(
            [InvokerParameterName] string paramName,
            string value
        ) =>
            throw new ArgumentException(
                string.Format(InvalidEmailMessageWithInfo, value),
                paramName
            );
    }
}
