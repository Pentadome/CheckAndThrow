using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw;

public class StateTests
{
    [Test]
    public async Task IsDisposed_object()
    {
        var exception = await Assert
            .That(() => Th.State.IsDisposed(new object()))
            .ThrowsExactly<ObjectDisposedException>();
        await Assert.That(exception!.ObjectName).IsEqualTo("System.Object");
    }

    [Test]
    public async Task IsDisposed_Type()
    {
        var exception = await Assert
            .That(() => Th.State.IsDisposed(typeof(object)))
            .ThrowsExactly<ObjectDisposedException>();
        await Assert.That(exception!.ObjectName).IsEqualTo("System.Object");
    }

    [Test]
    public async Task IsDisposed_string()
    {
        var exception = await Assert
            .That(() => Th.State.IsDisposed("instance"))
            .ThrowsExactly<ObjectDisposedException>();
        await Assert.That(exception!.ObjectName).IsEqualTo("instance");
    }

    [Test]
    public async Task IsDisposed_Generic_NoArguments()
    {
        var exception = await Assert
            .That(() => Th.State.IsDisposed<object>())
            .ThrowsExactly<ObjectDisposedException>();
        await Assert.That(exception!.ObjectName).IsEqualTo("System.Object");
    }

    [Test]
    public async Task IsNotInitialized_object()
    {
        var exception = await Assert
            .That(() => Th.State.IsNotInitialized(new object()))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("System.Object is not initialized.");
    }

    [Test]
    public async Task IsNotInitialized_Type()
    {
        var exception = await Assert
            .That(() => Th.State.IsNotInitialized(typeof(object)))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("System.Object is not initialized.");
    }

    [Test]
    public async Task IsNotInitialized_string()
    {
        var exception = await Assert
            .That(() => Th.State.IsNotInitialized("instance"))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("instance is not initialized.");
    }

    [Test]
    public async Task IsNotInitialized_Generic_NoArguments()
    {
        var exception = await Assert
            .That(() => Th.State.IsNotInitialized<object>())
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("System.Object is not initialized.");
    }

    [Test]
    public async Task IsNotMutable_object()
    {
        var exception = await Assert
            .That(() => Th.State.IsNotMutable(new object()))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("System.Object is not mutable.");
    }

    [Test]
    public async Task IsNotMutable_Type()
    {
        var exception = await Assert
            .That(() => Th.State.IsNotMutable(typeof(object)))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("System.Object is not mutable.");
    }

    [Test]
    public async Task IsNotMutable_string()
    {
        var exception = await Assert
            .That(() => Th.State.IsNotMutable("instance"))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("instance is not mutable.");
    }

    [Test]
    public async Task IsNotMutable_Generic_NoArguments()
    {
        var exception = await Assert
            .That(() => Th.State.IsNotMutable<object>())
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("System.Object is not mutable.");
    }

    [Test]
    public async Task IsDisposed_RejectsNull_object()
    {
        var exception = await Assert
            .That(() => Th.State.IsDisposed((object)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instance");
    }

    [Test]
    public async Task IsDisposed_RejectsNull_Type()
    {
        var exception = await Assert
            .That(() => Th.State.IsDisposed((Type)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("type");
    }

    [Test]
    public async Task IsDisposed_RejectsNull_string()
    {
        var exception = await Assert
            .That(() => Th.State.IsDisposed((string)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instanceName");
    }

    [Test]
    public async Task IsDisposed_RejectsEmptyName()
    {
        var exception = await Assert
            .That(() => Th.State.IsDisposed(" "))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instanceName");
    }

    [Test]
    public async Task IsNotInitialized_RejectsNull_object()
    {
        var exception = await Assert
            .That(() => Th.State.IsNotInitialized((object)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instance");
    }

    [Test]
    public async Task IsNotInitialized_RejectsNull_Type()
    {
        var exception = await Assert
            .That(() => Th.State.IsNotInitialized((Type)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("type");
    }

    [Test]
    public async Task IsNotInitialized_RejectsNull_string()
    {
        var exception = await Assert
            .That(() => Th.State.IsNotInitialized((string)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instanceName");
    }

    [Test]
    public async Task IsNotInitialized_RejectsEmptyName()
    {
        var exception = await Assert
            .That(() => Th.State.IsNotInitialized(" "))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instanceName");
    }

    [Test]
    public async Task IsNotMutable_RejectsNull_object()
    {
        var exception = await Assert
            .That(() => Th.State.IsNotMutable((object)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instance");
    }

    [Test]
    public async Task IsNotMutable_RejectsNull_Type()
    {
        var exception = await Assert
            .That(() => Th.State.IsNotMutable((Type)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instanceType");
    }

    [Test]
    public async Task IsNotMutable_RejectsNull_string()
    {
        var exception = await Assert
            .That(() => Th.State.IsNotMutable((string)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instanceName");
    }

    [Test]
    public async Task IsNotMutable_RejectsEmptyName()
    {
        var exception = await Assert
            .That(() => Th.State.IsNotMutable(" "))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instanceName");
    }
}
