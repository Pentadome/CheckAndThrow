namespace CheckAndThrow;

public static partial class Throw
{
    public static partial class Arg
    {
        const string CanNotBeEmpty = "Argument can not be empty.";
        const string CanNotHaveANullValue = "Argument can not have a null value";
        const string InvalidCountMessage = "Argument has an invalid count.";
        const string InvalidCountMessageWithInfo =
            "Argument has an invalid count, was {0} but should be {1}.";
        const string TooFewItemsMessage = "Argument has too few items.";
        const string TooFewItemsMessageWithInfo =
            "Argument has too few items, had {0} items but should have atleast {1} items.";
        const string TooManyItemsMessage = "Argument has too many items.";
        const string TooManyItemsMessageWithInfo =
            "Argument has too many items, had {0} items but should have at most {1} items.";
        const string DoesNotContainMessage = "Argument does not contain the required item.";
        const string IndexOutOfRangeWithInfo =
            "Argument has an out of range index value. Must be 0 or higher and less than {0}, but was {1}.";

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument is empty.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the collection is empty.</exception>
        [DoesNotReturn]
        public static void IsEmpty([InvokerParameterName] string paramName) =>
            throw new ArgumentException(CanNotBeEmpty, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument is empty.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the collection is empty.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsEmpty<TFakeReturn>([InvokerParameterName] string paramName) =>
            throw new ArgumentException(CanNotBeEmpty, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument contains a null value.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the collection contains a null value.</exception>
        [DoesNotReturn]
        public static void HasNullValue([InvokerParameterName] string paramName) =>
            throw new ArgumentException(CanNotHaveANullValue, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument contains a null value.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the collection contains a null value.</exception>
        [DoesNotReturn]
        public static TFakeReturn HasNullValue<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(CanNotHaveANullValue, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument has an invalid count.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the collection has an invalid count.</exception>
        [DoesNotReturn]
        public static void InvalidCount([InvokerParameterName] string paramName) =>
            throw new ArgumentException(InvalidCountMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument has an invalid count.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the collection has an invalid count.</exception>
        [DoesNotReturn]
        public static TFakeReturn InvalidCount<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(InvalidCountMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument has an invalid count.
        /// </summary>
        /// <param name="requiredCount">The required count.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="actualCount">The actual count.</param>
        /// <exception cref="ArgumentException">Thrown because the collection has an invalid count.</exception>
        [DoesNotReturn]
        public static void InvalidCount(
            object actualCount,
            object requiredCount,
            [InvokerParameterName] string paramName
        ) =>
            throw new ArgumentException(
                string.Format(InvalidCountMessageWithInfo, actualCount, requiredCount),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument has an invalid count.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="requiredCount">The required count.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="actualCount">The actual count.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the collection has an invalid count.</exception>
        [DoesNotReturn]
        public static TFakeReturn InvalidCount<TFakeReturn>(
            object actualCount,
            object requiredCount,
            [InvokerParameterName] string paramName
        ) =>
            throw new ArgumentException(
                string.Format(InvalidCountMessageWithInfo, actualCount, requiredCount),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument has too few items.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the collection has too few items.</exception>
        [DoesNotReturn]
        public static void TooFewItems([InvokerParameterName] string paramName) =>
            throw new ArgumentException(TooFewItemsMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument has too few items.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the collection has too few items.</exception>
        [DoesNotReturn]
        public static TFakeReturn TooFewItems<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(TooFewItemsMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument has too few items.
        /// </summary>
        /// <param name="requiredMinimumAmount">The minimum allowed number of items.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="actualAmount">The actual number of items.</param>
        /// <exception cref="ArgumentException">Thrown because the collection has too few items.</exception>
        [DoesNotReturn]
        public static void TooFewItems(
            object actualAmount,
            object requiredMinimumAmount,
            [InvokerParameterName] string paramName
        ) =>
            throw new ArgumentException(
                string.Format(TooFewItemsMessageWithInfo, actualAmount, requiredMinimumAmount),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument has too few items.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="requiredMinimumAmount">The minimum allowed number of items.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="actualAmount">The actual number of items.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the collection has too few items.</exception>
        [DoesNotReturn]
        public static TFakeReturn TooFewItems<TFakeReturn>(
            object actualAmount,
            object requiredMinimumAmount,
            [InvokerParameterName] string paramName
        ) =>
            throw new ArgumentException(
                string.Format(TooFewItemsMessageWithInfo, actualAmount, requiredMinimumAmount),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument has too many items.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the collection has too many items.</exception>
        [DoesNotReturn]
        public static void TooManyItems([InvokerParameterName] string paramName) =>
            throw new ArgumentException(TooManyItemsMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument has too many items.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the collection has too many items.</exception>
        [DoesNotReturn]
        public static TFakeReturn TooManyItems<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentException(TooManyItemsMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument has too many items.
        /// </summary>
        /// <param name="requiredMaximumAmount">The maximum allowed number of items.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="actualAmount">The actual number of items.</param>
        /// <exception cref="ArgumentException">Thrown because the collection has too many items.</exception>
        [DoesNotReturn]
        public static void TooManyItems(
            object actualAmount,
            object requiredMaximumAmount,
            [InvokerParameterName] string paramName
        ) =>
            throw new ArgumentException(
                string.Format(TooManyItemsMessageWithInfo, actualAmount, requiredMaximumAmount),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument has too many items.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="requiredMaximumAmount">The maximum allowed number of items.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="actualAmount">The actual number of items.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentException">Thrown because the collection has too many items.</exception>
        [DoesNotReturn]
        public static TFakeReturn TooManyItems<TFakeReturn>(
            object actualAmount,
            object requiredMaximumAmount,
            [InvokerParameterName] string paramName
        ) =>
            throw new ArgumentException(
                string.Format(TooManyItemsMessageWithInfo, actualAmount, requiredMaximumAmount),
                paramName
            );

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> because the collection argument does not contain the required item.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown because the collection does not contain the item.</exception>
        [DoesNotReturn]
        public static void DoesNotContain([InvokerParameterName] string paramName) =>
            throw new ArgumentException(DoesNotContainMessage, paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> because the index argument is out of range for the collection.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown because the index is out of range.</exception>
        [DoesNotReturn]
        public static void IsInvalidIndex([InvokerParameterName] string paramName) =>
            throw new ArgumentOutOfRangeException(paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> because the index argument is out of range for the collection.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown because the index is out of range.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsInvalidIndex<TFakeReturn>(
            [InvokerParameterName] string paramName
        ) => throw new ArgumentOutOfRangeException(paramName);

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> because the index argument is out of range for the collection.
        /// </summary>
        /// <param name="index">The invalid index value.</param>
        /// <param name="collectionSize">The size of the collection.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown because the index is out of range.</exception>
        [DoesNotReturn]
        public static void IsInvalidIndex(
            object index,
            object collectionSize,
            [InvokerParameterName] string paramName
        ) =>
            throw new ArgumentOutOfRangeException(
                paramName,
                index,
                string.Format(IndexOutOfRangeWithInfo, collectionSize, index)
            );

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> because the index argument is out of range for the collection.
        /// </summary>
        /// <typeparam name="TFakeReturn">The type of the fake return value.</typeparam>
        /// <param name="index">The invalid index value.</param>
        /// <param name="collectionSize">The size of the collection.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown because the index is out of range.</exception>
        [DoesNotReturn]
        public static TFakeReturn IsInvalidIndex<TFakeReturn>(
            object index,
            object collectionSize,
            [InvokerParameterName] string paramName
        ) =>
            throw new ArgumentOutOfRangeException(
                paramName,
                index,
                string.Format(IndexOutOfRangeWithInfo, collectionSize, index)
            );
    }
}
