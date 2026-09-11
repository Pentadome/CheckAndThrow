using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg.Range;

public class PositiveTests
{
    [Test]
    public async Task NotPositive_object_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotPositive(7, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.ActualValue).IsEqualTo((object)7);
        await Assert
            .That(exception!.Message)
            .StartsWith("input should have a positive value, but was $7.");
    }

    [Test]
    public async Task NotPositive_Generic_object_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotPositive<object>(7, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.ActualValue).IsEqualTo((object)7);
        await Assert
            .That(exception!.Message)
            .StartsWith("input should have a positive value, but was $7.");
    }
}
