namespace CheckAndThrow;

public static partial class Throw
{
    /// <summary>
    /// Provides methods to throw exceptions related to the state of an object.
    /// </summary>
    public static class State
    {
        /// <summary>
        /// Throws an <see cref="ObjectDisposedException"/> indicating that the specified instance is disposed.
        /// </summary>
        /// <param name="instance">The instance that is disposed.</param>
        /// <exception cref="ObjectDisposedException">Always thrown.</exception>
        [DoesNotReturn]
        public static void IsDisposed(object instance)
        {
            var name = Check.Arg.NotNull(instance).GetType().FullName!;

            throw new ObjectDisposedException(name);
        }

        /// <summary>
        /// Throws an <see cref="ObjectDisposedException"/> indicating that the specified type is disposed.
        /// </summary>
        /// <param name="type">The type that is disposed.</param>
        /// <exception cref="ObjectDisposedException">Always thrown.</exception>
        [DoesNotReturn]
        public static void IsDisposed(Type type)
        {
            var name = Check.Arg.NotNull(type).FullName!;

            throw new ObjectDisposedException(name);
        }

        /// <summary>
        /// Throws an <see cref="ObjectDisposedException"/> indicating that the specified instance name is disposed.
        /// </summary>
        /// <param name="instanceName">The name of the instance that is disposed.</param>
        /// <exception cref="ObjectDisposedException">Always thrown.</exception>
        [DoesNotReturn]
        public static void IsDisposed(string instanceName)
        {
            var name = Check.Arg.IsNotNullOrWhiteSpace(instanceName);

            throw new ObjectDisposedException(name);
        }

        /// <summary>
        /// Throws an <see cref="ObjectDisposedException"/> indicating that the type <typeparamref name="T"/> is disposed.
        /// </summary>
        /// <typeparam name="T">The type that is disposed.</typeparam>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ObjectDisposedException">Always thrown.</exception>
        [DoesNotReturn]
        public static T IsDisposed<T>()
        {
            var name = typeof(T).FullName!;

            throw new ObjectDisposedException(name);
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> indicating that the specified instance is not initialized.
        /// </summary>
        /// <param name="instance">The instance that is not initialized.</param>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static void IsNotInitialized(object instance)
        {
            var name = Check.Arg.NotNull(instance).GetType().FullName!;

            throw new InvalidOperationException($"{name} is not initialized.");
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> indicating that the specified type is not initialized.
        /// </summary>
        /// <param name="type">The type that is not initialized.</param>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static void IsNotInitialized(Type type)
        {
            var name = Check.Arg.NotNull(type).FullName!;

            throw new InvalidOperationException($"{name} is not initialized.");
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> indicating that the specified instance name is not initialized.
        /// </summary>
        /// <param name="instanceName">The name of the instance that is not initialized.</param>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static void IsNotInitialized(string instanceName)
        {
            var name = Check.Arg.IsNotNullOrWhiteSpace(instanceName);

            throw new InvalidOperationException($"{name} is not initialized.");
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> indicating that the type <typeparamref name="T"/> is not initialized.
        /// </summary>
        /// <typeparam name="T">The type that is not initialized.</typeparam>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static T IsNotInitialized<T>()
        {
            var name = typeof(T).FullName!;

            throw new InvalidOperationException($"{name} is not initialized.");
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> indicating that the specified instance is not mutable.
        /// </summary>
        /// <param name="instance">The instance that is not mutable.</param>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static void IsNotMutable(object instance)
        {
            var name = Check.Arg.NotNull(instance).GetType().FullName!;

            throw new InvalidOperationException($"{name} is not mutable.");
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> indicating that the specified type is not mutable.
        /// </summary>
        /// <param name="instanceType">The type that is not mutable.</param>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static void IsNotMutable(Type instanceType)
        {
            var name = Check.Arg.NotNull(instanceType).FullName!;

            throw new InvalidOperationException($"{name} is not mutable.");
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> indicating that the specified instance name is not mutable.
        /// </summary>
        /// <param name="instanceName">The name of the instance that is not mutable.</param>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static void IsNotMutable(string instanceName)
        {
            var name = Check.Arg.IsNotNullOrWhiteSpace(instanceName);

            throw new InvalidOperationException($"{name} is not mutable.");
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> indicating that the type <typeparamref name="T"/> is not mutable.
        /// </summary>
        /// <typeparam name="T">The type that is not mutable.</typeparam>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static T IsNotMutable<T>()
        {
            var name = typeof(T).FullName!;

            throw new InvalidOperationException($"{name} is not mutable.");
        }
    }
}
