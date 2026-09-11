using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check.Arg.Range;

public class ZeroOrPositiveTests
{
    [Test]
    [Arguments(-1, false)]
    [Arguments(0, true)]
    [Arguments(1, true)]
    public async Task ZeroOrPositive_int(int number, bool accepted)
    {
        var value = (int)number;
        if (accepted)
        {
            await Assert.That(C.Arg.ZeroOrPositive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.ZeroOrPositive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, false)]
    [Arguments(0, true)]
    [Arguments(1, true)]
    public async Task ZeroOrPositive_double(int number, bool accepted)
    {
        var value = (double)number;
        if (accepted)
        {
            await Assert.That(C.Arg.ZeroOrPositive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.ZeroOrPositive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, false)]
    [Arguments(0, true)]
    [Arguments(1, true)]
    public async Task ZeroOrPositive_long(int number, bool accepted)
    {
        var value = (long)number;
        if (accepted)
        {
            await Assert.That(C.Arg.ZeroOrPositive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.ZeroOrPositive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, false)]
    [Arguments(0, true)]
    [Arguments(1, true)]
    public async Task ZeroOrPositive_float(int number, bool accepted)
    {
        var value = (float)number;
        if (accepted)
        {
            await Assert.That(C.Arg.ZeroOrPositive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.ZeroOrPositive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, false)]
    [Arguments(0, true)]
    [Arguments(1, true)]
    public async Task ZeroOrPositive_decimal(int number, bool accepted)
    {
        var value = (decimal)number;
        if (accepted)
        {
            await Assert.That(C.Arg.ZeroOrPositive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.ZeroOrPositive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, false)]
    [Arguments(0, true)]
    [Arguments(1, true)]
    public async Task ZeroOrPositive_sbyte(int number, bool accepted)
    {
        var value = (sbyte)number;
        if (accepted)
        {
            await Assert.That(C.Arg.ZeroOrPositive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.ZeroOrPositive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, false)]
    [Arguments(0, true)]
    [Arguments(1, true)]
    public async Task ZeroOrPositive_nint(int number, bool accepted)
    {
        var value = (nint)number;
        if (accepted)
        {
            await Assert.That(C.Arg.ZeroOrPositive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.ZeroOrPositive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, false)]
    [Arguments(0, true)]
    [Arguments(1, true)]
    public async Task ZeroOrPositive_Generic(int number, bool accepted)
    {
        var value = (int)number;
        if (accepted)
        {
            await Assert.That(C.Arg.ZeroOrPositive<int>(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.ZeroOrPositive<int>(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    public async Task ZeroOrPositive_double_RejectsInfinities()
    {
        await Assert
            .That(() => C.Arg.ZeroOrPositive(double.PositiveInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert
            .That(() => C.Arg.ZeroOrPositive(double.NegativeInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ZeroOrPositive_double_RejectsNaN()
    {
        await Assert
            .That(() => C.Arg.ZeroOrPositive(double.NaN, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ZeroOrPositive_float_RejectsInfinities()
    {
        await Assert
            .That(() => C.Arg.ZeroOrPositive(float.PositiveInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert
            .That(() => C.Arg.ZeroOrPositive(float.NegativeInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ZeroOrPositive_float_RejectsNaN()
    {
        await Assert
            .That(() => C.Arg.ZeroOrPositive(float.NaN, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ZeroOrPositive_Generic_RejectsNaN()
    {
        var exception = await Assert
            .That(() => C.Arg.ZeroOrPositive<double>(double.NaN, paramName: "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(() => C.Arg.ZeroOrPositive<double>(double.PositiveInfinity, paramName: "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }
}
