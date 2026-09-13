using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg;

public class NumberTests
{
    [Test]
    public async Task NaN_ThrowsWithParameterName()
    {
        var exception = await Assert
            .That(() => Th.Arg.NaN("input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception.ActualValue).IsNull();
        await Assert
            .That(() => Th.Arg.NaN<object>("input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Infinity_ThrowsWithParameterName()
    {
        var exception = await Assert
            .That(() => Th.Arg.Infinity("input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception.ActualValue).IsNull();
        await Assert
            .That(() => Th.Arg.Infinity<object>("input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }
}
