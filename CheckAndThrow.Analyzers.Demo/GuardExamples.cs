namespace CheckAndThrow.Analyzers.Demo;

// Leave guards out: place the caret on a parameter or property and invoke Quick Actions.
public class GuardExamples
{
    // CAT0003: choose a range guard for the setter's implicit value.
    public int Count { get; set; }

    // CAT0001: add Check.Arg.NotNull.
    public void RequireObject(object value)
    {
        _ = value.ToString();
    }

    // CAT0002: add Check.Args.NotNull for both parameters (CAT0001 also appears on each).
    public void RequireBoth(object first, object second)
    {
        _ = first.ToString();
        _ = second.ToString();
    }

    // CAT0004: choose NotNullOrEmpty or NotNullOrWhiteSpace (CAT0001 also appears).
    public void RequireText(string text)
    {
        _ = text.Length;
    }

    // CAT0003: choose a numeric range guard.
    public void RequirePositive(int count)
    {
        _ = count.ToString();
    }
}
