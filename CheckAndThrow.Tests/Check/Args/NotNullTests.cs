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

    [Test]
    public async Task NotNull_5_NullAt5()
    {
        var exception = await Assert
            .That(() =>
                C.Args.NotNull<object, object, object, object, object>(
                    new(),
                    new(),
                    new(),
                    new(),
                    null!,
                    "arg1",
                    "arg2",
                    "arg3",
                    "arg4",
                    "arg5"
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("arg5");
    }

    [Test]
    public async Task NotNull_6_NullAt6()
    {
        var exception = await Assert
            .That(() =>
                C.Args.NotNull<object, object, object, object, object, object>(
                    new(),
                    new(),
                    new(),
                    new(),
                    new(),
                    null!,
                    "arg1",
                    "arg2",
                    "arg3",
                    "arg4",
                    "arg5",
                    "arg6"
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("arg6");
    }

    [Test]
    public async Task NotNull_7_NullAt7()
    {
        var exception = await Assert
            .That(() =>
                C.Args.NotNull<object, object, object, object, object, object, object>(
                    new(),
                    new(),
                    new(),
                    new(),
                    new(),
                    new(),
                    null!,
                    "arg1",
                    "arg2",
                    "arg3",
                    "arg4",
                    "arg5",
                    "arg6",
                    "arg7"
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("arg7");
    }

    [Test]
    public async Task NotNull_8_NullAt8()
    {
        var exception = await Assert
            .That(() =>
                C.Args.NotNull<object, object, object, object, object, object, object, object>(
                    new(),
                    new(),
                    new(),
                    new(),
                    new(),
                    new(),
                    new(),
                    null!,
                    "arg1",
                    "arg2",
                    "arg3",
                    "arg4",
                    "arg5",
                    "arg6",
                    "arg7",
                    "arg8"
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("arg8");
    }

    [Test]
    public async Task NotNull_9_ReturnsTupleAndReportsEveryNullParameter()
    {
        object? arg1 = new object();
        object? arg2 = new object();
        object? arg3 = new object();
        object? arg4 = new object();
        object? arg5 = new object();
        object? arg6 = new object();
        object? arg7 = new object();
        object? arg8 = new object();
        object? arg9 = new object();

        var result = C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        await Assert.That(result.arg9).IsSameReferenceAs(arg9);

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg1 = null;
        var exception1 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception1!.ParamName).IsEqualTo("arg1");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg2 = null;
        var exception2 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception2!.ParamName).IsEqualTo("arg2");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg3 = null;
        var exception3 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception3!.ParamName).IsEqualTo("arg3");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg4 = null;
        var exception4 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception4!.ParamName).IsEqualTo("arg4");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg5 = null;
        var exception5 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception5!.ParamName).IsEqualTo("arg5");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg6 = null;
        var exception6 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception6!.ParamName).IsEqualTo("arg6");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg7 = null;
        var exception7 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception7!.ParamName).IsEqualTo("arg7");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg8 = null;
        var exception8 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception8!.ParamName).IsEqualTo("arg8");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg9 = null;
        var exception9 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception9!.ParamName).IsEqualTo("arg9");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg1 = null;
        arg2 = null;
        var firstFailure = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(firstFailure!.ParamName).IsEqualTo("arg1");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        var tail = "tail";
        var trailingStringResult = C.Args.NotNull(
            arg1,
            arg2,
            arg3,
            arg4,
            arg5,
            arg6,
            arg7,
            arg8,
            tail
        );
        await Assert.That(trailingStringResult.arg9).IsEqualTo(tail);
    }

    [Test]
    public async Task NotNull_10_ReturnsTupleAndReportsEveryNullParameter()
    {
        object? arg1 = new object();
        object? arg2 = new object();
        object? arg3 = new object();
        object? arg4 = new object();
        object? arg5 = new object();
        object? arg6 = new object();
        object? arg7 = new object();
        object? arg8 = new object();
        object? arg9 = new object();
        object? arg10 = new object();

        var result = C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        await Assert.That(result.arg10).IsSameReferenceAs(arg10);

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg1 = null;
        var exception1 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception1!.ParamName).IsEqualTo("arg1");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg2 = null;
        var exception2 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception2!.ParamName).IsEqualTo("arg2");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg3 = null;
        var exception3 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception3!.ParamName).IsEqualTo("arg3");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg4 = null;
        var exception4 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception4!.ParamName).IsEqualTo("arg4");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg5 = null;
        var exception5 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception5!.ParamName).IsEqualTo("arg5");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg6 = null;
        var exception6 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception6!.ParamName).IsEqualTo("arg6");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg7 = null;
        var exception7 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception7!.ParamName).IsEqualTo("arg7");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg8 = null;
        var exception8 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception8!.ParamName).IsEqualTo("arg8");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg9 = null;
        var exception9 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception9!.ParamName).IsEqualTo("arg9");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg10 = null;
        var exception10 = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception10!.ParamName).IsEqualTo("arg10");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg1 = null;
        arg2 = null;
        var firstFailure = await Assert
            .That(() => C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(firstFailure!.ParamName).IsEqualTo("arg1");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        var tail = "tail";
        var trailingStringResult = C.Args.NotNull(
            arg1,
            arg2,
            arg3,
            arg4,
            arg5,
            arg6,
            arg7,
            arg8,
            arg9,
            tail
        );
        await Assert.That(trailingStringResult.arg10).IsEqualTo(tail);
    }

    [Test]
    public async Task NotNull_11_ReturnsTupleAndReportsEveryNullParameter()
    {
        object? arg1 = new object();
        object? arg2 = new object();
        object? arg3 = new object();
        object? arg4 = new object();
        object? arg5 = new object();
        object? arg6 = new object();
        object? arg7 = new object();
        object? arg8 = new object();
        object? arg9 = new object();
        object? arg10 = new object();
        object? arg11 = new object();

        var result = C.Args.NotNull(
            arg1,
            arg2,
            arg3,
            arg4,
            arg5,
            arg6,
            arg7,
            arg8,
            arg9,
            arg10,
            arg11
        );
        await Assert.That(result.arg11).IsSameReferenceAs(arg11);

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg1 = null;
        var exception1 = await Assert
            .That(() =>
                C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception1!.ParamName).IsEqualTo("arg1");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg2 = null;
        var exception2 = await Assert
            .That(() =>
                C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception2!.ParamName).IsEqualTo("arg2");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg3 = null;
        var exception3 = await Assert
            .That(() =>
                C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception3!.ParamName).IsEqualTo("arg3");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg4 = null;
        var exception4 = await Assert
            .That(() =>
                C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception4!.ParamName).IsEqualTo("arg4");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg5 = null;
        var exception5 = await Assert
            .That(() =>
                C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception5!.ParamName).IsEqualTo("arg5");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg6 = null;
        var exception6 = await Assert
            .That(() =>
                C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception6!.ParamName).IsEqualTo("arg6");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg7 = null;
        var exception7 = await Assert
            .That(() =>
                C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception7!.ParamName).IsEqualTo("arg7");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg8 = null;
        var exception8 = await Assert
            .That(() =>
                C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception8!.ParamName).IsEqualTo("arg8");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg9 = null;
        var exception9 = await Assert
            .That(() =>
                C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception9!.ParamName).IsEqualTo("arg9");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg10 = null;
        var exception10 = await Assert
            .That(() =>
                C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception10!.ParamName).IsEqualTo("arg10");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg11 = null;
        var exception11 = await Assert
            .That(() =>
                C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception11!.ParamName).IsEqualTo("arg11");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg1 = null;
        arg2 = null;
        var firstFailure = await Assert
            .That(() =>
                C.Args.NotNull(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(firstFailure!.ParamName).IsEqualTo("arg1");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        var tail = "tail";
        var trailingStringResult = C.Args.NotNull(
            arg1,
            arg2,
            arg3,
            arg4,
            arg5,
            arg6,
            arg7,
            arg8,
            arg9,
            arg10,
            tail
        );
        await Assert.That(trailingStringResult.arg11).IsEqualTo(tail);
    }

    [Test]
    public async Task NotNull_12_ReturnsTupleAndReportsEveryNullParameter()
    {
        object? arg1 = new object();
        object? arg2 = new object();
        object? arg3 = new object();
        object? arg4 = new object();
        object? arg5 = new object();
        object? arg6 = new object();
        object? arg7 = new object();
        object? arg8 = new object();
        object? arg9 = new object();
        object? arg10 = new object();
        object? arg11 = new object();
        object? arg12 = new object();

        var result = C.Args.NotNull(
            arg1,
            arg2,
            arg3,
            arg4,
            arg5,
            arg6,
            arg7,
            arg8,
            arg9,
            arg10,
            arg11,
            arg12
        );
        await Assert.That(result.arg12).IsSameReferenceAs(arg12);

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg1 = null;
        var exception1 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception1!.ParamName).IsEqualTo("arg1");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg2 = null;
        var exception2 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception2!.ParamName).IsEqualTo("arg2");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg3 = null;
        var exception3 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception3!.ParamName).IsEqualTo("arg3");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg4 = null;
        var exception4 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception4!.ParamName).IsEqualTo("arg4");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg5 = null;
        var exception5 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception5!.ParamName).IsEqualTo("arg5");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg6 = null;
        var exception6 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception6!.ParamName).IsEqualTo("arg6");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg7 = null;
        var exception7 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception7!.ParamName).IsEqualTo("arg7");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg8 = null;
        var exception8 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception8!.ParamName).IsEqualTo("arg8");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg9 = null;
        var exception9 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception9!.ParamName).IsEqualTo("arg9");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg10 = null;
        var exception10 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception10!.ParamName).IsEqualTo("arg10");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg11 = null;
        var exception11 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception11!.ParamName).IsEqualTo("arg11");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg12 = null;
        var exception12 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception12!.ParamName).IsEqualTo("arg12");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg1 = null;
        arg2 = null;
        var firstFailure = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(firstFailure!.ParamName).IsEqualTo("arg1");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        var tail = "tail";
        var trailingStringResult = C.Args.NotNull(
            arg1,
            arg2,
            arg3,
            arg4,
            arg5,
            arg6,
            arg7,
            arg8,
            arg9,
            arg10,
            arg11,
            tail
        );
        await Assert.That(trailingStringResult.arg12).IsEqualTo(tail);
    }

    [Test]
    public async Task NotNull_13_ReturnsTupleAndReportsEveryNullParameter()
    {
        object? arg1 = new object();
        object? arg2 = new object();
        object? arg3 = new object();
        object? arg4 = new object();
        object? arg5 = new object();
        object? arg6 = new object();
        object? arg7 = new object();
        object? arg8 = new object();
        object? arg9 = new object();
        object? arg10 = new object();
        object? arg11 = new object();
        object? arg12 = new object();
        object? arg13 = new object();

        var result = C.Args.NotNull(
            arg1,
            arg2,
            arg3,
            arg4,
            arg5,
            arg6,
            arg7,
            arg8,
            arg9,
            arg10,
            arg11,
            arg12,
            arg13
        );
        await Assert.That(result.arg13).IsSameReferenceAs(arg13);

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg1 = null;
        var exception1 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception1!.ParamName).IsEqualTo("arg1");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg2 = null;
        var exception2 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception2!.ParamName).IsEqualTo("arg2");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg3 = null;
        var exception3 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception3!.ParamName).IsEqualTo("arg3");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg4 = null;
        var exception4 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception4!.ParamName).IsEqualTo("arg4");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg5 = null;
        var exception5 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception5!.ParamName).IsEqualTo("arg5");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg6 = null;
        var exception6 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception6!.ParamName).IsEqualTo("arg6");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg7 = null;
        var exception7 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception7!.ParamName).IsEqualTo("arg7");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg8 = null;
        var exception8 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception8!.ParamName).IsEqualTo("arg8");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg9 = null;
        var exception9 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception9!.ParamName).IsEqualTo("arg9");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg10 = null;
        var exception10 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception10!.ParamName).IsEqualTo("arg10");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg11 = null;
        var exception11 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception11!.ParamName).IsEqualTo("arg11");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg12 = null;
        var exception12 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception12!.ParamName).IsEqualTo("arg12");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg13 = null;
        var exception13 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception13!.ParamName).IsEqualTo("arg13");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg1 = null;
        arg2 = null;
        var firstFailure = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(firstFailure!.ParamName).IsEqualTo("arg1");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        var tail = "tail";
        var trailingStringResult = C.Args.NotNull(
            arg1,
            arg2,
            arg3,
            arg4,
            arg5,
            arg6,
            arg7,
            arg8,
            arg9,
            arg10,
            arg11,
            arg12,
            tail
        );
        await Assert.That(trailingStringResult.arg13).IsEqualTo(tail);
    }

    [Test]
    public async Task NotNull_14_ReturnsTupleAndReportsEveryNullParameter()
    {
        object? arg1 = new object();
        object? arg2 = new object();
        object? arg3 = new object();
        object? arg4 = new object();
        object? arg5 = new object();
        object? arg6 = new object();
        object? arg7 = new object();
        object? arg8 = new object();
        object? arg9 = new object();
        object? arg10 = new object();
        object? arg11 = new object();
        object? arg12 = new object();
        object? arg13 = new object();
        object? arg14 = new object();

        var result = C.Args.NotNull(
            arg1,
            arg2,
            arg3,
            arg4,
            arg5,
            arg6,
            arg7,
            arg8,
            arg9,
            arg10,
            arg11,
            arg12,
            arg13,
            arg14
        );
        await Assert.That(result.arg14).IsSameReferenceAs(arg14);

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg1 = null;
        var exception1 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception1!.ParamName).IsEqualTo("arg1");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg2 = null;
        var exception2 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception2!.ParamName).IsEqualTo("arg2");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg3 = null;
        var exception3 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception3!.ParamName).IsEqualTo("arg3");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg4 = null;
        var exception4 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception4!.ParamName).IsEqualTo("arg4");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg5 = null;
        var exception5 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception5!.ParamName).IsEqualTo("arg5");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg6 = null;
        var exception6 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception6!.ParamName).IsEqualTo("arg6");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg7 = null;
        var exception7 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception7!.ParamName).IsEqualTo("arg7");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg8 = null;
        var exception8 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception8!.ParamName).IsEqualTo("arg8");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg9 = null;
        var exception9 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception9!.ParamName).IsEqualTo("arg9");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg10 = null;
        var exception10 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception10!.ParamName).IsEqualTo("arg10");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg11 = null;
        var exception11 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception11!.ParamName).IsEqualTo("arg11");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg12 = null;
        var exception12 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception12!.ParamName).IsEqualTo("arg12");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg13 = null;
        var exception13 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception13!.ParamName).IsEqualTo("arg13");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg14 = null;
        var exception14 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception14!.ParamName).IsEqualTo("arg14");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg1 = null;
        arg2 = null;
        var firstFailure = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(firstFailure!.ParamName).IsEqualTo("arg1");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        var tail = "tail";
        var trailingStringResult = C.Args.NotNull(
            arg1,
            arg2,
            arg3,
            arg4,
            arg5,
            arg6,
            arg7,
            arg8,
            arg9,
            arg10,
            arg11,
            arg12,
            arg13,
            tail
        );
        await Assert.That(trailingStringResult.arg14).IsEqualTo(tail);
    }

    [Test]
    public async Task NotNull_15_ReturnsTupleAndReportsEveryNullParameter()
    {
        object? arg1 = new object();
        object? arg2 = new object();
        object? arg3 = new object();
        object? arg4 = new object();
        object? arg5 = new object();
        object? arg6 = new object();
        object? arg7 = new object();
        object? arg8 = new object();
        object? arg9 = new object();
        object? arg10 = new object();
        object? arg11 = new object();
        object? arg12 = new object();
        object? arg13 = new object();
        object? arg14 = new object();
        object? arg15 = new object();

        var result = C.Args.NotNull(
            arg1,
            arg2,
            arg3,
            arg4,
            arg5,
            arg6,
            arg7,
            arg8,
            arg9,
            arg10,
            arg11,
            arg12,
            arg13,
            arg14,
            arg15
        );
        await Assert.That(result.arg15).IsSameReferenceAs(arg15);

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg1 = null;
        var exception1 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception1!.ParamName).IsEqualTo("arg1");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg2 = null;
        var exception2 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception2!.ParamName).IsEqualTo("arg2");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg3 = null;
        var exception3 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception3!.ParamName).IsEqualTo("arg3");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg4 = null;
        var exception4 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception4!.ParamName).IsEqualTo("arg4");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg5 = null;
        var exception5 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception5!.ParamName).IsEqualTo("arg5");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg6 = null;
        var exception6 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception6!.ParamName).IsEqualTo("arg6");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg7 = null;
        var exception7 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception7!.ParamName).IsEqualTo("arg7");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg8 = null;
        var exception8 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception8!.ParamName).IsEqualTo("arg8");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg9 = null;
        var exception9 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception9!.ParamName).IsEqualTo("arg9");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg10 = null;
        var exception10 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception10!.ParamName).IsEqualTo("arg10");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg11 = null;
        var exception11 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception11!.ParamName).IsEqualTo("arg11");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg12 = null;
        var exception12 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception12!.ParamName).IsEqualTo("arg12");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg13 = null;
        var exception13 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception13!.ParamName).IsEqualTo("arg13");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg14 = null;
        var exception14 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception14!.ParamName).IsEqualTo("arg14");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg15 = null;
        var exception15 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception15!.ParamName).IsEqualTo("arg15");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg1 = null;
        arg2 = null;
        var firstFailure = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(firstFailure!.ParamName).IsEqualTo("arg1");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        var tail = "tail";
        var trailingStringResult = C.Args.NotNull(
            arg1,
            arg2,
            arg3,
            arg4,
            arg5,
            arg6,
            arg7,
            arg8,
            arg9,
            arg10,
            arg11,
            arg12,
            arg13,
            arg14,
            tail
        );
        await Assert.That(trailingStringResult.arg15).IsEqualTo(tail);
    }

    [Test]
    public async Task NotNull_16_ReturnsTupleAndReportsEveryNullParameter()
    {
        object? arg1 = new object();
        object? arg2 = new object();
        object? arg3 = new object();
        object? arg4 = new object();
        object? arg5 = new object();
        object? arg6 = new object();
        object? arg7 = new object();
        object? arg8 = new object();
        object? arg9 = new object();
        object? arg10 = new object();
        object? arg11 = new object();
        object? arg12 = new object();
        object? arg13 = new object();
        object? arg14 = new object();
        object? arg15 = new object();
        object? arg16 = new object();

        var result = C.Args.NotNull(
            arg1,
            arg2,
            arg3,
            arg4,
            arg5,
            arg6,
            arg7,
            arg8,
            arg9,
            arg10,
            arg11,
            arg12,
            arg13,
            arg14,
            arg15,
            arg16
        );
        await Assert.That(result.arg16).IsSameReferenceAs(arg16);

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg1 = null;
        var exception1 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception1!.ParamName).IsEqualTo("arg1");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg2 = null;
        var exception2 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception2!.ParamName).IsEqualTo("arg2");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg3 = null;
        var exception3 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception3!.ParamName).IsEqualTo("arg3");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg4 = null;
        var exception4 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception4!.ParamName).IsEqualTo("arg4");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg5 = null;
        var exception5 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception5!.ParamName).IsEqualTo("arg5");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg6 = null;
        var exception6 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception6!.ParamName).IsEqualTo("arg6");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg7 = null;
        var exception7 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception7!.ParamName).IsEqualTo("arg7");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg8 = null;
        var exception8 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception8!.ParamName).IsEqualTo("arg8");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg9 = null;
        var exception9 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception9!.ParamName).IsEqualTo("arg9");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg10 = null;
        var exception10 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception10!.ParamName).IsEqualTo("arg10");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg11 = null;
        var exception11 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception11!.ParamName).IsEqualTo("arg11");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg12 = null;
        var exception12 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception12!.ParamName).IsEqualTo("arg12");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg13 = null;
        var exception13 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception13!.ParamName).IsEqualTo("arg13");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg14 = null;
        var exception14 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception14!.ParamName).IsEqualTo("arg14");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg15 = null;
        var exception15 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception15!.ParamName).IsEqualTo("arg15");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg16 = null;
        var exception16 = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception16!.ParamName).IsEqualTo("arg16");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        arg1 = null;
        arg2 = null;
        var firstFailure = await Assert
            .That(() =>
                C.Args.NotNull(
                    arg1,
                    arg2,
                    arg3,
                    arg4,
                    arg5,
                    arg6,
                    arg7,
                    arg8,
                    arg9,
                    arg10,
                    arg11,
                    arg12,
                    arg13,
                    arg14,
                    arg15,
                    arg16
                )
            )
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(firstFailure!.ParamName).IsEqualTo("arg1");

        arg1 = new object();
        arg2 = new object();
        arg3 = new object();
        arg4 = new object();
        arg5 = new object();
        arg6 = new object();
        arg7 = new object();
        arg8 = new object();
        arg9 = new object();
        arg10 = new object();
        arg11 = new object();
        arg12 = new object();
        arg13 = new object();
        arg14 = new object();
        arg15 = new object();
        arg16 = new object();
        var tail = "tail";
        var trailingStringResult = C.Args.NotNull(
            arg1,
            arg2,
            arg3,
            arg4,
            arg5,
            arg6,
            arg7,
            arg8,
            arg9,
            arg10,
            arg11,
            arg12,
            arg13,
            arg14,
            arg15,
            tail
        );
        await Assert.That(trailingStringResult.arg16).IsEqualTo(tail);
    }
}
