using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check;

public class ExpressionTests
{
    [Test]
    public async Task NotNull_ReturnsReference()
    {
        var value = new object();
        await Assert.That(C.Expression.NotNull(value)).IsSameReferenceAs(value);
    }

    [Test]
    public async Task NotNull_RejectsNull()
    {
        var exception = await Assert
            .That(() => C.Expression.NotNull((object)null!, "source"))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("source");
    }

    [Test]
    public async Task EqualTo_DefaultComparer()
    {
        await Assert.That(C.Expression.EqualTo("a", "a")).IsEqualTo("a");
        var exception = await Assert
            .That(() =>
                C.Expression.EqualTo(
                    "a",
                    "b",
                    expressionString: "source",
                    equalToValueString: "comparison"
                )
            )
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("source");
        await Assert.That(exception!.Message).Contains("comparison");
    }

    [Test]
    public async Task NotEqualTo_DefaultComparer()
    {
        await Assert.That(C.Expression.NotEqualTo("a", "b")).IsEqualTo("a");
        var exception = await Assert
            .That(() =>
                C.Expression.NotEqualTo(
                    "a",
                    "a",
                    expressionString: "source",
                    notEqualToValueString: "comparison"
                )
            )
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("source");
        await Assert.That(exception!.Message).Contains("comparison");
    }

    [Test]
    public async Task CustomComparers()
    {
        await Assert
            .That(C.Expression.EqualTo("a", "A", StringComparer.OrdinalIgnoreCase))
            .IsEqualTo("a");
        await Assert.That(C.Expression.NotEqualTo("a", "A", StringComparer.Ordinal)).IsEqualTo("a");
        var exception = await Assert
            .That(() => C.Expression.NotEqualTo("a", "A", StringComparer.OrdinalIgnoreCase))
            .ThrowsExactly<InvalidOperationException>();
    }

    [Test]
    public async Task IsTrue_BothBranches()
    {
        C.Expression.IsTrue(true);
        var exception = await Assert
            .That(() => C.Expression.IsTrue(false, "condition"))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("condition");
    }

    [Test]
    public async Task IsFalse_BothBranches()
    {
        C.Expression.IsFalse(false);
        var exception = await Assert
            .That(() => C.Expression.IsFalse(true, "condition"))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(exception!.Message).Contains("condition");
    }
}
