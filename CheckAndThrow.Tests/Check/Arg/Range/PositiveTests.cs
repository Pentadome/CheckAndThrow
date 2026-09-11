using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check.Arg.Range;

public class PositiveTests
{
    [Test]
    [Arguments(-1, false)]
    [Arguments(0, false)]
    [Arguments(1, true)]
    public async Task Positive_int(int number, bool accepted)
    {
        var value = (int)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Positive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Positive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, false)]
    [Arguments(0, false)]
    [Arguments(1, true)]
    public async Task Positive_double(int number, bool accepted)
    {
        var value = (double)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Positive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Positive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, false)]
    [Arguments(0, false)]
    [Arguments(1, true)]
    public async Task Positive_long(int number, bool accepted)
    {
        var value = (long)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Positive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Positive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, false)]
    [Arguments(0, false)]
    [Arguments(1, true)]
    public async Task Positive_float(int number, bool accepted)
    {
        var value = (float)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Positive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Positive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(0, false)]
    [Arguments(1, true)]
    public async Task Positive_uint(int number, bool accepted)
    {
        var value = (uint)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Positive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Positive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(0, false)]
    [Arguments(1, true)]
    public async Task Positive_ulong(int number, bool accepted)
    {
        var value = (ulong)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Positive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Positive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, false)]
    [Arguments(0, false)]
    [Arguments(1, true)]
    public async Task Positive_decimal(int number, bool accepted)
    {
        var value = (decimal)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Positive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Positive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(0, false)]
    [Arguments(1, true)]
    public async Task Positive_byte(int number, bool accepted)
    {
        var value = (byte)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Positive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Positive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, false)]
    [Arguments(0, false)]
    [Arguments(1, true)]
    public async Task Positive_sbyte(int number, bool accepted)
    {
        var value = (sbyte)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Positive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Positive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, false)]
    [Arguments(0, false)]
    [Arguments(1, true)]
    public async Task Positive_nint(int number, bool accepted)
    {
        var value = (nint)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Positive(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Positive(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(-1, false)]
    [Arguments(0, false)]
    [Arguments(1, true)]
    public async Task Positive_Generic(int number, bool accepted)
    {
        var value = (int)number;
        if (accepted)
        {
            await Assert.That(C.Arg.Positive<int>(value)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.Positive<int>(value))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    public async Task Positive_double_RejectsInfinities()
    {
        await Assert
            .That(() => C.Arg.Positive(double.PositiveInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert
            .That(() => C.Arg.Positive(double.NegativeInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Positive_double_RejectsNaN()
    {
        await Assert
            .That(() => C.Arg.Positive(double.NaN, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Positive_float_RejectsInfinities()
    {
        await Assert
            .That(() => C.Arg.Positive(float.PositiveInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert
            .That(() => C.Arg.Positive(float.NegativeInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Positive_float_RejectsNaN()
    {
        await Assert
            .That(() => C.Arg.Positive(float.NaN, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task PositiveOrInfinity_AcceptsPositiveInfinity()
    {
        await Assert.That(C.Arg.PositiveOrInfinity(double.PositiveInfinity)).IsEqualTo(double.PositiveInfinity);
        await Assert.That(C.Arg.PositiveOrInfinity(float.PositiveInfinity)).IsEqualTo(float.PositiveInfinity);
        await Assert.That(C.Arg.PositiveOrInfinity<double>(double.PositiveInfinity)).IsEqualTo(double.PositiveInfinity);
        await Assert.That(() => C.Arg.PositiveOrInfinity(double.NaN)).ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(() => C.Arg.PositiveOrInfinity(0.0)).ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task Positive_Generic_RejectsNaN()
    {
        var exception = await Assert
            .That(() => C.Arg.Positive<double>(double.NaN, paramName: "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(() => C.Arg.Positive<double>(double.PositiveInfinity, paramName: "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }
}
