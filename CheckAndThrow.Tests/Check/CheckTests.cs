using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check;

public class CheckTests
{
    sealed class FixedTimeProvider : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => new(2024, 1, 2, 12, 0, 0, TimeSpan.Zero);

        public override TimeZoneInfo LocalTimeZone => TimeZoneInfo.Utc;
    }

    [Test]
    [NotInParallel]
    public async Task DefaultTimeProvider_GetSetNullAndFallback()
    {
        var original = C.DefaultTimeProvider;
        var provider = new FixedTimeProvider();
        try
        {
            C.DefaultTimeProvider = provider;
            await Assert.That(C.DefaultTimeProvider).IsSameReferenceAs(provider);
            var now = provider.GetUtcNow();
            await Assert
                .That(C.Arg.InPast(now.AddDays(-1).UtcDateTime))
                .IsEqualTo(now.AddDays(-1).UtcDateTime);
            await Assert.That(C.Arg.InPast(now.AddDays(-1))).IsEqualTo(now.AddDays(-1));
            await Assert
                .That(C.Arg.InPastUtc(now.AddDays(-1).UtcDateTime))
                .IsEqualTo(now.AddDays(-1).UtcDateTime);
            await Assert
                .That(C.Arg.InFuture(now.AddDays(1).UtcDateTime))
                .IsEqualTo(now.AddDays(1).UtcDateTime);
            await Assert.That(C.Arg.InFuture(now.AddDays(1))).IsEqualTo(now.AddDays(1));
            await Assert
                .That(C.Arg.InFutureUtc(now.AddDays(1).UtcDateTime))
                .IsEqualTo(now.AddDays(1).UtcDateTime);
            await Assert
                .That(() => C.DefaultTimeProvider = null!)
                .ThrowsExactly<ArgumentNullException>();
            await Assert.That(C.DefaultTimeProvider).IsSameReferenceAs(provider);
        }
        finally
        {
            C.DefaultTimeProvider = original;
        }
    }
}
