using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check.Arg;

public class TypeTests
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class MarkerAttribute : Attribute;

    [Marker]
    public class Marked;

    public class Derived : Marked;

    [Test]
    public async Task AssignableTo_ReturnsReference()
    {
        var value = new Derived();
        await Assert.That(C.Arg.AssignableTo<Marked>(value)).IsSameReferenceAs(value);
        var exception = await Assert
            .That(() => C.Arg.AssignableTo<string>(value))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("value");
        await Assert
            .That(() => C.Arg.AssignableTo<string>(null!))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task AssignableFrom_UsesCorrectDirection()
    {
        await Assert.That(C.Arg.AssignableFrom<Derived>(typeof(Marked))).IsEqualTo(typeof(Marked));
        var exception = await Assert
            .That(() => C.Arg.AssignableFrom<Marked>(typeof(Derived), "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(() => C.Arg.AssignableFrom<Marked>(null!))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task HasTheAttribute_Generic(bool inherit)
    {
        await Assert
            .That(C.Arg.HasTheAttribute<MarkerAttribute>(typeof(Marked), inherit))
            .IsTypeOf<MarkerAttribute>();
        if (inherit)
            await Assert
                .That(C.Arg.HasTheAttribute<MarkerAttribute>(typeof(Derived), true))
                .IsTypeOf<MarkerAttribute>();
        else
            await Assert
                .That(() => C.Arg.HasTheAttribute<MarkerAttribute>(typeof(Derived)))
                .ThrowsExactly<ArgumentException>();
        var exception = await Assert
            .That(() => C.Arg.HasTheAttribute<MarkerAttribute>(typeof(object), inherit, "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(() => C.Arg.HasTheAttribute<MarkerAttribute>(null!))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task HasTheAttribute_RuntimeType(bool inherit)
    {
        await Assert
            .That(C.Arg.HasTheAttribute(typeof(Marked), typeof(MarkerAttribute), inherit))
            .IsTypeOf<MarkerAttribute>();
        if (inherit)
            await Assert
                .That(C.Arg.HasTheAttribute(typeof(Derived), typeof(MarkerAttribute), true))
                .IsTypeOf<MarkerAttribute>();
        else
            await Assert
                .That(() => C.Arg.HasTheAttribute(typeof(Derived), typeof(MarkerAttribute)))
                .ThrowsExactly<ArgumentException>();
        var exception = await Assert
            .That(() =>
                C.Arg.HasTheAttribute(typeof(object), typeof(MarkerAttribute), inherit, "input")
            )
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(() => C.Arg.HasTheAttribute(null!, typeof(MarkerAttribute)))
            .ThrowsExactly<ArgumentNullException>();
        await Assert
            .That(() => C.Arg.HasTheAttribute(typeof(Marked), null!))
            .ThrowsExactly<ArgumentNullException>();
    }
}
