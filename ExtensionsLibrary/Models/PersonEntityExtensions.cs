namespace ExtensionsLibrary.Models;

public static class PersonEntityExtensions
{
    extension(Person value)
    {
        /// <summary>
        /// Gets the full name, consisting of the first name followed by the last name.
        /// </summary>
       public string FullName => $"{value.Firstname} {value.Lastname}";
    }
}