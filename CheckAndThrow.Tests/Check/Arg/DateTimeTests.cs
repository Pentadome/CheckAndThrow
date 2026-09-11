using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check.Arg;

public class DateTimeTests
{
    private sealed class FixedTimeProvider : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => new(2024, 1, 2, 12, 0, 0, TimeSpan.Zero);

        public override TimeZoneInfo LocalTimeZone => TimeZoneInfo.Utc;
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task IsInPast_DateTime(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow().UtcDateTime;
        var value = now.AddTicks(offset);
        if (offset < 0)
        {
            await Assert.That(C.Arg.IsInPast(value, provider)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.IsInPast(value, provider))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task IsInPast_DateTimeOffset(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow();
        var value = now.AddTicks(offset);
        if (offset < 0)
        {
            await Assert.That(C.Arg.IsInPast(value, provider)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.IsInPast(value, provider))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task IsInPastUtc_DateTime(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow().UtcDateTime;
        var value = now.AddTicks(offset);
        if (offset < 0)
        {
            await Assert.That(C.Arg.IsInPastUtc(value, provider)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.IsInPastUtc(value, provider))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task IsInFuture_DateTime(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow().UtcDateTime;
        var value = now.AddTicks(offset);
        if (offset > 0)
        {
            await Assert.That(C.Arg.IsInFuture(value, provider)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.IsInFuture(value, provider))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task IsInFuture_DateTimeOffset(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow();
        var value = now.AddTicks(offset);
        if (offset > 0)
        {
            await Assert.That(C.Arg.IsInFuture(value, provider)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.IsInFuture(value, provider))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task IsInFutureUtc_DateTime(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow().UtcDateTime;
        var value = now.AddTicks(offset);
        if (offset > 0)
        {
            await Assert.That(C.Arg.IsInFutureUtc(value, provider)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.IsInFutureUtc(value, provider))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task IsLaterThan_DateTime(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow().UtcDateTime;
        var value = now.AddTicks(offset);
        if (offset > 0)
        {
            await Assert.That(C.Arg.IsLaterThan(value, now)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.IsLaterThan(value, now))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task IsLaterThan_DateTimeOffset(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow();
        var value = now.AddTicks(offset);
        if (offset > 0)
        {
            await Assert.That(C.Arg.IsLaterThan(value, now)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.IsLaterThan(value, now))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task IsEarlierThan_DateTime(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow().UtcDateTime;
        var value = now.AddTicks(offset);
        if (offset < 0)
        {
            await Assert.That(C.Arg.IsEarlierThan(value, now)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.IsEarlierThan(value, now))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task IsEarlierThan_DateTimeOffset(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow();
        var value = now.AddTicks(offset);
        if (offset < 0)
        {
            await Assert.That(C.Arg.IsEarlierThan(value, now)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.IsEarlierThan(value, now))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    public async Task DateTimeOffset_ComparisonUsesInstant()
    {
        var provider = new FixedTimeProvider();
        var value = provider.GetUtcNow().AddMinutes(-1).ToOffset(TimeSpan.FromHours(5));
        await Assert.That(C.Arg.IsInPast(value, provider)).IsEqualTo(value);
        await Assert.That(C.Arg.IsEarlierThan(value, provider.GetUtcNow())).IsEqualTo(value);
    }
}
