using System.Xml.Serialization;

namespace ConversionsApp.Models;

[XmlRoot("People")]
public class People
{
    [XmlElement("Person")]
    public List<Person> Persons { get; set; } = [];
}