using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw;

public class ThrowTests
{
    [Test]
    public async Task Unreachable_NoArguments()
    {
        var exception = await Assert
            .That(() => Th.Unreachable())
            .ThrowsExactly<System.Diagnostics.UnreachableException>();
        await Assert.That(exception!.Message).IsNotNullOrEmpty();
    }

    [Test]
    public async Task Unreachable_string()
    {
        var exception = await Assert
            .That(() => Th.Unreachable("custom message"))
            .ThrowsExactly<System.Diagnostics.UnreachableException>();
        await Assert.That(exception!.Message).IsEqualTo("custom message");
    }

    [Test]
    public async Task Unreachable_Generic_NoArguments()
    {
        var exception = await Assert
            .That(() => Th.Unreachable<object>())
            .ThrowsExactly<System.Diagnostics.UnreachableException>();
        await Assert.That(exception!.Message).IsNotNullOrEmpty();
    }

    [Test]
    public async Task Unreachable_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Unreachable<object>("custom message"))
            .ThrowsExactly<System.Diagnostics.UnreachableException>();
        await Assert.That(exception!.Message).IsEqualTo("custom message");
    }

    [Test]
    public async Task NotImplemented_NoArguments()
    {
        var exception = await Assert
            .That(() => Th.NotImplemented())
            .ThrowsExactly<NotImplementedException>();
        await Assert.That(exception!.Message).IsNotNullOrEmpty();
    }

    [Test]
    public async Task NotImplemented_string()
    {
        var exception = await Assert
            .That(() => Th.NotImplemented("custom message"))
            .ThrowsExactly<NotImplementedException>();
        await Assert.That(exception!.Message).IsEqualTo("custom message");
    }

    [Test]
    public async Task NotImplemented_Generic_NoArguments()
    {
        var exception = await Assert
            .That(() => Th.NotImplemented<object>())
            .ThrowsExactly<NotImplementedException>();
        await Assert.That(exception!.Message).IsNotNullOrEmpty();
    }

    [Test]
    public async Task NotImplemented_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.NotImplemented<object>("custom message"))
            .ThrowsExactly<NotImplementedException>();
        await Assert.That(exception!.Message).IsEqualTo("custom message");
    }

    [Test]
    public async Task NotSupported_NoArguments()
    {
        var exception = await Assert
            .That(() => Th.NotSupported())
            .ThrowsExactly<NotSupportedException>();
        await Assert.That(exception!.Message).IsNotNullOrEmpty();
    }

    [Test]
    public async Task NotSupported_string()
    {
        var exception = await Assert
            .That(() => Th.NotSupported("custom message"))
            .ThrowsExactly<NotSupportedException>();
        await Assert.That(exception!.Message).IsEqualTo("custom message");
    }

    [Test]
    public async Task NotSupported_Generic_NoArguments()
    {
        var exception = await Assert
            .That(() => Th.NotSupported<object>())
            .ThrowsExactly<NotSupportedException>();
        await Assert.That(exception!.Message).IsNotNullOrEmpty();
    }

    [Test]
    public async Task NotSupported_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.NotSupported<object>("custom message"))
            .ThrowsExactly<NotSupportedException>();
        await Assert.That(exception!.Message).IsEqualTo("custom message");
    }

    [Test]
    public async Task InvalidOperation_NoArguments()
    {
        var exception = await Assert
            .That(() => Th.InvalidOperation())
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsNotNullOrEmpty();
    }

    [Test]
    public async Task InvalidOperation_string()
    {
        var exception = await Assert
            .That(() => Th.InvalidOperation("custom message"))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("custom message");
    }

    [Test]
    public async Task InvalidOperation_Generic_NoArguments()
    {
        var exception = await Assert
            .That(() => Th.InvalidOperation<object>())
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsNotNullOrEmpty();
    }

    [Test]
    public async Task InvalidOperation_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.InvalidOperation<object>("custom message"))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).IsEqualTo("custom message");
    }

}
