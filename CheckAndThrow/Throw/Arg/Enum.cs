using System.ComponentModel;

namespace CheckAndThrow;

public static partial class Throw
{
    public static partial class Arg
    {
        [StackTraceHidden]
        static InvalidEnumArgumentException CreateInvalidEnumException(
            Type enumType,
            int enumValue,
            string paramName
        ) =>
            new(paramName, enumValue, enumType)
            {
                Data = { { nameof(enumType), enumType }, { nameof(enumValue), enumValue } },
            };

        [StackTraceHidden]
        static ArgumentException CreateInvalidEnumException(
            Type enumType,
            string enumValue,
            string paramName
        ) =>
            new($"\"{enumValue}\" is not a valid value for enum \"{enumType.FullName}\"", paramName)
            {
                Data = { { nameof(enumType), enumType }, { nameof(enumValue), enumValue } },
            };

        [StackTraceHidden]
        static ArgumentException CreateMissesFlagsException(
            Type enumType,
            string flags,
            string paramName
        ) =>
            new($"\"{paramName}\" misses the enum flag(s) \"{flags}\", but is required.", paramName)
            {
                Data = { { nameof(enumType), enumType }, { nameof(flags), flags } },
            };

        [StackTraceHidden]
        static ArgumentException CreateMissesAFlagException(
            Type enumType,
            string flags,
            string paramName
        ) =>
            new($"\"{paramName}\" requires any of these flags: \"{flags}", paramName)
            {
                Data = { { nameof(enumType), enumType }, { nameof(flags), flags } },
            };

        /// <summary>
        /// Throws an <see cref="InvalidEnumArgumentException"/> because the enum value is not defined.
        /// </summary>
        /// <param name="enumType">The type of the enum.</param>
        /// <param name="enumValue">The value of the enum.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown because the enum value is not defined.</exception>
        [DoesNotReturn]
        public static void IsInvalidEnumValue(
            Type enumType,
            int enumValue,
            [CallerArgumentExpression(nameof(enumValue)), InvokerParameterName]
                string paramName = ""
        ) => throw CreateInvalidEnumException(enumType, enumValue, paramName);

        /// <summary>
        /// Throws an <see cref="InvalidEnumArgumentException"/> because the enum value is not defined.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="enumType">The type of the enum.</param>
        /// <param name="enumValue">The value of the enum.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown because the enum value is not defined.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsInvalidEnumValue<TFakeReturn>(
            Type enumType,
            int enumValue,
            [CallerArgumentExpression(nameof(enumValue)), InvokerParameterName]
                string paramName = ""
        ) => throw CreateInvalidEnumException(enumType, enumValue, paramName);

        /// <summary>
        /// Throws an <see cref="InvalidEnumArgumentException"/> because the enum value is not defined.
        /// </summary>
        /// <param name="enumType">The type of the enum.</param>
        /// <param name="enumValue">The value of the enum.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown because the enum value is not defined.</exception>
        [DoesNotReturn]
        public static void IsInvalidEnumValue(
            Type enumType,
            Enum enumValue,
            [CallerArgumentExpression(nameof(enumValue)), InvokerParameterName]
                string paramName = ""
        ) =>
            throw CreateInvalidEnumException(
                enumType,
                ((IConvertible)enumValue).ToInt32(null),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="InvalidEnumArgumentException"/> because the enum value is not defined.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="enumType">The type of the enum.</param>
        /// <param name="enumValue">The value of the enum.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown because the enum value is not defined.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsInvalidEnumValue<TFakeReturn>(
            Type enumType,
            Enum enumValue,
            [CallerArgumentExpression(nameof(enumValue)), InvokerParameterName]
                string paramName = ""
        ) =>
            throw CreateInvalidEnumException(
                enumType,
                ((IConvertible)enumValue).ToInt32(null),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the enum value is not defined.
        /// </summary>
        /// <param name="enumType">The type of the enum.</param>
        /// <param name="enumValue">The value of the enum.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the enum value is not defined.</exception>
        [DoesNotReturn]
        public static void IsInvalidEnumValue(
            Type enumType,
            string enumValue,
            [CallerArgumentExpression(nameof(enumValue)), InvokerParameterName]
                string paramName = ""
        ) => throw CreateInvalidEnumException(enumType, enumValue, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the enum value is not defined.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="enumType">The type of the enum.</param>
        /// <param name="enumValue">The value of the enum.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the enum value is not defined.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsInvalidEnumValue<TFakeReturn>(
            Type enumType,
            string enumValue,
            [CallerArgumentExpression(nameof(enumValue)), InvokerParameterName]
                string paramName = ""
        ) => throw CreateInvalidEnumException(enumType, enumValue, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the argument misses any of the required flags.
        /// </summary>
        /// <param name="enumType">The type of the enum.</param>
        /// <param name="flags">The required flags.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the argument misses any of the required flags.</exception>
        [DoesNotReturn]
        public static void MissesAnyOfTheFlags(
            Type enumType,
            string flags,
            [InvokerParameterName] string paramName
        ) => throw CreateMissesFlagsException(enumType, flags, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the argument misses any of the required flags.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="enumType">The type of the enum.</param>
        /// <param name="flags">The required flags.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the argument misses any of the required flags.</exception>
        [DoesNotReturn]
        public static TFakeReturn MissesAnyOfTheFlags<TFakeReturn>(
            Type enumType,
            string flags,
            [InvokerParameterName] string paramName
        ) => throw CreateMissesFlagsException(enumType, flags, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the argument misses all of the required flags.
        /// </summary>
        /// <param name="enumType">The type of the enum.</param>
        /// <param name="flags">The required flags.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the argument misses all of the required flags.</exception>
        [DoesNotReturn]
        public static void MissesAllFlags(
            Type enumType,
            string flags,
            [InvokerParameterName] string paramName
        ) => throw CreateMissesAFlagException(enumType, flags, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the argument misses all of the required flags.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="enumType">The type of the enum.</param>
        /// <param name="flags">The required flags.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the argument misses all of the required flags.</exception>
        [DoesNotReturn]
        public static TFakeReturn MissesAllFlags<TFakeReturn>(
            Type enumType,
            string flags,
            [InvokerParameterName] string paramName
        ) => throw CreateMissesAFlagException(enumType, flags, paramName);
    }
}
