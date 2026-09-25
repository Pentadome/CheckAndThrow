using System.ComponentModel;
using static CheckAndThrow.Throw.Arg;

namespace CheckAndThrow;

public static partial class Check
{
    public static partial class Arg
    {
        /// <summary>
        /// Ensures that the specified enum value is defined in the enum type.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="enumValue">The enum value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original enum value if it is defined.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when <paramref name="enumValue"/> is not defined in <typeparamref name="T"/>.</exception>
        public static T ValidEnumValue<T>(
            T enumValue,
            [CallerArgumentExpression(nameof(enumValue)), InvokerParameterName]
                string paramName = ""
        )
            where T : struct, Enum, IConvertible
        {
            return EnumCache<T>.Values.ContainsValue(enumValue)
                ? enumValue
                : InvalidEnumValue<T>(typeof(T), enumValue, paramName);
        }

        /// <summary>
        /// Ensures that the specified string represents a defined enum value in the enum type.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="enumValue">The string representation of the enum value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The enum value corresponding to the specified string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="enumValue"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="enumValue"/> is not defined in <typeparamref name="T"/>.</exception>
        public static T ValidEnumValueName<T>(
            [NotNull] string? enumValue,
            [CallerArgumentExpression(nameof(enumValue)), InvokerParameterName]
                string paramName = ""
        )
            where T : struct, Enum, IConvertible
        {
            return EnumCache<T>.Values.TryGetValue(NotNull(enumValue, paramName), out var value)
                ? value
                : InvalidEnumValue<T>(typeof(T), enumValue, paramName);
        }

        /// <summary>
        /// Ensures that the specified string represents a defined enum value in the enum type, ignoring case.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="enumValue">The string representation of the enum value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The enum value corresponding to the specified string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="enumValue"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="enumValue"/> is not defined in <typeparamref name="T"/>.</exception>
        public static T ValidEnumValueNameIgnoreCase<T>(
            [NotNull] string? enumValue,
            [CallerArgumentExpression(nameof(enumValue)), InvokerParameterName]
                string paramName = ""
        )
            where T : struct, Enum, IConvertible
        {
            return EnumCache<T>.ValuesIgnoreCase.TryGetValue(
                NotNull(enumValue, paramName),
                out var value
            )
                ? value
                : InvalidEnumValue<T>(typeof(T), enumValue, paramName);
        }

        /// <summary>
        /// Ensures that the specified enum value has all of the specified flags.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="value">The enum value to check.</param>
        /// <param name="flags">The flags that the value must have.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original enum value if it has all specified flags.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> does not have all of the specified <paramref name="flags"/>.</exception>
        public static T HasAllFlags<T>(
            T value,
            T flags,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
            where T : struct, Enum, IConvertible
        {
            return value.HasFlag(flags)
                ? value
                : MissesAnyOfTheFlags<T>(typeof(T), flags.ToString(), value, paramName);
        }

        /// <summary>
        /// Ensures that the specified enum value has any of the specified flags.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="value">The enum value to check.</param>
        /// <param name="flags">The flags that the value must have at least one of.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original enum value if it has any of the specified flags.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> does not have any of the specified <paramref name="flags"/>.</exception>
        public static T HasAnyOfFlags<T>(
            T value,
            T flags,
            [CallerArgumentExpression(nameof(value)), InvokerParameterName] string paramName = ""
        )
            where T : struct, Enum, IConvertible
        {
            if (
                EnumCache<T>.UnderlyingType == typeof(int)
                || EnumCache<T>.UnderlyingType == typeof(uint)
            )
            {
                var valueInt = Unsafe.As<T, int>(ref value);
                var flagsInt = Unsafe.As<T, int>(ref flags);

                if (flagsInt == 0 || (valueInt & flagsInt) != 0)
                    return value;
            }
            else if (
                EnumCache<T>.UnderlyingType == typeof(long)
                || EnumCache<T>.UnderlyingType == typeof(ulong)
            )
            {
                var valueLong = Unsafe.As<T, long>(ref value);
                var flagsLong = Unsafe.As<T, long>(ref flags);

                if (flagsLong == 0 || (valueLong & flagsLong) != 0)
                    return value;
            }
            else if (EnumCache<T>.UnderlyingType == typeof(short))
            {
                var valueShort = Unsafe.As<T, short>(ref value);
                var flagsShort = Unsafe.As<T, short>(ref flags);

                if (flagsShort == 0 || (valueShort & flagsShort) != 0)
                    return value;
            }
            else
            {
                var valueConverted = value.ToInt32(null);
                var flagsConverted = flags.ToInt32(null);

                if (flagsConverted == 0 || (valueConverted & flagsConverted) != 0)
                    return value;
            }

            return MissesAllFlags<T>(typeof(T), flags.ToString(), value, paramName);
        }
    }
}
