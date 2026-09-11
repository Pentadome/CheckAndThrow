using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check.Arg;

public class GuidTests
{
    [Test]
    public async Task IsNotGuidEmpty_ReturnsOriginal()
    {
        var value = Guid.Parse("a928a3cd-8510-40f0-bb15-afed90f1b205");
        await Assert.That(C.Arg.IsNotGuidEmpty(value)).IsEqualTo(value);
    }

    [Test]
    public async Task IsNotGuidEmpty_RejectsEmpty()
    {
        var exception = await Assert
            .That(() => C.Arg.IsNotGuidEmpty(Guid.Empty, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }
}
