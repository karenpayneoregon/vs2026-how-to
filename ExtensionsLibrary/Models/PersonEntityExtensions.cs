namespace ExtensionsLibrary.Models;

public static class PersonEntityExtensions
{
    extension(Person value)
    {
       public string FullName => $"{value.Firstname} {value.Lastname}";
    }
}