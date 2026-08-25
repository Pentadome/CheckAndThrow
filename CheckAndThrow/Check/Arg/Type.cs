using System.Reflection;

namespace CheckAndThrow;

public static partial class Check
{
    public static partial class Arg
    {
        /// <summary>
        /// Ensures that the specified argument is an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The required type.</typeparam>
        /// <param name="argument">The argument to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original argument cast to the required type if it is an instance of that type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is not an instance of the required type.</exception>
        public static T IsAssignableTo<T>(
            object argument,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            if (NotNull(argument, paramName) is T result)
            {
                return result;
            }

            Throw.Arg.IsNotAssignableTo(paramName, typeof(T), argument.GetType());
            throw new UnreachableException();
        }

        /// <summary>
        /// Ensures that the specified type is assignable from the required type.
        /// </summary>
        /// <typeparam name="TTarget">The required type.</typeparam>
        /// <param name="argument">The type to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original type if it is assignable from the required type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is not assignable from the required type.</exception>
        public static Type IsAssignableFrom<TTarget>(
            Type argument,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            if (NotNull(argument, paramName).IsAssignableFrom(typeof(TTarget)))
            {
                return argument;
            }

            Throw.Arg.IsNotAssignableFrom(paramName);
            throw new UnreachableException();
        }

        /// <summary>
        /// Ensures that the specified type has the required attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="argument">The type to check.</param>
        /// <param name="includeAncestors">Whether to include attributes from ancestor types.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The attribute instance if the type has the required attribute.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> does not have the required attribute.</exception>
        /// <inheritdoc cref="M:System.Reflection.CustomAttributeExtensions.GetCustomAttribute``1(System.Reflection.MemberInfo,System.Boolean)" path="/exception[@cref='T:System.Reflection.AmbiguousMatchException']"/>
        /// <inheritdoc cref="M:System.Reflection.CustomAttributeExtensions.GetCustomAttribute``1(System.Reflection.MemberInfo,System.Boolean)" path="/exception[@cref='T:System.TypeLoadException']"/>
        public static TAttribute HasTheAttribute<TAttribute>(
            Type argument,
            bool includeAncestors = false,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
            where TAttribute : Attribute
        {
            if (
                NotNull(argument, paramName).GetCustomAttribute<TAttribute>(includeAncestors) is
                { } attribute
            )
            {
                return attribute;
            }

            Throw.Arg.DoesNotHaveAttribute(paramName, typeof(TAttribute), argument);

            throw new UnreachableException();
        }

        /// <summary>
        /// Ensures that the specified type has the required attribute.
        /// </summary>
        /// <param name="argument">The type to check.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <param name="includeAncestors">Whether to include attributes from ancestor types.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The attribute instance if the type has the required attribute.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> or <paramref name="attributeType"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> does not have the required attribute.</exception>
        /// <inheritdoc cref="M:System.Reflection.CustomAttributeExtensions.GetCustomAttribute``1(System.Reflection.MemberInfo,System.Boolean)" path="/exception[@cref='T:System.Reflection.AmbiguousMatchException']"/>
        /// <inheritdoc cref="M:System.Reflection.CustomAttributeExtensions.GetCustomAttribute``1(System.Reflection.MemberInfo,System.Boolean)" path="/exception[@cref='T:System.TypeLoadException']"/>
        public static Attribute HasTheAttribute(
            Type argument,
            Type attributeType,
            bool includeAncestors = false,
            [CallerArgumentExpression(nameof(argument)), InvokerParameterName] string paramName = ""
        )
        {
            if (
                NotNull(argument, paramName)
                    .GetCustomAttribute(NotNull(attributeType), includeAncestors) is
                { } attribute
            )
            {
                return attribute;
            }

            Throw.Arg.DoesNotHaveAttribute(paramName, attributeType, argument);

            throw new UnreachableException();
        }
    }
}
