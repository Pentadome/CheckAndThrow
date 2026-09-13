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
    public async Task Initialized_Instance()
    {
        var instance = new object();
        await Assert.That(C.State.Initialized(true, instance)).IsSameReferenceAs(instance);
        var exception = await Assert
            .That(() => C.State.Initialized<object>(false, new object()))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("System.Object");
    }

    [Test]
    public async Task Initialized_Type()
    {
        C.State.Initialized(true, typeof(object));
        var exception = await Assert
            .That(() => C.State.Initialized(false, typeof(object)))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("System.Object");
    }

    [Test]
    public async Task Initialized_Name()
    {
        C.State.Initialized(true, "instance");
        var exception = await Assert
            .That(() => C.State.Initialized(false, "instance"))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("instance");
    }

    [Test]
    public async Task Initialized_Generic()
    {
        C.State.Initialized<object>(true);
        var exception = await Assert
            .That(() => C.State.Initialized<object>(false))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("System.Object");
    }

    [Test]
    public async Task Mutable_Instance()
    {
        var instance = new object();
        await Assert.That(C.State.Mutable(true, instance)).IsSameReferenceAs(instance);
        var exception = await Assert
            .That(() => C.State.Mutable<object>(false, new object()))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("System.Object");
    }

    [Test]
    public async Task Mutable_Type()
    {
        C.State.Mutable(true, typeof(object));
        var exception = await Assert
            .That(() => C.State.Mutable(false, typeof(object)))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("System.Object");
    }

    [Test]
    public async Task Mutable_Name()
    {
        C.State.Mutable(true, "instance");
        var exception = await Assert
            .That(() => C.State.Mutable(false, "instance"))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("instance");
    }

    [Test]
    public async Task Mutable_Generic()
    {
        C.State.Mutable<object>(true);
        var exception = await Assert
            .That(() => C.State.Mutable<object>(false))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("System.Object");
    }
}
