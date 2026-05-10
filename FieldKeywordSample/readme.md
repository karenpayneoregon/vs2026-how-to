# About

The token `field` enables you to write a property accessor body without declaring an explicit backing field. The token field is replaced with a compiler synthesized backing field.

- See Micorosoft's [documentation](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/field) for more information.
- Blog post: [The C# "field" Keyword and Visual Studio Tooling](https://jeremybytes.blogspot.com/2024/11/the-c-field-keyword-and-visual-studio.html)




## Example 1

Uppercase first letter

```csharp
public required string FirstName
{
    get;
    set => field = value.CapitalizeFirstLetter();
}
```

## Example 2

Uppercase property value followed by a validation.

```csharp
public required string State
{
    get;
    set
    {
        field = value.ToUpper();
        if (!GetStateAbbreviations().Contains(field))
        {
            throw new ArgumentException("Invalid state abbreviation.");
        }
    }
}
```

