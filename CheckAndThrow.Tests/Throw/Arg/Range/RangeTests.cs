using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg.Range;

public class RangeTests
{
    [Test]
    public async Task OutOfRange_object_object_object_string_string_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.OutOfRange(7, 3, 5, "input", "lower", "upper"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.ActualValue).IsEqualTo((object)7);
        await Assert
            .That(exception!.Message)
            .StartsWith("input must be 3(lower), 5(upper) or any value in between, but was 7.");
        await Assert.That(exception!.Data["min"]).IsEqualTo((object)3);
        await Assert.That(exception!.Data["max"]).IsEqualTo((object)5);
    }

    [Test]
    public async Task OutOfRange_Generic_object_object_object_string_string_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.OutOfRange<object>(7, 3, 5, "input", "lower", "upper"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.ActualValue).IsEqualTo((object)7);
        await Assert
            .That(exception!.Message)
            .StartsWith("input must be 3(lower), 5(upper) or any value in between, but was 7.");
        await Assert.That(exception!.Data["min"]).IsEqualTo((object)3);
        await Assert.That(exception!.Data["max"]).IsEqualTo((object)5);
    }

    [Test]
    public async Task OutOfRange_LiteralBoundsDoNotRepeatExpressions()
    {
        var value = 7;
        var exception = await Assert
            .That(() => Th.Arg.OutOfRange(value, 3, 5))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("value");
        await Assert
            .That(exception!.Message)
            .StartsWith("value must be 3, 5 or any value in between, but was 7.");
        await Assert.That(exception!.Data["value"]).IsEqualTo((object)7);
        await Assert.That(exception!.Data["minArgumentExpression"]).IsEqualTo((object)"3");
        await Assert.That(exception!.Data["maxArgumentExpression"]).IsEqualTo((object)"5");
    }
}
