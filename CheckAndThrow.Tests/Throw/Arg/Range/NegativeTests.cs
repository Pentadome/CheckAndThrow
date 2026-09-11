using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg.Range;

public class NegativeTests
{
    [Test]
    public async Task NotNegative_object_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotNegative(7, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.ActualValue).IsEqualTo((object)7);
        await Assert
            .That(exception!.Message)
            .StartsWith("input should have a negative value, but was $7.");
    }

    [Test]
    public async Task NotNegative_Generic_object_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotNegative<object>(7, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.ActualValue).IsEqualTo((object)7);
        await Assert
            .That(exception!.Message)
            .StartsWith("input should have a negative value, but was $7.");
    }
}
