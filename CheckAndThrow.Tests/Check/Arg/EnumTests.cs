using System.ComponentModel;
using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check.Arg;

public class EnumTests
{
    [Flags]
    public enum Flags
    {
        None = 0,
        One = 1,
        Two = 2,
        Both = 3,
    }

    [Flags]
    public enum LongFlags : long
    {
        One = 1,
        Two = 2,
    }

    [Flags]
    public enum ShortFlags : short
    {
        One = 1,
        Two = 2,
    }

    [Flags]
    public enum ByteFlags : byte
    {
        One = 1,
        Two = 2,
    }

    [Flags]
    public enum UnsignedFlags : ulong
    {
        High = 1UL << 63,
    }

    [Flags]
    public enum UIntFlags : uint
    {
        High = 1U << 31,
    }

    [Test]
    public async Task ValidEnumValue_DeclaredAndUndefined()
    {
        await Assert.That(C.Arg.ValidEnumValue(Flags.None)).IsEqualTo(Flags.None);
        await Assert.That(C.Arg.ValidEnumValue(Flags.Both)).IsEqualTo(Flags.Both);
        var exception = await Assert
            .That(() => C.Arg.ValidEnumValue((Flags)4, "input"))
            .ThrowsExactly<InvalidEnumArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task ValidEnumValueName_CaseSensitive()
    {
        await Assert.That(C.Arg.ValidEnumValueName<Flags>("One")).IsEqualTo(Flags.One);
        var exception = await Assert
            .That(() => C.Arg.ValidEnumValueName<Flags>("one", "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(() => C.Arg.ValidEnumValueName<Flags>("1"))
            .ThrowsExactly<ArgumentException>();
        await Assert
            .That(() => C.Arg.ValidEnumValueName<Flags>(null))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task ValidEnumValueNameIgnoreCase()
    {
        await Assert.That(C.Arg.ValidEnumValueNameIgnoreCase<Flags>("oNe")).IsEqualTo(Flags.One);
        var exception = await Assert
            .That(() => C.Arg.ValidEnumValueNameIgnoreCase<Flags>("missing", "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(() => C.Arg.ValidEnumValueNameIgnoreCase<Flags>(null))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task HasAllFlags_ZeroCompositeAndMissing()
    {
        await Assert.That(C.Arg.HasAllFlags(Flags.One, Flags.None)).IsEqualTo(Flags.One);
        await Assert.That(C.Arg.HasAllFlags(Flags.Both, Flags.Both)).IsEqualTo(Flags.Both);
        var exception = await Assert
            .That(() => C.Arg.HasAllFlags(Flags.One, Flags.Both, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task HasAnyOfFlags_ZeroCompositeAndMissing()
    {
        await Assert.That(C.Arg.HasAnyOfFlags(Flags.One, Flags.None)).IsEqualTo(Flags.One);
        await Assert.That(C.Arg.HasAnyOfFlags(Flags.One, Flags.Both)).IsEqualTo(Flags.One);
        var exception = await Assert
            .That(() => C.Arg.HasAnyOfFlags(Flags.One, Flags.Two, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(C.Arg.HasAnyOfFlags(LongFlags.One, LongFlags.One))
            .IsEqualTo(LongFlags.One);
        await Assert
            .That(C.Arg.HasAnyOfFlags(ShortFlags.One, ShortFlags.One))
            .IsEqualTo(ShortFlags.One);
        await Assert
            .That(C.Arg.HasAnyOfFlags(ByteFlags.One, ByteFlags.One))
            .IsEqualTo(ByteFlags.One);
        await Assert
            .That(() => C.Arg.HasAnyOfFlags(LongFlags.One, LongFlags.Two))
            .ThrowsExactly<ArgumentException>();
        await Assert
            .That(() => C.Arg.HasAnyOfFlags(ShortFlags.One, ShortFlags.Two))
            .ThrowsExactly<ArgumentException>();
        await Assert
            .That(() => C.Arg.HasAnyOfFlags(ByteFlags.One, ByteFlags.Two))
            .ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task HasAnyOfFlags_UnsignedHighBit_IsValid()
    {
        await Assert
            .That(C.Arg.HasAnyOfFlags(UnsignedFlags.High, UnsignedFlags.High))
            .IsEqualTo(UnsignedFlags.High);
        await Assert
            .That(C.Arg.HasAnyOfFlags(UnsignedFlags.High, (UnsignedFlags)0))
            .IsEqualTo(UnsignedFlags.High);
        await Assert
            .That(() => C.Arg.HasAnyOfFlags(UnsignedFlags.High, (UnsignedFlags)1))
            .ThrowsExactly<ArgumentException>();
        await Assert
            .That(C.Arg.HasAnyOfFlags(UIntFlags.High, UIntFlags.High))
            .IsEqualTo(UIntFlags.High);
        await Assert
            .That(C.Arg.HasAnyOfFlags(UIntFlags.High, (UIntFlags)0))
            .IsEqualTo(UIntFlags.High);
        await Assert
            .That(() => C.Arg.HasAnyOfFlags(UIntFlags.High, (UIntFlags)1))
            .ThrowsExactly<ArgumentException>();
    }
}
