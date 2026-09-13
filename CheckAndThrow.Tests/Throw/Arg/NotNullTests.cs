using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg;

public class NotNullTests
{
    [Test]
    public async Task Null_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.Null("input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task Null_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.Null<object>("input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }
}
