namespace CheckAndThrow;

public static partial class Check
{
    public static partial class Args
    {
        /// <summary>
        /// Checks if the arguments are not null.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <param name="arg1">The first argument to check.</param>
        /// <param name="arg2">The second argument to check.</param>
        /// <param name="paramName1">The name of the first parameter.</param>
        /// <param name="paramName2">The name of the second parameter.</param>
        /// <returns>A tuple containing the arguments if they are not null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="arg1"/> or <paramref name="arg2"/> is null.</exception>
        [OverloadResolutionPriority(1)] // Avoid using the other overloads when the last argument is a string.
        public static (T1 arg1, T2 arg2) NotNull<T1, T2>(
            [NotNull, NoEnumeration] T1 arg1,
            [NotNull, NoEnumeration] T2 arg2,
            [CallerArgumentExpression(nameof(arg1)), InvokerParameterName] string paramName1 = "",
            [CallerArgumentExpression(nameof(arg2)), InvokerParameterName] string paramName2 = ""
        )
        {
            return arg1 is null ? throw new ArgumentNullException(paramName1)
                : arg2 is null ? throw new ArgumentNullException(paramName2)
                : (arg1, arg2);
        }

        /// <summary>
        /// Checks if the arguments are not null.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <param name="arg1">The first argument to check.</param>
        /// <param name="arg2">The second argument to check.</param>
        /// <param name="arg3">The third argument to check.</param>
        /// <param name="paramName1">The name of the first parameter.</param>
        /// <param name="paramName2">The name of the second parameter.</param>
        /// <param name="paramName3">The name of the third parameter.</param>
        /// <returns>A tuple containing the arguments if they are not null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="arg1"/>, <paramref name="arg2"/>, or <paramref name="arg3"/> is null.</exception>
        [OverloadResolutionPriority(2)] // Avoid using the other overloads when the last argument is a string.
        public static (T1 arg1, T2 arg2, T3 arg3) NotNull<T1, T2, T3>(
            [NotNull, NoEnumeration] T1 arg1,
            [NotNull, NoEnumeration] T2 arg2,
            [NotNull, NoEnumeration] T3 arg3,
            [CallerArgumentExpression(nameof(arg1)), InvokerParameterName] string paramName1 = "",
            [CallerArgumentExpression(nameof(arg2)), InvokerParameterName] string paramName2 = "",
            [CallerArgumentExpression(nameof(arg3)), InvokerParameterName] string paramName3 = ""
        )
        {
            return arg1 is null ? throw new ArgumentNullException(paramName1)
                : arg2 is null ? throw new ArgumentNullException(paramName2)
                : arg3 is null ? throw new ArgumentNullException(paramName3)
                : (arg1, arg2, arg3);
        }

        /// <summary>
        /// Checks if the arguments are not null.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <param name="arg1">The first argument to check.</param>
        /// <param name="arg2">The second argument to check.</param>
        /// <param name="arg3">The third argument to check.</param>
        /// <param name="arg4">The fourth argument to check.</param>
        /// <param name="paramName1">The name of the first parameter.</param>
        /// <param name="paramName2">The name of the second parameter.</param>
        /// <param name="paramName3">The name of the third parameter.</param>
        /// <param name="paramName4">The name of the fourth parameter.</param>
        /// <returns>A tuple containing the arguments if they are not null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="arg1"/>, <paramref name="arg2"/>, <paramref name="arg3"/>, or <paramref name="arg4"/> is null.</exception>
        [OverloadResolutionPriority(3)] // Avoid using the other overloads when the last argument is a string.
        public static (T1 arg1, T2 arg2, T3 arg3, T4 arg4) NotNull<T1, T2, T3, T4>(
            [NotNull, NoEnumeration] T1 arg1,
            [NotNull, NoEnumeration] T2 arg2,
            [NotNull, NoEnumeration] T3 arg3,
            [NotNull, NoEnumeration] T4 arg4,
            [CallerArgumentExpression(nameof(arg1)), InvokerParameterName] string paramName1 = "",
            [CallerArgumentExpression(nameof(arg2)), InvokerParameterName] string paramName2 = "",
            [CallerArgumentExpression(nameof(arg3)), InvokerParameterName] string paramName3 = "",
            [CallerArgumentExpression(nameof(arg4)), InvokerParameterName] string paramName4 = ""
        )
        {
            return arg1 is null ? throw new ArgumentNullException(paramName1)
                : arg2 is null ? throw new ArgumentNullException(paramName2)
                : arg3 is null ? throw new ArgumentNullException(paramName3)
                : arg4 is null ? throw new ArgumentNullException(paramName4)
                : (arg1, arg2, arg3, arg4);
        }
    }
}
