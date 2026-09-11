using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check.Arg.Range;

public class ZeroOrNegativeTests
{
    [Test]
    [Arguments(-1, true)]
    [Arguments(0, true)]
    [Arguments(1, false)]
    public async Task ZeroOrNegative_int(int number, bool accepted)
    {
        var value = (int)number;
        if (accepted)
        {
            await Assert.That(C.Arg.ZeroOrNegative(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.ZeroOrNegative(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, true)]
    [Arguments(0, true)]
    [Arguments(1, false)]
    public async Task ZeroOrNegative_double(int number, bool accepted)
    {
        var value = (double)number;
        if (accepted)
        {
            await Assert.That(C.Arg.ZeroOrNegative(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.ZeroOrNegative(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, true)]
    [Arguments(0, true)]
    [Arguments(1, false)]
    public async Task ZeroOrNegative_long(int number, bool accepted)
    {
        var value = (long)number;
        if (accepted)
        {
            await Assert.That(C.Arg.ZeroOrNegative(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.ZeroOrNegative(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, true)]
    [Arguments(0, true)]
    [Arguments(1, false)]
    public async Task ZeroOrNegative_float(int number, bool accepted)
    {
        var value = (float)number;
        if (accepted)
        {
            await Assert.That(C.Arg.ZeroOrNegative(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.ZeroOrNegative(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, true)]
    [Arguments(0, true)]
    [Arguments(1, false)]
    public async Task ZeroOrNegative_decimal(int number, bool accepted)
    {
        var value = (decimal)number;
        if (accepted)
        {
            await Assert.That(C.Arg.ZeroOrNegative(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.ZeroOrNegative(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, true)]
    [Arguments(0, true)]
    [Arguments(1, false)]
    public async Task ZeroOrNegative_sbyte(int number, bool accepted)
    {
        var value = (sbyte)number;
        if (accepted)
        {
            await Assert.That(C.Arg.ZeroOrNegative(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.ZeroOrNegative(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, true)]
    [Arguments(0, true)]
    [Arguments(1, false)]
    public async Task ZeroOrNegative_nint(int number, bool accepted)
    {
        var value = (nint)number;
        if (accepted)
        {
            await Assert.That(C.Arg.ZeroOrNegative(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.ZeroOrNegative(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, true)]
    [Arguments(0, true)]
    [Arguments(1, false)]
    public async Task ZeroOrNegative_Generic(int number, bool accepted)
    {
        var value = (int)number;
        if (accepted)
        {
            await Assert.That(C.Arg.ZeroOrNegative<int>(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.ZeroOrNegative<int>(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    public async Task ZeroOrNegative_double_RejectsInfinities()
    {
        await Assert
            .That(() => C.Arg.ZeroOrNegative(double.NegativeInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert
            .That(() => C.Arg.ZeroOrNegative(double.PositiveInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ZeroOrNegative_double_RejectsNaN()
    {
        await Assert
            .That(() => C.Arg.ZeroOrNegative(double.NaN, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ZeroOrNegative_float_RejectsInfinities()
    {
        await Assert
            .That(() => C.Arg.ZeroOrNegative(float.NegativeInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert
            .That(() => C.Arg.ZeroOrNegative(float.PositiveInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ZeroOrNegative_float_RejectsNaN()
    {
        await Assert
            .That(() => C.Arg.ZeroOrNegative(float.NaN, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ZeroOrNegative_Generic_RejectsNaN()
    {
        var exception = await Assert
            .That(() => C.Arg.ZeroOrNegative<double>(double.NaN, paramName: "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(() => C.Arg.ZeroOrNegative<double>(double.NegativeInfinity, paramName: "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }
}
