namespace CheckAndThrow;

public static partial class Throw
{
    public static partial class Arg
    {
        const string NotAssignableToMessage = "Argument is not assignable to the required type.";
        const string NotAssignableToMessageWithInfo =
            $"{NotAssignableToMessage} Expected type: \"{{0}}\". Argument type: \"{{1}}\".";

        const string NotAssignableFromMessage =
            "Argument type is not assignable from the required type.";
        const string NotAssignableFromMessageWithInfo =
            $"{NotAssignableFromMessage} Expected type: \"{{0}}\". Argument type: \"{{1}}\".";
        const string MissesAttributeMessage = "Argument type misses the required attribute";
        const string MissesAttributeMessageWithInfo =
            $"{MissesAttributeMessage} Expected attribute: \"{{0}}\". Missing on argument type: \"{{1}}\".";

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the argument is not an instance of the required type.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the argument is not an instance of the type.</exception>
        [DoesNotReturn]
        public static void IsNotAssignableTo([InvokerParameterName] string paramName) =>
            throw new ArgumentException(NotAssignableToMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the argument is not an instance of the required type.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the argument is not an instance of the type.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsNotAssignableTo<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(NotAssignableToMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the argument is not an instance of the required type.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="expectedType">The type the argument should be assignable to.</param>
        /// <param name="argumentType">The type of the argument.</param>
        /// <exception cref="ArgumentException">Thrown because the argument is not an instance of the type.</exception>
        [DoesNotReturn]
        public static void IsNotAssignableTo(
            [InvokerParameterName] string paramName,
            Type expectedType,
            Type argumentType
        ) =>
            throw new ArgumentException(
                string.Format(NotAssignableToMessageWithInfo, expectedType, argumentType),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the argument is not an instance of the required type.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="expectedType">The expected type that the argument should be assignable to.</param>
        /// <param name="argumentType">The type of the argument.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the argument is not an instance of the type.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsNotAssignableTo<TFakeReturn>(
            [InvokerParameterName] string paramName,
            Type expectedType,
            Type argumentType
        ) =>
            throw new ArgumentException(
                string.Format(NotAssignableToMessageWithInfo, expectedType, argumentType),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the argument type is not assignable from the required type.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the argument type is not assignable from the type.</exception>
        [DoesNotReturn]
        public static void IsNotAssignableFrom([InvokerParameterName] string paramName) =>
            throw new ArgumentException(NotAssignableFromMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the argument type is not assignable from the required type.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the argument type is not assignable from the type.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsNotAssignableFrom<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(NotAssignableFromMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the argument type is not assignable from the required type.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="expectedType">The expected type that the argument should be assignable from.</param>
        /// <param name="argumentType">The type of the argument.</param>
        /// <exception cref="ArgumentException">Thrown because the argument type is not assignable from the type.</exception>
        [DoesNotReturn]
        public static void IsNotAssignableFrom(
            [InvokerParameterName] string paramName,
            Type expectedType,
            Type argumentType
        ) =>
            throw new ArgumentException(
                string.Format(NotAssignableFromMessageWithInfo, expectedType, argumentType),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the argument type is not assignable from the required type.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="expectedType">The expected type that the argument should be assignable from.</param>
        /// <param name="argumentType">The type of the argument.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the argument type is not assignable from the type.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsNotAssignableFrom<TFakeReturn>(
            [InvokerParameterName] string paramName,
            Type expectedType,
            Type argumentType
        ) =>
            throw new ArgumentException(
                string.Format(NotAssignableFromMessageWithInfo, expectedType, argumentType),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the argument type misses the required attribute.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the argument type misses the required attribute.</exception>
        [DoesNotReturn]
        public static void DoesNotHaveAttribute([InvokerParameterName] string paramName) =>
            throw new ArgumentException(MissesAttributeMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the argument type misses the required attribute.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the argument type misses the required attribute.</exception>
        [DoesNotReturn]
        public static TFakeReturn DoesNotHaveAttribute<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(MissesAttributeMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the argument type misses the required attribute.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="attributeType">The type of the required attribute.</param>
        /// <param name="argumentType">The type of the argument.</param>
        /// <exception cref="ArgumentException">Thrown because the argument type misses the required attribute.</exception>
        [DoesNotReturn]
        public static void DoesNotHaveAttribute(
            [InvokerParameterName] string paramName,
            Type attributeType,
            Type argumentType
        ) =>
            throw new ArgumentException(
                string.Format(MissesAttributeMessageWithInfo, attributeType, argumentType),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the argument type misses the required attribute.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="attributeType">The type of the required attribute.</param>
        /// <param name="argumentType">The type of the argument.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the argument type misses the required attribute.</exception>
        [DoesNotReturn]
        public static TFakeReturn DoesNotHaveAttribute<TFakeReturn>(
            [InvokerParameterName] string paramName,
            Type attributeType,
            Type argumentType
        ) =>
            throw new ArgumentException(
                string.Format(MissesAttributeMessageWithInfo, attributeType, argumentType),
                paramName
            );
    }
}
