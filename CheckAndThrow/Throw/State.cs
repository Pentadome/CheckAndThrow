namespace CheckAndThrow;

public static partial class Throw
{
    /// <summary>
    /// Provides methods to throw exceptions related to the state of an object.
    /// </summary>
    public static class State
    {
        /// <summary>Throws because an object is disposed.</summary>
        /// <exception cref="ObjectDisposedException">Always thrown.</exception>
        [DoesNotReturn]
        public static void Disposed() => throw new ObjectDisposedException("Object");

        /// <summary>Throws because an object is not initialized.</summary>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static void NotInitialized() =>
            throw new InvalidOperationException("Object is not initialized.");

        /// <summary>Throws because an object is not mutable.</summary>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static void NotMutable() =>
            throw new InvalidOperationException("Object is not mutable.");

        /// <summary>
        /// Throws an <see cref="ObjectDisposedException"/> indicating that the specified instance is disposed.
        /// </summary>
        /// <param name="instance">The instance that is disposed.</param>
        /// <exception cref="ObjectDisposedException">Always thrown.</exception>
        [DoesNotReturn]
        public static void Disposed(object instance)
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
        public static void Disposed(Type type)
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
        public static void Disposed(string instanceName)
        {
            var name = Check.Arg.NotNullOrWhiteSpace(instanceName);

            throw new ObjectDisposedException(name);
        }

        /// <summary>
        /// Throws an <see cref="ObjectDisposedException"/> indicating that the type <typeparamref name="T"/> is disposed.
        /// </summary>
        /// <typeparam name="T">The type that is disposed.</typeparam>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="ObjectDisposedException">Always thrown.</exception>
        [DoesNotReturn]
        public static T Disposed<T>()
        {
            var name = typeof(T).FullName!;

            throw new ObjectDisposedException(name);
        }

        /// <summary>Throws because the specified instance is disposed.</summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="instance">The disposed instance.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="ObjectDisposedException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn Disposed<TFakeReturn>(object instance) =>
            throw new ObjectDisposedException(Check.Arg.NotNull(instance).GetType().FullName);

        /// <summary>Throws because the specified type is disposed.</summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="type">The disposed type.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="ObjectDisposedException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn Disposed<TFakeReturn>(Type type) =>
            throw new ObjectDisposedException(Check.Arg.NotNull(type).FullName);

        /// <summary>Throws because the specified named instance is disposed.</summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="instanceName">The disposed instance name.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="ObjectDisposedException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn Disposed<TFakeReturn>(string instanceName) =>
            throw new ObjectDisposedException(Check.Arg.NotNullOrWhiteSpace(instanceName));

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> indicating that the specified instance is not initialized.
        /// </summary>
        /// <param name="instance">The instance that is not initialized.</param>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static void NotInitialized(object instance)
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
        public static void NotInitialized(Type type)
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
        public static void NotInitialized(string instanceName)
        {
            var name = Check.Arg.NotNullOrWhiteSpace(instanceName);

            throw new InvalidOperationException($"{name} is not initialized.");
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> indicating that the type <typeparamref name="T"/> is not initialized.
        /// </summary>
        /// <typeparam name="T">The type that is not initialized.</typeparam>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static T NotInitialized<T>()
        {
            var name = typeof(T).FullName!;

            throw new InvalidOperationException($"{name} is not initialized.");
        }

        /// <summary>Throws because the specified instance is not initialized.</summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="instance">The uninitialized instance.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn NotInitialized<TFakeReturn>(object instance) =>
            throw new InvalidOperationException(
                $"{Check.Arg.NotNull(instance).GetType().FullName} is not initialized."
            );

        /// <summary>Throws because the specified type is not initialized.</summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="type">The uninitialized type.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn NotInitialized<TFakeReturn>(Type type) =>
            throw new InvalidOperationException(
                $"{Check.Arg.NotNull(type).FullName} is not initialized."
            );

        /// <summary>Throws because the specified named instance is not initialized.</summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="instanceName">The uninitialized instance name.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn NotInitialized<TFakeReturn>(string instanceName) =>
            throw new InvalidOperationException(
                $"{Check.Arg.NotNullOrWhiteSpace(instanceName)} is not initialized."
            );

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> indicating that the specified instance is not mutable.
        /// </summary>
        /// <param name="instance">The instance that is not mutable.</param>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static void NotMutable(object instance)
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
        public static void NotMutable(Type instanceType)
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
        public static void NotMutable(string instanceName)
        {
            var name = Check.Arg.NotNullOrWhiteSpace(instanceName);

            throw new InvalidOperationException($"{name} is not mutable.");
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> indicating that the type <typeparamref name="T"/> is not mutable.
        /// </summary>
        /// <typeparam name="T">The type that is not mutable.</typeparam>
        /// <returns>Nothing is returned as the method always throws.</returns>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static T NotMutable<T>()
        {
            var name = typeof(T).FullName!;

            throw new InvalidOperationException($"{name} is not mutable.");
        }

        /// <summary>Throws because the specified instance is not mutable.</summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="instance">The immutable instance.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn NotMutable<TFakeReturn>(object instance) =>
            throw new InvalidOperationException(
                $"{Check.Arg.NotNull(instance).GetType().FullName} is not mutable."
            );

        /// <summary>Throws because the specified type is not mutable.</summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="instanceType">The immutable type.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn NotMutable<TFakeReturn>(Type instanceType) =>
            throw new InvalidOperationException(
                $"{Check.Arg.NotNull(instanceType).FullName} is not mutable."
            );

        /// <summary>Throws because the specified named instance is not mutable.</summary>
        /// <typeparam name="TFakeReturn">The fake return type.</typeparam>
        /// <param name="instanceName">The immutable instance name.</param>
        /// <returns>This method never returns.</returns>
        /// <exception cref="InvalidOperationException">Always thrown.</exception>
        [DoesNotReturn]
        public static TFakeReturn NotMutable<TFakeReturn>(string instanceName) =>
            throw new InvalidOperationException(
                $"{Check.Arg.NotNullOrWhiteSpace(instanceName)} is not mutable."
            );
    }
}
