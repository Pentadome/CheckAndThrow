namespace CheckAndThrow;

public static partial class Check
{
    /// <summary>
    /// Provides methods to check expressions.
    /// </summary>
    public static class Expression
    {
        /// <summary>
        /// Ensures that the specified expression does not evaluate to null.
        /// </summary>
        /// <typeparam name="T">The type of the expression.</typeparam>
        /// <param name="expression">The expression to check.</param>
        /// <param name="expressionString">The string representation of the expression.</param>
        /// <returns>The result of the expression if it is not null.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the expression evaluates to null.</exception>
        [return: NotNull]
        public static T NotNull<T>(
            [NotNull, NoEnumeration] T expression,
            [CallerArgumentExpression(nameof(expression))] string expressionString = ""
        )
            where T : class
        {
            if (expression is null)
            {
                throw new InvalidOperationException(
                    $"Evaluating the expression: \"{expressionString}\" evaluated into a null value unexpectedly."
                );
            }

            return expression;
        }

        /// <summary>
        /// Ensures that the specified expression does not evaluate to a value equal to the specified value.
        /// </summary>
        /// <typeparam name="T">The type of the expression.</typeparam>
        /// <param name="expression">The expression to check.</param>
        /// <param name="notEqualToValue">The value that the expression should not be equal to.</param>
        /// <param name="equalityComparer">The equality comparer to use, or null to use the default comparer.</param>
        /// <param name="expressionString">The string representation of the expression.</param>
        /// <param name="notEqualToValueString">The string representation of the value.</param>
        /// <returns>The result of the expression if it is not equal to the specified value.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the expression evaluates to a value equal to the specified value.</exception>
        public static T NotEqualTo<T>(
            [NoEnumeration] T expression,
            [NoEnumeration] T notEqualToValue,
            IEqualityComparer<T>? equalityComparer = null,
            [CallerArgumentExpression(nameof(expression))] string expressionString = "",
            [CallerArgumentExpression(nameof(notEqualToValue))] string notEqualToValueString = ""
        )
        {
            equalityComparer ??= EqualityComparer<T>.Default;
            if (equalityComparer.Equals(expression, notEqualToValue))
            {
                throw new InvalidOperationException(
                    $"Evaluating the expression: \"{expressionString}\" evaluated into a value that is equal to \"{notEqualToValueString}\" unexpectedly."
                );
            }

            return expression;
        }

        /// <summary>
        /// Ensures that the specified expression evaluates to a value equal to the specified value.
        /// </summary>
        /// <typeparam name="T">The type of the expression.</typeparam>
        /// <param name="expression">The expression to check.</param>
        /// <param name="equalToValue">The value that the expression should be equal to.</param>
        /// <param name="equalityComparer">The equality comparer to use, or null to use the default comparer.</param>
        /// <param name="expressionString">The string representation of the expression.</param>
        /// <param name="equalToValueString">The string representation of the value.</param>
        /// <returns>The result of the expression if it is equal to the specified value.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the expression evaluates to a value not equal to the specified value.</exception>
        public static T EqualTo<T>(
            [NoEnumeration] T expression,
            [NoEnumeration] T equalToValue,
            IEqualityComparer<T>? equalityComparer = null,
            [CallerArgumentExpression(nameof(expression))] string expressionString = "",
            [CallerArgumentExpression(nameof(equalToValue))] string equalToValueString = ""
        )
        {
            equalityComparer ??= EqualityComparer<T>.Default;
            if (!equalityComparer.Equals(expression, equalToValue))
            {
                throw new InvalidOperationException(
                    $"Evaluating the expression: \"{expressionString}\" evaluated into a value that is not equal to \"{equalToValueString}\" unexpectedly."
                );
            }

            return expression;
        }
    }
}
