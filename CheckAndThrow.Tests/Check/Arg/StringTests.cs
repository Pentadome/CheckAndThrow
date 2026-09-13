using C = CheckAndThrow.Check;

namespace CheckAndThrow.Tests.Check.Arg;

public class StringTests
{
    [Test]
    public async Task NotNullOrEmpty_Accepts()
    {
        var value = "aa";
        await Assert.That(C.Arg.NotNullOrEmpty(value)).IsEqualTo(value);
    }

    [Test]
    public async Task NotNullOrEmpty_Rejects()
    {
        var exception = await Assert
            .That(() => C.Arg.NotNullOrEmpty("", paramName: "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task NotNullOrEmpty_RejectsNull()
    {
        var exception = await Assert
            .That(() => C.Arg.NotNullOrEmpty(null!, paramName: "input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task NotNullOrWhiteSpace_Accepts()
    {
        var value = "aa";
        await Assert.That(C.Arg.NotNullOrWhiteSpace(value)).IsEqualTo(value);
    }

    [Test]
    public async Task NotNullOrWhiteSpace_Rejects()
    {
        var exception = await Assert
            .That(() => C.Arg.NotNullOrWhiteSpace("", paramName: "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task NotNullOrWhiteSpace_RejectsNull()
    {
        var exception = await Assert
            .That(() => C.Arg.NotNullOrWhiteSpace(null!, paramName: "input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task Matches_Accepts()
    {
        var value = "aa";
        await Assert.That(C.Arg.Matches(value, "^a+$")).IsEqualTo(value);
    }

    [Test]
    public async Task Matches_Rejects()
    {
        var exception = await Assert
            .That(() => C.Arg.Matches("b", "^a+$", paramName: "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task Matches_RejectsNull()
    {
        var exception = await Assert
            .That(() => C.Arg.Matches(null!, "^a+$", paramName: "input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task HasLength_Accepts()
    {
        var value = "aa";
        await Assert.That(C.Arg.HasLength(value, 2)).IsEqualTo(value);
    }

    [Test]
    public async Task HasLength_Rejects()
    {
        var exception = await Assert
            .That(() => C.Arg.HasLength("", 2, paramName: "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task HasLength_RejectsNull()
    {
        var exception = await Assert
            .That(() => C.Arg.HasLength(null!, 2, paramName: "input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task HasMinLength_Accepts()
    {
        var value = "aa";
        await Assert.That(C.Arg.HasMinLength(value, 2)).IsEqualTo(value);
    }

    [Test]
    public async Task HasMinLength_Rejects()
    {
        var exception = await Assert
            .That(() => C.Arg.HasMinLength("", 2, paramName: "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task HasMinLength_RejectsNull()
    {
        var exception = await Assert
            .That(() => C.Arg.HasMinLength(null!, 2, paramName: "input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task HasMaxLength_Accepts()
    {
        var value = "aa";
        await Assert.That(C.Arg.HasMaxLength(value, 2)).IsEqualTo(value);
    }

    [Test]
    public async Task HasMaxLength_Rejects()
    {
        var exception = await Assert
            .That(() => C.Arg.HasMaxLength("aaa", 2, paramName: "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task HasMaxLength_RejectsNull()
    {
        var exception = await Assert
            .That(() => C.Arg.HasMaxLength(null!, 2, paramName: "input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task Email_Accepts()
    {
        var value = "a@b.co";
        await Assert.That(C.Arg.Email(value)).IsEqualTo(value);
    }

    [Test]
    public async Task Email_Rejects()
    {
        var exception = await Assert
            .That(() => C.Arg.Email("a@", paramName: "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task Email_RejectsNull()
    {
        var exception = await Assert
            .That(() => C.Arg.Email(null!, paramName: "input"))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task Whitespace_IsEmptyOnlyWhenRequired()
    {
        await Assert.That(C.Arg.NotNullOrEmpty(" ")).IsEqualTo(" ");
        var exception = await Assert
            .That(() => C.Arg.NotNullOrWhiteSpace(" ", "input"))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(exception!.ParamName).IsEqualTo("input");
    }

    [Test]
    public async Task Matches_RejectsNullPattern()
    {
        var exception = await Assert
            .That(() => C.Arg.Matches("a", null!))
            .ThrowsExactly<ArgumentNullException>();
        await Assert.That(exception!.ParamName).IsEqualTo("pattern");
    }

    [Test]
    public async Task Length_Boundaries()
    {
        await Assert.That(C.Arg.HasLength("", 0)).IsEqualTo("");
        await Assert.That(C.Arg.HasMinLength("abc", 2)).IsEqualTo("abc");
        await Assert.That(C.Arg.HasMaxLength("a", 2)).IsEqualTo("a");
    }
}
