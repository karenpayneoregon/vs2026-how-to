using System.Xml.Serialization;

namespace ConversionsApp.Models;

public class Person
{
    public int? Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    [XmlIgnore]
    public DateOnly? BirthDate { get; set; }

    [XmlElement("BirthDate")]
    public string? BirthDate1
    {
        get => BirthDate?.ToString("yyyy-MM-dd");

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                BirthDate = null;
                return;
            }

            BirthDate = DateOnly.Parse(value);
        }
    }

    public required Address Address { get; set; }
}