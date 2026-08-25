namespace CheckAndThrow;

public static partial class Check
{
    /// <summary>
    /// Provides methods to check the state of an object.
    /// </summary>
    public static class State
    {
        /// <summary>
        /// Ensures that the specified instance is not disposed.
        /// </summary>
        /// <param name="isDisposed">A boolean value indicating whether the instance is disposed.</param>
        /// <param name="instance">The instance to check.</param>
        /// <returns>The <paramref name="instance"/> if not disposed.</returns>
        /// <exception cref="ObjectDisposedException">Thrown when <paramref name="isDisposed"/> is true.</exception>
        public static T NotDisposed<T>([DoesNotReturnIf(true)] bool isDisposed, T instance)
            where T : notnull
        {
            if (!isDisposed)
                return instance;

            Throw.State.IsDisposed(instance);

            throw new UnreachableException();
        }

        /// <summary>
        /// Ensures that the specified instance is not disposed.
        /// </summary>
        /// <param name="isDisposed">A boolean value indicating whether the instance is disposed.</param>
        /// <param name="instanceType">The type of the instance to check.</param>
        /// <exception cref="ObjectDisposedException">Thrown when <paramref name="isDisposed"/> is true.</exception>
        public static void NotDisposed([DoesNotReturnIf(true)] bool isDisposed, Type instanceType)
        {
            if (!isDisposed)
                return;

            Throw.State.IsDisposed(instanceType);
        }

        /// <summary>
        /// Ensures that the specified instance is not disposed.
        /// </summary>
        /// <param name="isDisposed">A boolean value indicating whether the instance is disposed.</param>
        /// <param name="instanceName">The name of the instance to check.</param>
        /// <exception cref="ObjectDisposedException">Thrown when <paramref name="isDisposed"/> is true.</exception>
        public static void NotDisposed([DoesNotReturnIf(true)] bool isDisposed, string instanceName)
        {
            if (!isDisposed)
                return;

            Throw.State.IsDisposed(instanceName);
        }

        /// <summary>
        /// Ensures that the specified instance is not disposed.
        /// </summary>
        /// <typeparam name="T">The type of the instance to check.</typeparam>
        /// <param name="isDisposed">A boolean value indicating whether the instance is disposed.</param>
        /// <exception cref="ObjectDisposedException">Thrown when <paramref name="isDisposed"/> is true.</exception>
        public static void NotDisposed<T>([DoesNotReturnIf(true)] bool isDisposed)
        {
            if (!isDisposed)
                return;

            Throw.State.IsDisposed<T>();
        }

        /// <summary>
        /// Ensures that the specified instance is initialized.
        /// </summary>
        /// <param name="isInitialized">A boolean value indicating whether the instance is initialized.</param>
        /// <param name="instance">The instance to check.</param>
        /// <returns><paramref name="instance"/> if initialized.</returns>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="isInitialized"/> is false.</exception>
        public static T IsInitialized<T>([DoesNotReturnIf(false)] bool isInitialized, T instance)
            where T : notnull
        {
            if (isInitialized)
                return instance;

            Throw.State.IsNotInitialized(instance);

            throw new UnreachableException();
        }

        /// <summary>
        /// Ensures that the specified instance is initialized.
        /// </summary>
        /// <param name="isInitialized">A boolean value indicating whether the instance is initialized.</param>
        /// <param name="instanceType">The type of the instance to check.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="isInitialized"/> is false.</exception>
        public static void IsInitialized(
            [DoesNotReturnIf(false)] bool isInitialized,
            Type instanceType
        )
        {
            if (isInitialized)
                return;

            Throw.State.IsNotInitialized(instanceType);
        }

        /// <summary>
        /// Ensures that the specified instance is initialized.
        /// </summary>
        /// <param name="isInitialized">A boolean value indicating whether the instance is initialized.</param>
        /// <param name="instanceName">The name of the instance to check.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="isInitialized"/> is false.</exception>
        public static void IsInitialized(
            [DoesNotReturnIf(false)] bool isInitialized,
            string instanceName
        )
        {
            if (isInitialized)
                return;

            Throw.State.IsNotInitialized(instanceName);
        }

        /// <summary>
        /// Ensures that the specified instance is initialized.
        /// </summary>
        /// <typeparam name="T">The type of the instance to check.</typeparam>
        /// <param name="isInitialized">A boolean value indicating whether the instance is initialized.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="isInitialized"/> is false.</exception>
        public static void IsInitialized<T>([DoesNotReturnIf(false)] bool isInitialized)
        {
            if (isInitialized)
                return;

            Throw.State.IsNotInitialized<T>();
        }

        /// <summary>
        /// Ensures that the specified instance is mutable.
        /// </summary>
        /// <param name="isMutable">A boolean value indicating whether the instance is mutable.</param>
        /// <param name="instance">The instance to check.</param>
        /// <returns><paramref name="instance"/> if mutable.</returns>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="isMutable"/> is false.</exception>
        public static T IsMutable<T>([DoesNotReturnIf(false)] bool isMutable, T instance)
            where T : notnull
        {
            if (isMutable)
                return instance;

            Throw.State.IsNotMutable(instance);

            throw new UnreachableException();
        }

        /// <summary>
        /// Ensures that the specified instance is mutable.
        /// </summary>
        /// <param name="isMutable">A boolean value indicating whether the instance is mutable.</param>
        /// <param name="instanceType">The type of the instance to check.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="isMutable"/> is false.</exception>
        public static void IsMutable([DoesNotReturnIf(false)] bool isMutable, Type instanceType)
        {
            if (isMutable)
                return;

            Throw.State.IsNotMutable(instanceType);
        }

        /// <summary>
        /// Ensures that the specified instance is mutable.
        /// </summary>
        /// <param name="isMutable">A boolean value indicating whether the instance is mutable.</param>
        /// <param name="instanceName">The name of the instance to check.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="isMutable"/> is false.</exception>
        public static void IsMutable([DoesNotReturnIf(false)] bool isMutable, string instanceName)
        {
            if (isMutable)
                return;

            Throw.State.IsNotMutable(instanceName);
        }

        /// <summary>
        /// Ensures that the specified instance is mutable.
        /// </summary>
        /// <typeparam name="T">The type of the instance to check.</typeparam>
        /// <param name="isMutable">A boolean value indicating whether the instance is mutable.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="isMutable"/> is false.</exception>
        public static void IsMutable<T>([DoesNotReturnIf(false)] bool isMutable)
        {
            if (isMutable)
                return;

            Throw.State.IsNotMutable<T>();
        }
    }
}
