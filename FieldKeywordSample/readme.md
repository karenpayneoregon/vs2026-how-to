# About

The token `field` enables you to write a property accessor body without declaring an explicit backing field. The token field is replaced with a compiler synthesized backing field.

Use the contextual keyword `field`, introduced in C# 14, in a property accessor to access the compiler-synthesized backing field of a property. By using this syntax, you can define the body of a `get` or `set` accessor and let the compiler generate the other accessor as it would in an automatically implemented property.

- See Micorosoft's [documentation](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/field) for more information.
- Blog post: [The C# "field" Keyword and Visual Studio Tooling](https://jeremybytes.blogspot.com/2024/11/the-c-field-keyword-and-visual-studio.html)
- The [value](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/value) implicit parameter. The `set` accessor in [property](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties) and [indexer](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/) declarations uses the implicit parameter `value`. This parameter acts as an input for the method. The word `value` refers to the value that client code tries to assign to the property or indexer.




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

