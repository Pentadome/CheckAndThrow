using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg;

public class DateTimeTests
{
    [Test]
    public async Task IsNotInPast_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotInPast("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument must be in the past.");
    }

    [Test]
    public async Task IsNotInPast_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotInPast<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument must be in the past.");
    }

    [Test]
    public async Task IsNotInPast_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotInPast("input", 7, 3))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be in the past, but was 7. Current time is 3.");
    }

    [Test]
    public async Task IsNotInPast_Generic_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotInPast<object>("input", 7, 3))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be in the past, but was 7. Current time is 3.");
    }

    [Test]
    public async Task IsNotInFuture_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotInFuture("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument must be in the future.");
    }

    [Test]
    public async Task IsNotInFuture_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotInFuture<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument must be in the future.");
    }

    [Test]
    public async Task IsNotInFuture_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotInFuture("input", 7, 3))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be in the future, but was 7. Current time is 3.");
    }

    [Test]
    public async Task IsNotInFuture_Generic_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotInFuture<object>("input", 7, 3))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be in the future, but was 7. Current time is 3.");
    }

    [Test]
    public async Task IsNotLaterThan_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotLaterThan("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be later than the comparison value.");
    }

    [Test]
    public async Task IsNotLaterThan_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotLaterThan<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be later than the comparison value.");
    }

    [Test]
    public async Task IsNotLaterThan_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotLaterThan("input", 3, 7))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be later than 3, but was 7.");
    }

    [Test]
    public async Task IsNotLaterThan_Generic_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotLaterThan<object>("input", 3, 7))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be later than 3, but was 7.");
    }

    [Test]
    public async Task IsNotEarlierThan_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotEarlierThan("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be earlier than the comparison value.");
    }

    [Test]
    public async Task IsNotEarlierThan_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotEarlierThan<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be earlier than the comparison value.");
    }

    [Test]
    public async Task IsNotEarlierThan_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotEarlierThan("input", 3, 7))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be earlier than 3, but was 7.");
    }

    [Test]
    public async Task IsNotEarlierThan_Generic_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotEarlierThan<object>("input", 3, 7))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument must be earlier than 3, but was 7.");
    }
}
