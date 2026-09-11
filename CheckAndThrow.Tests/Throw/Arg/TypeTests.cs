using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg;

public class TypeTests
{
    [Test]
    public async Task IsNotAssignableTo_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotAssignableTo("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument is not assignable to the required type.");
    }

    [Test]
    public async Task IsNotAssignableTo_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotAssignableTo<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument is not assignable to the required type.");
    }

    [Test]
    public async Task IsNotAssignableTo_string_Type_Type()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotAssignableTo("input", typeof(string), typeof(int)))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument is not assignable to the required type. Expected type: \"System.String\". Argument type: \"System.Int32\"."
            );
    }

    [Test]
    public async Task IsNotAssignableTo_Generic_string_Type_Type()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotAssignableTo<object>("input", typeof(string), typeof(int)))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument is not assignable to the required type. Expected type: \"System.String\". Argument type: \"System.Int32\"."
            );
    }

    [Test]
    public async Task IsNotAssignableFrom_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotAssignableFrom("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument type is not assignable from the required type.");
    }

    [Test]
    public async Task IsNotAssignableFrom_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotAssignableFrom<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument type is not assignable from the required type.");
    }

    [Test]
    public async Task IsNotAssignableFrom_string_Type_Type()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotAssignableFrom("input", typeof(string), typeof(int)))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument type is not assignable from the required type. Expected type: \"System.String\". Argument type: \"System.Int32\"."
            );
    }

    [Test]
    public async Task IsNotAssignableFrom_Generic_string_Type_Type()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNotAssignableFrom<object>("input", typeof(string), typeof(int)))
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
