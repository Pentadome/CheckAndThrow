using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg;

public class CollectionTests
{
    [Test]
    public async Task IsEmpty_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsEmpty("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument can not be empty.");
    }

    [Test]
    public async Task IsEmpty_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsEmpty<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument can not be empty.");
    }

    [Test]
    public async Task HasNullValue_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.HasNullValue("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument can not have a null value");
    }

    [Test]
    public async Task HasNullValue_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.HasNullValue<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument can not have a null value");
    }

    [Test]
    public async Task InvalidCount_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidCount("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument has an invalid count.");
    }

    [Test]
    public async Task InvalidCount_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidCount<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument has an invalid count.");
    }

    [Test]
    public async Task InvalidCount_object_object_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidCount(7, 3, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument has an invalid count, was 7 but should be 3.");
    }

    [Test]
    public async Task InvalidCount_Generic_object_object_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidCount<object>(7, 3, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument has an invalid count, was 7 but should be 3.");
    }

    [Test]
    public async Task TooFewItems_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.TooFewItems("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument has too few items.");
    }

    [Test]
    public async Task TooFewItems_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.TooFewItems<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument has too few items.");
    }

    [Test]
    public async Task TooFewItems_object_object_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.TooFewItems(7, 3, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument has too few items, had 7 items but should have atleast 3 items.");
    }

    [Test]
    public async Task TooFewItems_Generic_object_object_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.TooFewItems<object>(7, 3, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument has too few items, had 7 items but should have atleast 3 items.");
    }

    [Test]
    public async Task TooManyItems_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.TooManyItems("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument has too many items.");
    }

    [Test]
    public async Task TooManyItems_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.TooManyItems<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument has too many items.");
    }

    [Test]
    public async Task TooManyItems_object_object_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.TooManyItems(7, 3, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument has too many items, had 7 items but should have at most 3 items."
            );
    }

    [Test]
    public async Task TooManyItems_Generic_object_object_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.TooManyItems<object>(7, 3, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument has too many items, had 7 items but should have at most 3 items."
            );
    }

    [Test]
    public async Task DoesNotContain_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.DoesNotContain("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument does not contain the required item.");
    }

    [Test]
    public async Task IsInvalidIndex_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsInvalidIndex("input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.ActualValue).IsNull();
    }

    [Test]
    public async Task IsInvalidIndex_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsInvalidIndex<object>("input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.ActualValue).IsNull();
    }

    [Test]
    public async Task IsInvalidIndex_object_object_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsInvalidIndex(7, 3, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.ActualValue).IsEqualTo((object)7);
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument has an out of range index value. Must be 0 or higher and less than 3, but was 7."
            );
    }

    [Test]
    public async Task IsInvalidIndex_Generic_object_object_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsInvalidIndex<object>(7, 3, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.ActualValue).IsEqualTo((object)7);
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument has an out of range index value. Must be 0 or higher and less than 3, but was 7."
            );
    }
}
