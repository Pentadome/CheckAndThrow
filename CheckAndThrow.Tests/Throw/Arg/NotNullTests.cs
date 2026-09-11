using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg;

public class NotNullTests
{
    [Test]
    public async Task IsNull_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNull("input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task IsNull_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNull<object>("input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }
}
