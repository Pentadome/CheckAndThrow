using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg;

public class NumberTests
{
    [Test]
    public async Task IsNaN_ThrowsWithParameterName()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNaN("input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception.ActualValue).IsNull();
        await Assert
            .That(() => Th.Arg.IsNaN<object>("input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task IsInfinity_ThrowsWithParameterName()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsInfinity("input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception.ActualValue).IsNull();
        await Assert
            .That(() => Th.Arg.IsInfinity<object>("input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }
}
