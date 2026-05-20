namespace ReadMockJsonUsingDapperApp.Classes;

/// <summary>
/// Represents a row of data containing information about a person's skill.
/// </summary>
/// <remarks>
/// This class is used to map the result of a query that extracts data from a JSON structure.
/// It includes details such as the person's first and last name, age, and skill information.
/// </remarks>
public sealed class PersonSkillRow
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public int Age { get; set; }
    public int SkillIndex { get; set; }
    public string SkillName { get; set; } = "";
}