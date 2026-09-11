using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check.Arg;

public class StateTests
{
    [Test]
    public async Task HasValidState()
    {
        var value = "ab";
        await Assert.That(C.Arg.HasValidState(value, x => x.Length == 2)).IsSameReferenceAs(value);
        var exception = await Assert
            .That(() => C.Arg.HasValidState(value, x => false, "input", "predicate"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).Contains("Failed predicate: \"predicate\"");
    }

    [Test]
    public async Task HasValidState_WithArgument()
    {
        var value = "ab";
        await Assert
            .That(C.Arg.HasValidState(value, 2, static (x, n) => x.Length == n))
            .IsSameReferenceAs(value);
        var exception = await Assert
            .That(() =>
                C.Arg.HasValidState(value, 3, static (x, n) => x.Length == n, "input", "predicate")
            )
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).Contains("Failed predicate: \"predicate\"");
    }
}
