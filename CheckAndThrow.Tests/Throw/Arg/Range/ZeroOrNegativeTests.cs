using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg.Range;

public class ZeroOrNegativeTests
{
    [Test]
    public async Task NotZeroOrNegative_object_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotZeroOrNegative(7, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.ActualValue).IsEqualTo((object)7);
        await Assert
            .That(exception!.Message)
            .StartsWith("input should have a zero or negative value, but was $7.");
    }

    [Test]
    public async Task NotZeroOrNegative_Generic_object_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotZeroOrNegative<object>(7, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.ActualValue).IsEqualTo((object)7);
        await Assert
            .That(exception!.Message)
            .StartsWith("input should have a zero or negative value, but was $7.");
    }
}
