using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg;

public class GuidTests
{
    [Test]
    public async Task IsGuidEmpty_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsGuidEmpty("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument cannot be an empty Guid.");
    }

    [Test]
    public async Task IsGuidEmpty_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsGuidEmpty<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument cannot be an empty Guid.");
    }
}
