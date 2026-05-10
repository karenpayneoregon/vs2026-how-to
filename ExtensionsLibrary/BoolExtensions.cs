namespace ExtensionsLibrary;

public static class BoolExtensions
{
    extension(bool value)
    {
        public string ToYesNo() => value ? "Yes" : "No";
    }
}