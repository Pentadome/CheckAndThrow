using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check.Arg.Range;

public class RangeTests
{
    [Test]
    [Arguments(1, true)]
    [Arguments(2, true)]
    [Arguments(3, true)]
    [Arguments(0, false)]
    [Arguments(4, false)]
    public async Task InRange_int(int number, bool accepted)
    {
        var value = (int)number;
        if (accepted)
        {
            await Assert.That(C.Arg.InRange(value, (int)1, (int)3)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InRange(value, (int)1, (int)3))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(1, true)]
    [Arguments(2, true)]
    [Arguments(3, true)]
    [Arguments(0, false)]
    [Arguments(4, false)]
    public async Task InRange_double(int number, bool accepted)
    {
        var value = (double)number;
        if (accepted)
        {
            await Assert.That(C.Arg.InRange(value, (double)1, (double)3)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InRange(value, (double)1, (double)3))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(1, true)]
    [Arguments(2, true)]
    [Arguments(3, true)]
    [Arguments(0, false)]
    [Arguments(4, false)]
    public async Task InRange_long(int number, bool accepted)
    {
        var value = (long)number;
        if (accepted)
        {
            await Assert.That(C.Arg.InRange(value, (long)1, (long)3)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InRange(value, (long)1, (long)3))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(1, true)]
    [Arguments(2, true)]
    [Arguments(3, true)]
    [Arguments(0, false)]
    [Arguments(4, false)]
    public async Task InRange_float(int number, bool accepted)
    {
        var value = (float)number;
        if (accepted)
        {
            await Assert.That(C.Arg.InRange(value, (float)1, (float)3)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InRange(value, (float)1, (float)3))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(1, true)]
    [Arguments(2, true)]
    [Arguments(3, true)]
    [Arguments(0, false)]
    [Arguments(4, false)]
    public async Task InRange_uint(int number, bool accepted)
    {
        var value = (uint)number;
        if (accepted)
        {
            await Assert.That(C.Arg.InRange(value, (uint)1, (uint)3)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InRange(value, (uint)1, (uint)3))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(1, true)]
    [Arguments(2, true)]
    [Arguments(3, true)]
    [Arguments(0, false)]
    [Arguments(4, false)]
    public async Task InRange_ulong(int number, bool accepted)
    {
        var value = (ulong)number;
        if (accepted)
        {
            await Assert.That(C.Arg.InRange(value, (ulong)1, (ulong)3)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InRange(value, (ulong)1, (ulong)3))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(1, true)]
    [Arguments(2, true)]
    [Arguments(3, true)]
    [Arguments(0, false)]
    [Arguments(4, false)]
    public async Task InRange_decimal(int number, bool accepted)
    {
        var value = (decimal)number;
        if (accepted)
        {
            await Assert.That(C.Arg.InRange(value, (decimal)1, (decimal)3)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InRange(value, (decimal)1, (decimal)3))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(1, true)]
    [Arguments(2, true)]
    [Arguments(3, true)]
    [Arguments(0, false)]
    [Arguments(4, false)]
    public async Task InRange_byte(int number, bool accepted)
    {
        var value = (byte)number;
        if (accepted)
        {
            await Assert.That(C.Arg.InRange(value, (byte)1, (byte)3)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InRange(value, (byte)1, (byte)3))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(1, true)]
    [Arguments(2, true)]
    [Arguments(3, true)]
    [Arguments(0, false)]
    [Arguments(4, false)]
    public async Task InRange_sbyte(int number, bool accepted)
    {
        var value = (sbyte)number;
        if (accepted)
        {
            await Assert.That(C.Arg.InRange(value, (sbyte)1, (sbyte)3)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InRange(value, (sbyte)1, (sbyte)3))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(1, true)]
    [Arguments(2, true)]
    [Arguments(3, true)]
    [Arguments(0, false)]
    [Arguments(4, false)]
    public async Task InRange_nint(int number, bool accepted)
    {
        var value = (nint)number;
        if (accepted)
        {
            await Assert.That(C.Arg.InRange(value, (nint)1, (nint)3)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InRange(value, (nint)1, (nint)3))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    [Arguments(1, true)]
    [Arguments(2, true)]
    [Arguments(3, true)]
    [Arguments(0, false)]
    [Arguments(4, false)]
    public async Task InRange_Generic(int number, bool accepted)
    {
        var value = (int)number;
        if (accepted)
        {
            await Assert.That(C.Arg.InRange<int>(value, (int)1, (int)3)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InRange<int>(value, (int)1, (int)3))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)value);
        }
    }

    [Test]
    public async Task InRange_double_InfinityBounds()
    {
        await Assert
            .That(
                C.Arg.InRange(
                    double.PositiveInfinity,
                    double.NegativeInfinity,
                    double.PositiveInfinity
                )
            )
            .IsEqualTo(double.PositiveInfinity);
        var exception = await Assert
            .That(() => C.Arg.InRange(double.PositiveInfinity, (double)0, (double)1, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task InRange_double_RejectsNaN()
    {
        await Assert
            .That(() => C.Arg.InRange(double.NaN, (double)0, (double)1, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task InRange_float_InfinityBounds()
    {
        await Assert
            .That(
                C.Arg.InRange(
                    float.PositiveInfinity,
                    float.NegativeInfinity,
                    float.PositiveInfinity
                )
            )
            .IsEqualTo(float.PositiveInfinity);
        var exception = await Assert
            .That(() => C.Arg.InRange(float.PositiveInfinity, (float)0, (float)1, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task InRange_float_RejectsNaN()
    {
        await Assert
            .That(() => C.Arg.InRange(float.NaN, (float)0, (float)1, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task InRange_Generic_RejectsNaN()
    {
        var exception = await Assert
            .That(() => C.Arg.InRange<double>(double.NaN, 0, 1, paramName: "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }
}
