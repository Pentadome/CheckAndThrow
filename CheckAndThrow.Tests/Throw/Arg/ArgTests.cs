using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg;

public class ArgTests
{
    [Test]
    public async Task Exception_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.Exception("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument is invalid.");
    }

    [Test]
    public async Task Exception_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.Exception<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument is invalid.");
    }

    [Test]
    public async Task Exception_string_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.Exception("input", "custom message"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("custom message");
    }

    [Test]
    public async Task Exception_Generic_string_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.Exception<object>("input", "custom message"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("custom message");
    }
}
