namespace ReadMockJsonUsingDapperApp.Classes;

/// <summary>
/// Represents a data transfer object (DTO) for a person, containing personal details
/// and a collection of associated skills.
/// </summary>
/// <remarks>
/// This class is designed to facilitate the transfer of person-related data, including
/// their first name, last name, age, and a list of skills represented by <see cref="SkillDto"/>.
/// </remarks>
public sealed class PersonDto
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public int Age { get; set; }
    public List<SkillDto> Skills { get; set; } = [];
}