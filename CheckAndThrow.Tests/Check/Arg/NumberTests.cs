using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check.Arg;

public class NumberTests
{
    [Test]
    public async Task NotNaN_ReturnsValuesAndRejectsNaN()
    {
        await Assert.That(C.Arg.NotNaN(1.5)).IsEqualTo(1.5);
        await Assert.That(C.Arg.NotNaN(1.5F)).IsEqualTo(1.5F);

        var doubleException = await Assert
            .That(() => C.Arg.NotNaN(double.NaN, "doubleValue"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(doubleException!.ParamName).IsEqualTo("doubleValue");
        await Assert.That(double.IsNaN((double)doubleException.ActualValue!)).IsTrue();

        var floatException = await Assert
            .That(() => C.Arg.NotNaN(float.NaN, "floatValue"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(floatException!.ParamName).IsEqualTo("floatValue");
        await Assert.That(float.IsNaN((float)floatException.ActualValue!)).IsTrue();
    }

    [Test]
    public async Task NotInfinity_ReturnsFiniteAndNaNValuesAndRejectsInfinity()
    {
        await Assert.That(double.IsNaN(C.Arg.NotInfinity(double.NaN))).IsTrue();
        await Assert.That(float.IsNaN(C.Arg.NotInfinity(float.NaN))).IsTrue();

        var doubleException = await Assert
            .That(() => C.Arg.NotInfinity(double.PositiveInfinity, "doubleValue"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(doubleException!.ParamName).IsEqualTo("doubleValue");
        await Assert.That(doubleException.ActualValue).IsEqualTo((object)double.PositiveInfinity);

        var floatException = await Assert
            .That(() => C.Arg.NotInfinity(float.NegativeInfinity, "floatValue"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(floatException!.ParamName).IsEqualTo("floatValue");
        await Assert.That(floatException.ActualValue).IsEqualTo((object)float.NegativeInfinity);
    }

    [Test]
    public async Task RealNumber_ReturnsFiniteValuesAndRejectsNaNAndInfinity()
    {
        await Assert.That(C.Arg.RealNumber(1.5)).IsEqualTo(1.5);
        await Assert.That(C.Arg.RealNumber(1.5F)).IsEqualTo(1.5F);

        foreach (
            var value in new[] { double.NaN, double.PositiveInfinity, double.NegativeInfinity }
        )
        {
            var exception = await Assert
                .That(() => C.Arg.RealNumber(value, "doubleValue"))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("doubleValue");
        }

        foreach (var value in new[] { float.NaN, float.PositiveInfinity, float.NegativeInfinity })
        {
            var exception = await Assert
                .That(() => C.Arg.RealNumber(value, "floatValue"))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("floatValue");
        }
    }
}
