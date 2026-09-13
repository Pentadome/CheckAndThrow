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
    public async Task InPast_DateTime(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow().UtcDateTime;
        var value = now.AddTicks(offset);
        if (offset < 0)
        {
            await Assert.That(C.Arg.InPast(value, provider)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InPast(value, provider))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task InPast_DateTimeOffset(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow();
        var value = now.AddTicks(offset);
        if (offset < 0)
        {
            await Assert.That(C.Arg.InPast(value, provider)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InPast(value, provider))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task InPastUtc_DateTime(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow().UtcDateTime;
        var value = now.AddTicks(offset);
        if (offset < 0)
        {
            await Assert.That(C.Arg.InPastUtc(value, provider)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InPastUtc(value, provider))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task InFuture_DateTime(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow().UtcDateTime;
        var value = now.AddTicks(offset);
        if (offset > 0)
        {
            await Assert.That(C.Arg.InFuture(value, provider)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InFuture(value, provider))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task InFuture_DateTimeOffset(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow();
        var value = now.AddTicks(offset);
        if (offset > 0)
        {
            await Assert.That(C.Arg.InFuture(value, provider)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InFuture(value, provider))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task InFutureUtc_DateTime(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow().UtcDateTime;
        var value = now.AddTicks(offset);
        if (offset > 0)
        {
            await Assert.That(C.Arg.InFutureUtc(value, provider)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.InFutureUtc(value, provider))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task LaterThan_DateTime(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow().UtcDateTime;
        var value = now.AddTicks(offset);
        if (offset > 0)
        {
            await Assert.That(C.Arg.LaterThan(value, now)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.LaterThan(value, now))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task LaterThan_DateTimeOffset(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow();
        var value = now.AddTicks(offset);
        if (offset > 0)
        {
            await Assert.That(C.Arg.LaterThan(value, now)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.LaterThan(value, now))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task EarlierThan_DateTime(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow().UtcDateTime;
        var value = now.AddTicks(offset);
        if (offset < 0)
        {
            await Assert.That(C.Arg.EarlierThan(value, now)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.EarlierThan(value, now))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    [Arguments(-1)]
    [Arguments(0)]
    [Arguments(1)]
    public async Task EarlierThan_DateTimeOffset(int offset)
    {
        var provider = new FixedTimeProvider();
        var now = provider.GetUtcNow();
        var value = now.AddTicks(offset);
        if (offset < 0)
        {
            await Assert.That(C.Arg.EarlierThan(value, now)).IsEqualTo(value);
        }
        else
        {
            var exception = await Assert
                .That(() => C.Arg.EarlierThan(value, now))
                .ThrowsExactly<ArgumentException>();
            await Assert.That(exception!.ParamName).IsEqualTo("value");
        }
    }

    [Test]
    public async Task DateTimeOffset_ComparisonUsesInstant()
    {
        var provider = new FixedTimeProvider();
        var value = provider.GetUtcNow().AddMinutes(-1).ToOffset(TimeSpan.FromHours(5));
        await Assert.That(C.Arg.InPast(value, provider)).IsEqualTo(value);
        await Assert.That(C.Arg.EarlierThan(value, provider.GetUtcNow())).IsEqualTo(value);
    }
}
