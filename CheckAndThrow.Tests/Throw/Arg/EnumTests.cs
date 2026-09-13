using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg;

public class EnumTests
{
    [Test]
    public async Task InvalidEnumValue_Type_int_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidEnumValue(typeof(DayOfWeek), 99, "input"))
            .ThrowsExactly<System.ComponentModel.InvalidEnumArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Data["enumType"]).IsEqualTo((object)typeof(DayOfWeek));
        await Assert.That(exception!.Data["enumValue"]).IsEqualTo((object)99);
    }

    [Test]
    public async Task InvalidEnumValue_Generic_Type_int_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidEnumValue<object>(typeof(DayOfWeek), 99, "input"))
            .ThrowsExactly<System.ComponentModel.InvalidEnumArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Data["enumType"]).IsEqualTo((object)typeof(DayOfWeek));
        await Assert.That(exception!.Data["enumValue"]).IsEqualTo((object)99);
    }

    [Test]
    public async Task InvalidEnumValue_Type_Enum_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidEnumValue(typeof(DayOfWeek), (Enum)(DayOfWeek)99, "input"))
            .ThrowsExactly<System.ComponentModel.InvalidEnumArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Data["enumType"]).IsEqualTo((object)typeof(DayOfWeek));
        await Assert.That(exception!.Data["enumValue"]).IsEqualTo((object)99);
    }

    [Test]
    public async Task InvalidEnumValue_Generic_Type_Enum_string()
    {
        var exception = await Assert
            .That(() =>
                Th.Arg.InvalidEnumValue<object>(typeof(DayOfWeek), (Enum)(DayOfWeek)99, "input")
            )
            .ThrowsExactly<System.ComponentModel.InvalidEnumArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Data["enumType"]).IsEqualTo((object)typeof(DayOfWeek));
        await Assert.That(exception!.Data["enumValue"]).IsEqualTo((object)99);
    }

    [Test]
    public async Task InvalidEnumValue_Type_string_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidEnumValue(typeof(DayOfWeek), "missing", "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Data["enumType"]).IsEqualTo((object)typeof(DayOfWeek));
        await Assert.That(exception!.Data["enumValue"]).IsEqualTo((object)"missing");
    }

    [Test]
    public async Task InvalidEnumValue_Generic_Type_string_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidEnumValue<object>(typeof(DayOfWeek), "missing", "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Data["enumType"]).IsEqualTo((object)typeof(DayOfWeek));
        await Assert.That(exception!.Data["enumValue"]).IsEqualTo((object)"missing");
    }

    [Test]
    public async Task MissesAnyOfTheFlags_Type_string_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.MissesAnyOfTheFlags(typeof(DayOfWeek), "Monday", "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Data["enumType"]).IsEqualTo((object)typeof(DayOfWeek));
        await Assert.That(exception!.Data["flags"]).IsEqualTo((object)"Monday");
    }

    [Test]
    public async Task MissesAnyOfTheFlags_Generic_Type_string_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.MissesAnyOfTheFlags<object>(typeof(DayOfWeek), "Monday", "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Data["enumType"]).IsEqualTo((object)typeof(DayOfWeek));
        await Assert.That(exception!.Data["flags"]).IsEqualTo((object)"Monday");
    }

    [Test]
    public async Task MissesAllFlags_Type_string_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.MissesAllFlags(typeof(DayOfWeek), "Monday", "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Data["enumType"]).IsEqualTo((object)typeof(DayOfWeek));
        await Assert.That(exception!.Data["flags"]).IsEqualTo((object)"Monday");
    }

    [Test]
    public async Task MissesAllFlags_Generic_Type_string_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.MissesAllFlags<object>(typeof(DayOfWeek), "Monday", "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Data["enumType"]).IsEqualTo((object)typeof(DayOfWeek));
        await Assert.That(exception!.Data["flags"]).IsEqualTo((object)"Monday");
    }
}
