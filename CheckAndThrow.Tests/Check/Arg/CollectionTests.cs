using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check.Arg;

public class CollectionTests
{
    [Test]
    public async Task IsNotNullOrEmpty_ReturnsOriginal()
    {
        var collection = new[] { 1 };
        await Assert.That(C.Arg.IsNotNullOrEmpty(collection)).IsSameReferenceAs(collection);
    }

    [Test]
    public async Task IsNotNullOrEmpty_RejectsInvalid()
    {
        var exception = await Assert
            .That(() => C.Arg.IsNotNullOrEmpty(Array.Empty<int>(), "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task IsNotNullOrEmpty_RejectsNull()
    {
        var exception = await Assert
            .That(() => C.Arg.IsNotNullOrEmpty((int[])null!, "input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task IsNotNullAndHasNoNulls_ReturnsOriginal()
    {
        var collection = new object[] { 1 };
        await Assert.That(C.Arg.IsNotNullAndHasNoNulls(collection)).IsSameReferenceAs(collection);
    }

    [Test]
    public async Task IsNotNullAndHasNoNulls_RejectsInvalid()
    {
        var exception = await Assert
            .That(() => C.Arg.IsNotNullAndHasNoNulls(new object?[] { 1, null }, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task IsNotNullAndHasNoNulls_RejectsNull()
    {
        var exception = await Assert
            .That(() => C.Arg.IsNotNullAndHasNoNulls((int[])null!, "input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task HasCount_ReturnsOriginal()
    {
        var collection = new[] { 1, 2 };
        await Assert.That(C.Arg.HasCount(collection, 2)).IsSameReferenceAs(collection);
    }

    [Test]
    public async Task HasCount_RejectsInvalid()
    {
        var exception = await Assert
            .That(() => C.Arg.HasCount(new[] { 1 }, 2, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task HasCount_RejectsNull()
    {
        var exception = await Assert
            .That(() => C.Arg.HasCount((int[])null!, 2, "input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task HasMinCount_ReturnsOriginal()
    {
        var collection = new[] { 1, 2 };
        await Assert.That(C.Arg.HasMinCount(collection, 2)).IsSameReferenceAs(collection);
    }

    [Test]
    public async Task HasMinCount_RejectsInvalid()
    {
        var exception = await Assert
            .That(() => C.Arg.HasMinCount(new[] { 1 }, 2, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task HasMinCount_RejectsNull()
    {
        var exception = await Assert
            .That(() => C.Arg.HasMinCount((int[])null!, 2, "input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task HasMaxCount_ReturnsOriginal()
    {
        var collection = new[] { 1, 2 };
        await Assert.That(C.Arg.HasMaxCount(collection, 2)).IsSameReferenceAs(collection);
    }

    [Test]
    public async Task HasMaxCount_RejectsInvalid()
    {
        var exception = await Assert
            .That(() => C.Arg.HasMaxCount(new[] { 1, 2, 3 }, 2, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task HasMaxCount_RejectsNull()
    {
        var exception = await Assert
            .That(() => C.Arg.HasMaxCount((int[])null!, 2, "input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task Contains_ReturnsOriginal()
    {
        var collection = new List<int> { 1 };
        await Assert.That(C.Arg.Contains(collection, 1)).IsSameReferenceAs(collection);
    }

    [Test]
    public async Task Contains_RejectsInvalid()
    {
        var exception = await Assert
            .That(() => C.Arg.Contains(new List<int> { 2 }, 1, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task Contains_RejectsNull()
    {
        var exception = await Assert
            .That(() => C.Arg.Contains((List<int>)null!, 1, "input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(2)]
    public async Task HasValidIndex_Collection(int index)
    {
        if (index >= 0 && index < 2)
        {
            await Assert.That(C.Arg.HasValidIndex(index, new[] { 1, 2 })).IsEqualTo(index);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.HasValidIndex(index, new[] { 1, 2 }))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("index");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)index);
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(2)]
    public async Task HasValidIndex_Size(int index)
    {
        if (index >= 0 && index < 2)
        {
            await Assert.That(C.Arg.HasValidIndex(index, 2)).IsEqualTo(index);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.HasValidIndex(index, 2))
                .ThrowsExactly<ArgumentOutOfRangeException>();
            await Assert.That(exception!.ParamName).IsEqualTo("index");
            await Assert.That(exception!.ActualValue).IsEqualTo((object)index);
        }
    }

    [Test]
    public async Task EmptyEnumerableAndCountBounds()
    {
        var empty = Array.Empty<object>();
        await Assert.That(C.Arg.IsNotNullAndHasNoNulls(empty)).IsSameReferenceAs(empty);
        await Assert.That(C.Arg.HasCount(empty, 0)).IsSameReferenceAs(empty);
        await Assert.That(C.Arg.HasMinCount(empty, 0)).IsSameReferenceAs(empty);
        await Assert.That(C.Arg.HasMaxCount(empty, 1)).IsSameReferenceAs(empty);
        var exception = await Assert
            .That(() => C.Arg.HasValidIndex(0, 0, "input"))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }
}
