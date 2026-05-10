namespace ExtensionsLibrary;

public static class BoolExtensions
{
    extension(bool value)
    {
        /// <summary>
        /// Converts the current boolean value to its equivalent "Yes" or "No" string representation.
        /// </summary>
        /// <returns>A string that is "Yes" if the value is <see langword="true"/>; otherwise, "No".</returns>
        public string ToYesNo() => value ? "Yes" : "No";
    }
}