using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check;

public class StateTests
{
    [Test]
    public async Task NotDisposed_Instance()
    {
        var instance = new object();
        await Assert.That(C.State.NotDisposed(false, instance)).IsSameReferenceAs(instance);
        var exception = await Assert
            .That(() => C.State.NotDisposed<object>(true, new object()))
            .ThrowsExactly<ObjectDisposedException>();
        await Assert.That(exception!.ObjectName).IsEqualTo("System.Object");
    }

    [Test]
    public async Task NotDisposed_Type()
    {
        C.State.NotDisposed(false, typeof(object));
        var exception = await Assert
            .That(() => C.State.NotDisposed(true, typeof(object)))
            .ThrowsExactly<ObjectDisposedException>();
        await Assert.That(exception!.ObjectName).IsEqualTo("System.Object");
    }

    [Test]
    public async Task NotDisposed_Name()
    {
        C.State.NotDisposed(false, "instance");
        var exception = await Assert
            .That(() => C.State.NotDisposed(true, "instance"))
            .ThrowsExactly<ObjectDisposedException>();
        await Assert.That(exception!.ObjectName).IsEqualTo("instance");
    }

    [Test]
    public async Task NotDisposed_Generic()
    {
        C.State.NotDisposed<object>(false);
        var exception = await Assert
            .That(() => C.State.NotDisposed<object>(true))
            .ThrowsExactly<ObjectDisposedException>();
        await Assert.That(exception!.ObjectName).IsEqualTo("System.Object");
    }

    [Test]
    public async Task IsInitialized_Instance()
    {
        var instance = new object();
        await Assert.That(C.State.IsInitialized(true, instance)).IsSameReferenceAs(instance);
        var exception = await Assert
            .That(() => C.State.IsInitialized<object>(false, new object()))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("System.Object");
    }

    [Test]
    public async Task IsInitialized_Type()
    {
        C.State.IsInitialized(true, typeof(object));
        var exception = await Assert
            .That(() => C.State.IsInitialized(false, typeof(object)))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("System.Object");
    }

    [Test]
    public async Task IsInitialized_Name()
    {
        C.State.IsInitialized(true, "instance");
        var exception = await Assert
            .That(() => C.State.IsInitialized(false, "instance"))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("instance");
    }

    [Test]
    public async Task IsInitialized_Generic()
    {
        C.State.IsInitialized<object>(true);
        var exception = await Assert
            .That(() => C.State.IsInitialized<object>(false))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("System.Object");
    }

    [Test]
    public async Task IsMutable_Instance()
    {
        var instance = new object();
        await Assert.That(C.State.IsMutable(true, instance)).IsSameReferenceAs(instance);
        var exception = await Assert
            .That(() => C.State.IsMutable<object>(false, new object()))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("System.Object");
    }

    [Test]
    public async Task IsMutable_Type()
    {
        C.State.IsMutable(true, typeof(object));
        var exception = await Assert
            .That(() => C.State.IsMutable(false, typeof(object)))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("System.Object");
    }

    [Test]
    public async Task IsMutable_Name()
    {
        C.State.IsMutable(true, "instance");
        var exception = await Assert
            .That(() => C.State.IsMutable(false, "instance"))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("instance");
    }

    [Test]
    public async Task IsMutable_Generic()
    {
        C.State.IsMutable<object>(true);
        var exception = await Assert
            .That(() => C.State.IsMutable<object>(false))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("System.Object");
    }
}
