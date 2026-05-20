namespace ReadMockJsonUsingDapperApp.Classes;

/// <summary>
/// Represents a data transfer object (DTO) for a skill, containing details
/// such as the skill's index and name.
/// </summary>
/// <remarks>
/// This class is primarily used to facilitate the transfer of skill-related data
/// and is often associated with a person, as seen in <see cref="PersonDto"/>.
/// </remarks>
public sealed class SkillDto
{
    public int SkillIndex { get; set; }
    public string SkillName { get; set; } = "";
}