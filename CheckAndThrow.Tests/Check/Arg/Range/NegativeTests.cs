using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check.Arg.Range;

public class NegativeTests
{
    [Test]
    [Arguments(-1, true)]
    [Arguments(0, false)]
    [Arguments(1, false)]
    public async Task Negative_int(int number, bool accepted)
    {
        var value = (int)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Negative(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Negative(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, true)]
    [Arguments(0, false)]
    [Arguments(1, false)]
    public async Task Negative_double(int number, bool accepted)
    {
        var value = (double)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Negative(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Negative(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, true)]
    [Arguments(0, false)]
    [Arguments(1, false)]
    public async Task Negative_long(int number, bool accepted)
    {
        var value = (long)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Negative(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Negative(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, true)]
    [Arguments(0, false)]
    [Arguments(1, false)]
    public async Task Negative_float(int number, bool accepted)
    {
        var value = (float)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Negative(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Negative(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, true)]
    [Arguments(0, false)]
    [Arguments(1, false)]
    public async Task Negative_decimal(int number, bool accepted)
    {
        var value = (decimal)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Negative(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Negative(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, true)]
    [Arguments(0, false)]
    [Arguments(1, false)]
    public async Task Negative_sbyte(int number, bool accepted)
    {
        var value = (sbyte)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Negative(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Negative(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, true)]
    [Arguments(0, false)]
    [Arguments(1, false)]
    public async Task Negative_nint(int number, bool accepted)
    {
        var value = (nint)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Negative(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Negative(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, true)]
    [Arguments(0, false)]
    [Arguments(1, false)]
    public async Task Negative_Generic(int number, bool accepted)
    {
        var value = (int)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Negative<int>(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Negative<int>(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    public async Task Negative_double_RejectsInfinities()
    {
        await Assert
            .That(() => C.Arg.Negative(double.NegativeInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert
            .That(() => C.Arg.Negative(double.PositiveInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Negative_double_RejectsNaN()
    {
        await Assert
            .That(() => C.Arg.Negative(double.NaN, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Negative_float_RejectsInfinities()
    {
        await Assert
            .That(() => C.Arg.Negative(float.NegativeInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert
            .That(() => C.Arg.Negative(float.PositiveInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Negative_float_RejectsNaN()
    {
        await Assert
            .That(() => C.Arg.Negative(float.NaN, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task NegativeOrNegativeInfinity_AcceptsNegativeInfinity()
    {
        await Assert.That(C.Arg.NegativeOrNegativeInfinity(double.NegativeInfinity)).IsEqualTo(double.NegativeInfinity);
        await Assert.That(C.Arg.NegativeOrNegativeInfinity(float.NegativeInfinity)).IsEqualTo(float.NegativeInfinity);
        await Assert.That(C.Arg.NegativeOrNegativeInfinity<double>(double.NegativeInfinity)).IsEqualTo(double.NegativeInfinity);
        await Assert.That(() => C.Arg.NegativeOrNegativeInfinity(double.NaN)).ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(() => C.Arg.NegativeOrNegativeInfinity(0.0)).ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Negative_Generic_RejectsNegativeZeroAndNaN()
    {
        var exception = await Assert
            .That(() => C.Arg.Negative<double>(-0.0, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(() => C.Arg.Negative<double>(double.NaN, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert
            .That(() => C.Arg.Negative<double>(double.NegativeInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }
}
