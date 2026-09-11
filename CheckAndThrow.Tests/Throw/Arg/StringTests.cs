using Th = CheckAndThrow.Throw;

namespace CheckAndThrow.Tests.Throw.Arg;

public class StringTests
{
    [Test]
    public async Task IsNullOrEmpty_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNullOrEmpty("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument cannot be null or empty.");
    }

    [Test]
    public async Task IsNullOrEmpty_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNullOrEmpty<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument cannot be null or empty.");
    }

    [Test]
    public async Task IsNullOrWhiteSpace_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNullOrWhiteSpace("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument cannot be null or empty and can not only contain white space characters."
            );
    }

    [Test]
    public async Task IsNullOrWhiteSpace_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.IsNullOrWhiteSpace<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument cannot be null or empty and can not only contain white space characters."
            );
    }

    [Test]
    public async Task DoesNotMatch_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.DoesNotMatch("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument does not match the required pattern.");
    }

    [Test]
    public async Task DoesNotMatch_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.DoesNotMatch<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument does not match the required pattern.");
    }

    [Test]
    public async Task DoesNotMatch_string_string_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.DoesNotMatch("input", "pattern", "value"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument does not match the required pattern. Pattern: \"pattern\". Value: \"value\"."
            );
    }

    [Test]
    public async Task DoesNotMatch_Generic_string_string_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.DoesNotMatch<object>("input", "pattern", "value"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith(
                "Argument does not match the required pattern. Pattern: \"pattern\". Value: \"value\"."
            );
    }

    [Test]
    public async Task InvalidLength_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidLength("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument has an invalid length.");
    }

    [Test]
    public async Task InvalidLength_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidLength<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument has an invalid length.");
    }

    [Test]
    public async Task InvalidLength_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidLength("input", 3, 7))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument has an invalid length. Expected length: 3. Actual length: 7");
    }

    [Test]
    public async Task InvalidLength_Generic_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidLength<object>("input", 3, 7))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument has an invalid length. Expected length: 3. Actual length: 7");
    }

    [Test]
    public async Task TooShort_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.TooShort("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument is too short.");
    }

    [Test]
    public async Task TooShort_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.TooShort<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument is too short.");
    }

    [Test]
    public async Task TooShort_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.TooShort("input", 3, 7))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument is too short. Minimum length: 3. Actual length: 7");
    }

    [Test]
    public async Task TooShort_Generic_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.TooShort<object>("input", 3, 7))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument is too short. Minimum length: 3. Actual length: 7");
    }

    [Test]
    public async Task TooLong_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.TooLong("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument is too long.");
    }

    [Test]
    public async Task TooLong_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.TooLong<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument is too long.");
    }

    [Test]
    public async Task TooLong_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.TooLong("input", 3, 7))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument is too long. Maximum length: 3. Actual length: 7");
    }

    [Test]
    public async Task TooLong_Generic_string_object_object()
    {
        var exception = await Assert
            .That(() => Th.Arg.TooLong<object>("input", 3, 7))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument is too long. Maximum length: 3. Actual length: 7");
    }

    [Test]
    public async Task InvalidEmail_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidEmail("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument is not a valid email address.");
    }

    [Test]
    public async Task InvalidEmail_Generic_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidEmail<object>("input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert.That(exception!.Message).StartsWith("Argument is not a valid email address.");
    }

    [Test]
    public async Task InvalidEmail_string_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidEmail("input", "value"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument is not a valid email address. Value: \"value\".");
    }

    [Test]
    public async Task InvalidEmail_Generic_string_string()
    {
        var exception = await Assert
            .That(() => Th.Arg.InvalidEmail<object>("input", "value"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
        await Assert
            .That(exception!.Message)
            .StartsWith("Argument is not a valid email address. Value: \"value\".");
    }
}
