using C = CheckAndThrow.Check;
using Flags = CheckAndThrow.Tests.Check.Arg.EnumTests.Flags;
using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw;

public class OverloadParityTests
{
    [Test]
    public async Task Range_PlainVariantsAndGenericTwins()
    {
        Action[] calls =
        [
            () => Th.Arg.NotNegative("input"),
            () => Th.Arg.NotNegative<int>("input"),
            () => Th.Arg.NotPositive("input"),
            () => Th.Arg.NotPositive<int>("input"),
            () => Th.Arg.NotZeroOrNegative("input"),
            () => Th.Arg.NotZeroOrNegative<int>("input"),
            () => Th.Arg.NotZeroOrPositive("input"),
            () => Th.Arg.NotZeroOrPositive<int>("input"),
            () => Th.Arg.OutOfRange("input"),
            () => Th.Arg.OutOfRange<int>("input"),
        ];

        foreach (var call in calls)
        {
            var exception = await Assert.That(call).ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("input");
            await Assert.That(exception.ActualValue).IsNull();
        }

        // Existing object-valued signature remains selectable for string values.
        var rich = await Assert
            .That(() => Th.Arg.NotPositive((object)"invalid", "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(rich!.ActualValue).IsEqualTo((object)"invalid");
    }

    [Test]
    public async Task State_PlainVariantsAndGenericRichTwins()
    {
        var disposed = await Assert
            .That(() => Th.State.Disposed())
            .ThrowsExactly<ObjectDisposedException>();
        await Assert.That(disposed!.ObjectName).IsEqualTo("Object");

        Action[] richDisposed =
        [
            () => Th.State.Disposed<int>(new object()),
            () => Th.State.Disposed<int>(typeof(object)),
            () => Th.State.Disposed<int>("System.Object"),
        ];
        foreach (var call in richDisposed)
        {
            var exception = await Assert.That(call).ThrowsExactly<ObjectDisposedException>();
            await Assert.That(exception!.ObjectName).IsEqualTo("System.Object");
        }

        Action[] plainAndRichInitialized =
        [
            () => Th.State.NotInitialized(),
            () => Th.State.NotInitialized<int>(new object()),
            () => Th.State.NotInitialized<int>(typeof(object)),
            () => Th.State.NotInitialized<int>("System.Object"),
        ];
        foreach (var call in plainAndRichInitialized)
        {
            var exception = await Assert.That(call).ThrowsExactly<InvalidOperationException>();
            await Assert.That(exception!.Message).Contains("not initialized");
        }

        Action[] plainAndRichMutable =
        [
            () => Th.State.NotMutable(),
            () => Th.State.NotMutable<int>(new object()),
            () => Th.State.NotMutable<int>(typeof(object)),
            () => Th.State.NotMutable<int>("System.Object"),
        ];
        foreach (var call in plainAndRichMutable)
        {
            var exception = await Assert.That(call).ThrowsExactly<InvalidOperationException>();
            await Assert.That(exception!.Message).Contains("not mutable");
        }
    }

    [Test]
    public async Task Collection_NewVariantsPreserveParameterAndDetails()
    {
        Action[] calls =
        [
            () => Th.Arg.HasNullValue("input", 2),
            () => Th.Arg.HasNullValue<int>("input", 2),
            () => Th.Arg.DoesNotContain<int>("input"),
            () => Th.Arg.DoesNotContain("input", 42),
            () => Th.Arg.DoesNotContain<int>("input", 42),
        ];
        foreach (var call in calls)
        {
            var exception = await Assert.That(call).ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("input");
        }

        var missing = await Assert
            .That(() => C.Arg.Contains(new[] { 1, 2 }, 42, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(missing!.Message).Contains("42");
        var nullItem = await Assert
            .That(() => C.Arg.NotNullAndHasNoNulls(new object?[] { 1, null }, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(nullItem!.Message).Contains("index 1");
    }

    [Test]
    public async Task Enum_PlainAndValueAwareVariants()
    {
        Action[] calls =
        [
            () => Th.Arg.InvalidEnumValue("input"),
            () => Th.Arg.InvalidEnumValue<int>("input"),
            () => Th.Arg.MissesAnyOfTheFlags("input"),
            () => Th.Arg.MissesAnyOfTheFlags<int>("input"),
            () => Th.Arg.MissesAllFlags("input"),
            () => Th.Arg.MissesAllFlags<int>("input"),
            () =>
                Th.Arg.MissesAnyOfTheFlags(typeof(DayOfWeek), "Monday", DayOfWeek.Sunday, "input"),
            () =>
                Th.Arg.MissesAnyOfTheFlags<int>(
                    typeof(DayOfWeek),
                    "Monday",
                    DayOfWeek.Sunday,
                    "input"
                ),
            () => Th.Arg.MissesAllFlags(typeof(DayOfWeek), "Monday", DayOfWeek.Sunday, "input"),
            () =>
                Th.Arg.MissesAllFlags<int>(typeof(DayOfWeek), "Monday", DayOfWeek.Sunday, "input"),
        ];
        foreach (var call in calls)
        {
            var exception = await Assert.That(call).ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("input");
        }

        var flagError = await Assert
            .That(() => C.Arg.HasAllFlags(Flags.One, Flags.Both, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(flagError!.Data["value"]).IsEqualTo((object)Flags.One);
    }

    [Test]
    public async Task Number_RichVariantsExposeActualValue()
    {
        Action[] calls =
        [
            () => Th.Arg.NaN(double.NaN, "input"),
            () => Th.Arg.NaN<int>(double.NaN, "input"),
            () => Th.Arg.Infinity(double.NegativeInfinity, "input"),
            () => Th.Arg.Infinity<int>(double.NegativeInfinity, "input"),
        ];
        foreach (var call in calls)
        {
            var exception = await Assert.That(call).ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("input");
            await Assert.That(exception.ActualValue).IsNotNull();
        }

        var negativeInfinity = await Assert
            .That(() => C.Arg.RealNumber(double.NegativeInfinity, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(negativeInfinity!.ActualValue).IsEqualTo((object)double.NegativeInfinity);
    }

    [Test]
    public async Task Guards_UseAvailableFailureContext()
    {
        var count = await Assert
            .That(() => C.Arg.HasCount(new[] { 1 }, 2, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(count!.Message).Contains("was 1 but should be 2");
        var few = await Assert
            .That(() => C.Arg.HasMinCount(new[] { 1 }, 2, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(few!.Message).Contains("had 1 items");
        var many = await Assert
            .That(() => C.Arg.HasMaxCount(new[] { 1, 2 }, 1, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(many!.Message).Contains("had 2 items");
        var type = await Assert
            .That(() => C.Arg.AssignableFrom<string>(typeof(int), "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(type!.Message).Contains("System.String");
        await Assert.That(type.Message).Contains("System.Int32");

        object instance = new List<int>();
        var disposed = await Assert
            .That(() => C.State.NotDisposed(true, instance))
            .ThrowsExactly<ObjectDisposedException>();
        await Assert.That(disposed!.ObjectName).IsEqualTo(typeof(List<int>).FullName);
        var initialized = await Assert
            .That(() => C.State.Initialized(false, instance))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(initialized!.Message).Contains(typeof(List<int>).FullName!);
        var mutable = await Assert
            .That(() => C.State.Mutable(false, instance))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(mutable!.Message).Contains(typeof(List<int>).FullName!);
    }
}
