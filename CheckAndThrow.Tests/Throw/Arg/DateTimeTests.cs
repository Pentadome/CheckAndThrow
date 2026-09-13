using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg;

public class DateTimeTests
{
    [Test]
    public async Task NotInPast_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotInPast("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument must be in the past.");
    }

    [Test]
    public async Task NotInPast_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotInPast<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument must be in the past.");
    }

    [Test]
    public async Task NotInPast_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotInPast("input", 7, 3))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be in the past, but was 7. Current time is 3.");
    }

    [Test]
    public async Task NotInPast_Generic_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotInPast<object>("input", 7, 3))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be in the past, but was 7. Current time is 3.");
    }

    [Test]
    public async Task NotInFuture_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotInFuture("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument must be in the future.");
    }

    [Test]
    public async Task NotInFuture_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotInFuture<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument must be in the future.");
    }

    [Test]
    public async Task NotInFuture_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotInFuture("input", 7, 3))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be in the future, but was 7. Current time is 3.");
    }

    [Test]
    public async Task NotInFuture_Generic_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotInFuture<object>("input", 7, 3))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be in the future, but was 7. Current time is 3.");
    }

    [Test]
    public async Task NotLaterThan_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotLaterThan("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be later than the comparison value.");
    }

    [Test]
    public async Task NotLaterThan_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotLaterThan<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be later than the comparison value.");
    }

    [Test]
    public async Task NotLaterThan_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotLaterThan("input", 3, 7))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be later than 3, but was 7.");
    }

    [Test]
    public async Task NotLaterThan_Generic_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotLaterThan<object>("input", 3, 7))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be later than 3, but was 7.");
    }

    [Test]
    public async Task NotEarlierThan_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotEarlierThan("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be earlier than the comparison value.");
    }

    [Test]
    public async Task NotEarlierThan_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotEarlierThan<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be earlier than the comparison value.");
    }

    [Test]
    public async Task NotEarlierThan_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotEarlierThan("input", 3, 7))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be earlier than 3, but was 7.");
    }

    [Test]
    public async Task NotEarlierThan_Generic_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotEarlierThan<object>("input", 3, 7))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be earlier than 3, but was 7.");
    }
}
