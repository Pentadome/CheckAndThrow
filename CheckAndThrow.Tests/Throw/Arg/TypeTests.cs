using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg;

public class TypeTests
{
    [Test]
    public async Task NotAssignableTo_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotAssignableTo("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument is not assignable to the required type.");
    }

    [Test]
    public async Task NotAssignableTo_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotAssignableTo<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument is not assignable to the required type.");
    }

    [Test]
    public async Task NotAssignableTo_string_Type_Type()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotAssignableTo("input", typeof(string), typeof(int)))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument is not assignable to the required type. Expected type: \"System.String\". Argument type: \"System.Int32\"."
            );
    }

    [Test]
    public async Task NotAssignableTo_Generic_string_Type_Type()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotAssignableTo<object>("input", typeof(string), typeof(int)))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument is not assignable to the required type. Expected type: \"System.String\". Argument type: \"System.Int32\"."
            );
    }

    [Test]
    public async Task NotAssignableFrom_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotAssignableFrom("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument type is not assignable from the required type.");
    }

    [Test]
    public async Task NotAssignableFrom_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotAssignableFrom<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument type is not assignable from the required type.");
    }

    [Test]
    public async Task NotAssignableFrom_string_Type_Type()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotAssignableFrom("input", typeof(string), typeof(int)))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument type is not assignable from the required type. Expected type: \"System.String\". Argument type: \"System.Int32\"."
            );
    }

    [Test]
    public async Task NotAssignableFrom_Generic_string_Type_Type()
    {
        var exception = await Assert
            .That(() => Th.Arg.NotAssignableFrom<object>("input", typeof(string), typeof(int)))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument type is not assignable from the required type. Expected type: \"System.String\". Argument type: \"System.Int32\"."
            );
    }

    [Test]
    public async Task DoesNotHaveAttribute_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.DoesNotHaveAttribute("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument type misses the required attribute");
    }

    [Test]
    public async Task DoesNotHaveAttribute_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.DoesNotHaveAttribute<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument type misses the required attribute");
    }

    [Test]
    public async Task DoesNotHaveAttribute_string_Type_Type()
    {
        var exception = await Assert
            .That(() =>
                Th.Arg.DoesNotHaveAttribute("input", typeof(ObsoleteAttribute), typeof(int))
            )
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument type misses the required attribute Expected attribute: \"System.ObsoleteAttribute\". Missing on argument type: \"System.Int32\"."
            );
    }

    [Test]
    public async Task DoesNotHaveAttribute_Generic_string_Type_Type()
    {
        var exception = await Assert
            .That(() =>
                Th.Arg.DoesNotHaveAttribute<object>("input", typeof(ObsoleteAttribute), typeof(int))
            )
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument type misses the required attribute Expected attribute: \"System.ObsoleteAttribute\". Missing on argument type: \"System.Int32\"."
            );
    }
}
