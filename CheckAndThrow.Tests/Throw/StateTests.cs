using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw;

public class StateTests
{
    [Test]
    public async Task Disposed_object()
    {
        var exception = await Assert
            .That(() => Th.State.Disposed(new object()))
            .ThrowsExactly<ObjectDisposedException>();
        await Assert.That(exception!.ObjectName).IsEqualTo("System.Object");
    }

    [Test]
    public async Task Disposed_Type()
    {
        var exception = await Assert
            .That(() => Th.State.Disposed(typeof(object)))
            .ThrowsExactly<ObjectDisposedException>();
        await Assert.That(exception!.ObjectName).IsEqualTo("System.Object");
    }

    [Test]
    public async Task Disposed_string()
    {
        var exception = await Assert
            .That(() => Th.State.Disposed("instance"))
            .ThrowsExactly<ObjectDisposedException>();
        await Assert.That(exception!.ObjectName).IsEqualTo("instance");
    }

    [Test]
    public async Task Disposed_Generic_NoArguments()
    {
        var exception = await Assert
            .That(() => Th.State.Disposed<object>())
            .ThrowsExactly<ObjectDisposedException>();
        await Assert.That(exception!.ObjectName).IsEqualTo("System.Object");
    }

    [Test]
    public async Task NotInitialized_object()
    {
        var exception = await Assert
            .That(() => Th.State.NotInitialized(new object()))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("System.Object is not initialized.");
    }

    [Test]
    public async Task NotInitialized_Type()
    {
        var exception = await Assert
            .That(() => Th.State.NotInitialized(typeof(object)))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("System.Object is not initialized.");
    }

    [Test]
    public async Task NotInitialized_string()
    {
        var exception = await Assert
            .That(() => Th.State.NotInitialized("instance"))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("instance is not initialized.");
    }

    [Test]
    public async Task NotInitialized_Generic_NoArguments()
    {
        var exception = await Assert
            .That(() => Th.State.NotInitialized<object>())
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("System.Object is not initialized.");
    }

    [Test]
    public async Task NotMutable_object()
    {
        var exception = await Assert
            .That(() => Th.State.NotMutable(new object()))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("System.Object is not mutable.");
    }

    [Test]
    public async Task NotMutable_Type()
    {
        var exception = await Assert
            .That(() => Th.State.NotMutable(typeof(object)))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("System.Object is not mutable.");
    }

    [Test]
    public async Task NotMutable_string()
    {
        var exception = await Assert
            .That(() => Th.State.NotMutable("instance"))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("instance is not mutable.");
    }

    [Test]
    public async Task NotMutable_Generic_NoArguments()
    {
        var exception = await Assert
            .That(() => Th.State.NotMutable<object>())
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("System.Object is not mutable.");
    }

    [Test]
    public async Task Disposed_RejectsNull_object()
    {
        var exception = await Assert
            .That(() => Th.State.Disposed((object)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instance");
    }

    [Test]
    public async Task Disposed_RejectsNull_Type()
    {
        var exception = await Assert
            .That(() => Th.State.Disposed((Type)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("type");
    }

    [Test]
    public async Task Disposed_RejectsNull_string()
    {
        var exception = await Assert
            .That(() => Th.State.Disposed((string)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instanceName");
    }

    [Test]
    public async Task Disposed_RejectsEmptyName()
    {
        var exception = await Assert
            .That(() => Th.State.Disposed(" "))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instanceName");
    }

    [Test]
    public async Task NotInitialized_RejectsNull_object()
    {
        var exception = await Assert
            .That(() => Th.State.NotInitialized((object)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instance");
    }

    [Test]
    public async Task NotInitialized_RejectsNull_Type()
    {
        var exception = await Assert
            .That(() => Th.State.NotInitialized((Type)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("type");
    }

    [Test]
    public async Task NotInitialized_RejectsNull_string()
    {
        var exception = await Assert
            .That(() => Th.State.NotInitialized((string)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instanceName");
    }

    [Test]
    public async Task NotInitialized_RejectsEmptyName()
    {
        var exception = await Assert
            .That(() => Th.State.NotInitialized(" "))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instanceName");
    }

    [Test]
    public async Task NotMutable_RejectsNull_object()
    {
        var exception = await Assert
            .That(() => Th.State.NotMutable((object)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instance");
    }

    [Test]
    public async Task NotMutable_RejectsNull_Type()
    {
        var exception = await Assert
            .That(() => Th.State.NotMutable((Type)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instanceType");
    }

    [Test]
    public async Task NotMutable_RejectsNull_string()
    {
        var exception = await Assert
            .That(() => Th.State.NotMutable((string)null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instanceName");
    }

    [Test]
    public async Task NotMutable_RejectsEmptyName()
    {
        var exception = await Assert
            .That(() => Th.State.NotMutable(" "))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("instanceName");
    }
}
