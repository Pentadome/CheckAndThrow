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
            return !isDisposed ? instance : Throw.State.Disposed<T>();
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

            Throw.State.Disposed(instanceType);
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

            Throw.State.Disposed(instanceName);
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

            Throw.State.Disposed<T>();
        }

        /// <summary>
        /// Ensures that the specified instance is initialized.
        /// </summary>
        /// <param name="isInitialized">A boolean value indicating whether the instance is initialized.</param>
        /// <param name="instance">The instance to check.</param>
        /// <returns><paramref name="instance"/> if initialized.</returns>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="isInitialized"/> is false.</exception>
        public static T Initialized<T>([DoesNotReturnIf(false)] bool isInitialized, T instance)
            where T : notnull
        {
            return isInitialized ? instance : Throw.State.NotInitialized<T>();
        }

        /// <summary>
        /// Ensures that the specified instance is initialized.
        /// </summary>
        /// <param name="isInitialized">A boolean value indicating whether the instance is initialized.</param>
        /// <param name="instanceType">The type of the instance to check.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="isInitialized"/> is false.</exception>
        public static void Initialized(
            [DoesNotReturnIf(false)] bool isInitialized,
            Type instanceType
        )
        {
            if (isInitialized)
                return;

            Throw.State.NotInitialized(instanceType);
        }

        /// <summary>
        /// Ensures that the specified instance is initialized.
        /// </summary>
        /// <param name="isInitialized">A boolean value indicating whether the instance is initialized.</param>
        /// <param name="instanceName">The name of the instance to check.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="isInitialized"/> is false.</exception>
        public static void Initialized(
            [DoesNotReturnIf(false)] bool isInitialized,
            string instanceName
        )
        {
            if (isInitialized)
                return;

            Throw.State.NotInitialized(instanceName);
        }

        /// <summary>
        /// Ensures that the specified instance is initialized.
        /// </summary>
        /// <typeparam name="T">The type of the instance to check.</typeparam>
        /// <param name="isInitialized">A boolean value indicating whether the instance is initialized.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="isInitialized"/> is false.</exception>
        public static void Initialized<T>([DoesNotReturnIf(false)] bool isInitialized)
        {
            if (isInitialized)
                return;

            Throw.State.NotInitialized<T>();
        }

        /// <summary>
        /// Ensures that the specified instance is mutable.
        /// </summary>
        /// <param name="isMutable">A boolean value indicating whether the instance is mutable.</param>
        /// <param name="instance">The instance to check.</param>
        /// <returns><paramref name="instance"/> if mutable.</returns>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="isMutable"/> is false.</exception>
        public static T Mutable<T>([DoesNotReturnIf(false)] bool isMutable, T instance)
            where T : notnull
        {
            return isMutable ? instance : Throw.State.NotMutable<T>();
        }

        /// <summary>
        /// Ensures that the specified instance is mutable.
        /// </summary>
        /// <param name="isMutable">A boolean value indicating whether the instance is mutable.</param>
        /// <param name="instanceType">The type of the instance to check.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="isMutable"/> is false.</exception>
        public static void Mutable([DoesNotReturnIf(false)] bool isMutable, Type instanceType)
        {
            if (isMutable)
                return;

            Throw.State.NotMutable(instanceType);
        }

        /// <summary>
        /// Ensures that the specified instance is mutable.
        /// </summary>
        /// <param name="isMutable">A boolean value indicating whether the instance is mutable.</param>
        /// <param name="instanceName">The name of the instance to check.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="isMutable"/> is false.</exception>
        public static void Mutable([DoesNotReturnIf(false)] bool isMutable, string instanceName)
        {
            if (isMutable)
                return;

            Throw.State.NotMutable(instanceName);
        }

        /// <summary>
        /// Ensures that the specified instance is mutable.
        /// </summary>
        /// <typeparam name="T">The type of the instance to check.</typeparam>
        /// <param name="isMutable">A boolean value indicating whether the instance is mutable.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="isMutable"/> is false.</exception>
        public static void Mutable<T>([DoesNotReturnIf(false)] bool isMutable)
        {
            if (isMutable)
                return;

            Throw.State.NotMutable<T>();
        }
    }
}
