using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg.Range;

public class ZeroOrPositiveTests
{
    [Test]
    public async Task NotZeroOrPositive_object_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotZeroOrPositive(7, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.ActualValue).IsEqualTo((object)7);
        await Assert
            .That(exception!.Message)
            .StartsWith("input should have a zero or positive value, but was $7.");
    }

    [Test]
    public async Task NotZeroOrPositive_Generic_object_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotZeroOrPositive<object>(7, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.ActualValue).IsEqualTo((object)7);
        await Assert
            .That(exception!.Message)
            .StartsWith("input should have a zero or positive value, but was $7.");
    }
}
