using System.Collections;

namespace CheckAndThrow;

public static partial class Check
{
    public static partial class Arg
    {
        /// <summary>
        /// Ensures that the specified collection is not null and not empty.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collection">The collection to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original collection if it is not null and not empty.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="collection"/> is empty.</exception>
        [return: NotNull]
        public static T IsNotNullOrEmpty<T>(
            [NoEnumeration, NotNull] T collection,
            [CallerArgumentExpression(nameof(collection)), InvokerParameterName]
                string paramName = ""
        )
            where T : ICollection
        {
            if (NotNull(collection, paramName).Count == 0)
            {
                Throw.Arg.IsEmpty(paramName);
            }
            return collection;
        }

        /// <summary>
        /// Ensures that the specified enumerable is not null and does not contain any null elements.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original enumerable if it is not null and contains no null elements.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="enumerable"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="enumerable"/> contains a null element.</exception>
        public static T IsNotNullAndHasNoNulls<T>(
            [InstantHandle, NotNull] T enumerable,
            [CallerArgumentExpression(nameof(enumerable)), InvokerParameterName]
                string paramName = ""
        )
            where T : IEnumerable
        {
            NotNull(enumerable, paramName);

            foreach (var value in enumerable)
            {
                if (value is null)
                    Throw.Arg.HasNullValue(paramName);
            }
            return enumerable;
        }

        /// <summary>
        /// Ensures that the specified collection has the required count of items.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collection">The collection to check.</param>
        /// <param name="count">The required count of items.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original collection if it has the required count of items.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="collection"/> does not have the required count of items.</exception>
        public static T HasCount<T>(
            [NoEnumeration, NotNull] T collection,
            int count,
            [CallerArgumentExpression(nameof(collection)), InvokerParameterName]
                string paramName = ""
        )
            where T : ICollection
        {
            if (NotNull(collection, paramName).Count != count)
            {
                Throw.Arg.InvalidCount(paramName);
            }
            return collection;
        }

        /// <summary>
        /// Ensures that the specified collection has at least the required minimum count of items.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collection">The collection to check.</param>
        /// <param name="minCount">The minimum required count of items.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original collection if it has at least the required minimum count of items.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="collection"/> has fewer than <paramref name="minCount"/> items.</exception>
        public static T HasMinCount<T>(
            [NoEnumeration, NotNull] T collection,
            int minCount,
            [CallerArgumentExpression(nameof(collection)), InvokerParameterName]
                string paramName = ""
        )
            where T : ICollection
        {
            if (NotNull(collection, paramName).Count < minCount)
            {
                Throw.Arg.TooFewItems(paramName);
            }
            return collection;
        }

        /// <summary>
        /// Ensures that the specified collection has at most the required maximum count of items.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collection">The collection to check.</param>
        /// <param name="maxCount">The maximum required count of items.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original collection if it has at most the required maximum count of items.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="collection"/> has more than <paramref name="maxCount"/> items.</exception>
        public static T HasMaxCount<T>(
            [NoEnumeration, NotNull] T collection,
            int maxCount,
            [CallerArgumentExpression(nameof(collection)), InvokerParameterName]
                string paramName = ""
        )
            where T : ICollection
        {
            if (NotNull(collection, paramName).Count > maxCount)
            {
                Throw.Arg.TooManyItems(paramName);
            }
            return collection;
        }

        /// <summary>
        /// Ensures that the specified collection contains the required item.
        /// </summary>
        /// <typeparam name="TCollection">The type of the collection.</typeparam>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="collection">The collection to check.</param>
        /// <param name="item">The item that must be in the collection.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original collection if it contains the item.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="collection"/> does not contain the <paramref name="item"/>.</exception>
        public static TCollection Contains<TCollection, TItem>(
            [NotNull] TCollection collection,
            TItem item,
            [CallerArgumentExpression(nameof(collection)), InvokerParameterName]
                string paramName = ""
        )
            where TCollection : ICollection<TItem>
        {
            if (!NotNull(collection, paramName).Contains(item))
            {
                Throw.Arg.DoesNotContain(paramName);
            }
            return collection;
        }

        /// <summary>
        /// Ensures that the specified index is valid for the given collection.
        /// </summary>
        /// <param name="indexArgument">The index to validate.</param>
        /// <param name="collection">The collection to check the index against.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original index if it is valid.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="indexArgument"/> is less than 0 or greater than or equal to the collection count.</exception>
        public static int HasValidIndex(
            int indexArgument,
            ICollection collection,
            [CallerArgumentExpression(nameof(indexArgument)), InvokerParameterName]
                string paramName = ""
        )
        {
            if (indexArgument < 0 || indexArgument >= collection.Count)
            {
                Throw.Arg.IsInvalidIndex(indexArgument, collection.Count, paramName);
            }
            return indexArgument;
        }

        /// <summary>
        /// Ensures that the specified index is valid for the given collection size.
        /// </summary>
        /// <param name="indexArgument">The index to validate.</param>
        /// <param name="collectionSize">The size of the collection to check the index against.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The original index if it is valid.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="indexArgument"/> is less than 0 or greater than or equal to <paramref name="collectionSize"/>.</exception>
        public static int HasValidIndex(
            int indexArgument,
            int collectionSize,
            [CallerArgumentExpression(nameof(indexArgument)), InvokerParameterName]
                string paramName = ""
        )
        {
            if (indexArgument < 0 || indexArgument >= collectionSize)
            {
                Throw.Arg.IsInvalidIndex(indexArgument, collectionSize, paramName);
            }
            return indexArgument;
        }
    }
}
