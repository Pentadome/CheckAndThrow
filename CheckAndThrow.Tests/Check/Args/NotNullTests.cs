using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check.Args;

public class NotNullTests
{
    [Test]
    public async Task NotNull_2_ReturnsTuple()
    {
        var a1 = new object();
        var a2 = new object();
        var result = C.Args.NotNull<object, object>(a1, a2);
        await Assert.That(result.Item1).IsSameReferenceAs(a1);
        await Assert.That(result.Item2).IsSameReferenceAs(a2);
    }

    [Test]
    public async Task NotNull_2_NullAt1()
    {
        var exception = await Assert
            .That(() => C.Args.NotNull<object, object>((object)null!, new object(), "arg1", "arg2"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("arg1");
    }

    [Test]
    public async Task NotNull_2_NullAt2()
    {
        var exception = await Assert
            .That(() => C.Args.NotNull<object, object>(new object(), (object)null!, "arg1", "arg2"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("arg2");
    }

    [Test]
    public async Task NotNull_2_FirstFailure()
    {
        var exception = await Assert
            .That(() => C.Args.NotNull<object, object>(null!, null!, "arg1", "arg2"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("arg1");
    }

    [Test]
    public async Task NotNull_2_TrailingString()
    {
        var result = C.Args.NotNull(new object(), "tail");
        await Assert.That(result.Item2).IsEqualTo("tail");
    }

    [Test]
    public async Task NotNull_2_CallerName()
    {
        object? missing = null;
        var exception = await Assert
            .That(() => C.Args.NotNull<object, object>(new object(), missing!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("missing");
    }

    [Test]
    public async Task NotNull_3_ReturnsTuple()
    {
        var a1 = new object();
        var a2 = new object();
        var a3 = new object();
        var result = C.Args.NotNull<object, object, object>(a1, a2, a3);
        await Assert.That(result.Item1).IsSameReferenceAs(a1);
        await Assert.That(result.Item2).IsSameReferenceAs(a2);
        await Assert.That(result.Item3).IsSameReferenceAs(a3);
    }

    [Test]
    public async Task NotNull_3_NullAt1()
    {
        var exception = await Assert
            .That(() =>
                C.Args.NotNull<object, object, object>(
                    (object)null!,
                    new object(),
                    new object(),
                    "arg1",
                    "arg2",
                    "arg3"
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("arg1");
    }

    [Test]
    public async Task NotNull_3_NullAt2()
    {
        var exception = await Assert
            .That(() =>
                C.Args.NotNull<object, object, object>(
                    new object(),
                    (object)null!,
                    new object(),
                    "arg1",
                    "arg2",
                    "arg3"
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("arg2");
    }

    [Test]
    public async Task NotNull_3_NullAt3()
    {
        var exception = await Assert
            .That(() =>
                C.Args.NotNull<object, object, object>(
                    new object(),
                    new object(),
                    (object)null!,
                    "arg1",
                    "arg2",
                    "arg3"
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("arg3");
    }

    [Test]
    public async Task NotNull_3_FirstFailure()
    {
        var exception = await Assert
            .That(() =>
                C.Args.NotNull<object, object, object>(null!, null!, null!, "arg1", "arg2", "arg3")
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("arg1");
    }

    [Test]
    public async Task NotNull_3_TrailingString()
    {
        var result = C.Args.NotNull(new object(), new object(), "tail");
        await Assert.That(result.Item3).IsEqualTo("tail");
    }

    [Test]
    public async Task NotNull_3_CallerName()
    {
        object? missing = null;
        var exception = await Assert
            .That(() =>
                C.Args.NotNull<object, object, object>(new object(), new object(), missing!)
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("missing");
    }

    [Test]
    public async Task NotNull_4_ReturnsTuple()
    {
        var a1 = new object();
        var a2 = new object();
        var a3 = new object();
        var a4 = new object();
        var result = C.Args.NotNull<object, object, object, object>(a1, a2, a3, a4);
        await Assert.That(result.Item1).IsSameReferenceAs(a1);
        await Assert.That(result.Item2).IsSameReferenceAs(a2);
        await Assert.That(result.Item3).IsSameReferenceAs(a3);
        await Assert.That(result.Item4).IsSameReferenceAs(a4);
    }

    [Test]
    public async Task NotNull_4_NullAt1()
    {
        var exception = await Assert
            .That(() =>
                C.Args.NotNull<object, object, object, object>(
                    (object)null!,
                    new object(),
                    new object(),
                    new object(),
                    "arg1",
                    "arg2",
                    "arg3",
                    "arg4"
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("arg1");
    }

    [Test]
    public async Task NotNull_4_NullAt2()
    {
        var exception = await Assert
            .That(() =>
                C.Args.NotNull<object, object, object, object>(
                    new object(),
                    (object)null!,
                    new object(),
                    new object(),
                    "arg1",
                    "arg2",
                    "arg3",
                    "arg4"
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("arg2");
    }

    [Test]
    public async Task NotNull_4_NullAt3()
    {
        var exception = await Assert
            .That(() =>
                C.Args.NotNull<object, object, object, object>(
                    new object(),
                    new object(),
                    (object)null!,
                    new object(),
                    "arg1",
                    "arg2",
                    "arg3",
                    "arg4"
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("arg3");
    }

    [Test]
    public async Task NotNull_4_NullAt4()
    {
        var exception = await Assert
            .That(() =>
                C.Args.NotNull<object, object, object, object>(
                    new object(),
                    new object(),
                    new object(),
                    (object)null!,
                    "arg1",
                    "arg2",
                    "arg3",
                    "arg4"
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("arg4");
    }

    [Test]
    public async Task NotNull_4_FirstFailure()
    {
        var exception = await Assert
            .That(() =>
                C.Args.NotNull<object, object, object, object>(
                    null!,
                    null!,
                    null!,
                    null!,
                    "arg1",
                    "arg2",
                    "arg3",
                    "arg4"
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("arg1");
    }

    [Test]
    public async Task NotNull_4_TrailingString()
    {
        var result = C.Args.NotNull(new object(), new object(), new object(), "tail");
        await Assert.That(result.Item4).IsEqualTo("tail");
    }

    [Test]
    public async Task NotNull_4_CallerName()
    {
        object? missing = null;
        var exception = await Assert
            .That(() =>
                C.Args.NotNull<object, object, object, object>(
                    new object(),
                    new object(),
                    new object(),
                    missing!
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("missing");
    }
}
