using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check.Arg;

public class NotNullTests
{
    [Test]
    public async Task NotNull_ReturnsReferenceAndValue()
    {
        var value = new object();
        await Assert.That(C.Arg.NotNull(value)).IsSameReferenceAs(value);
        await Assert.That(C.Arg.NotNull(0)).IsEqualTo(0);
    }

    [Test]
    public async Task NotNull_InfersName()
    {
        object? value = null;
        var exception = await Assert
            .That(() => C.Arg.NotNull(value))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("value");
    }

    [Test]
    public async Task NotNull_ExplicitName()
    {
        var exception = await Assert
            .That(() => C.Arg.NotNull((int?)null, "input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }
}
