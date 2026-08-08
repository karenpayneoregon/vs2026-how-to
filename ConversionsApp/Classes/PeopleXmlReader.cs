using ConversionsApp.Models;
using System.Xml.Serialization;

namespace ConversionsApp.Classes;

public static class PeopleXmlReader
{
    public static People Read(string filePath)
    {
        XmlSerializer serializer = new(typeof(People));

        using FileStream stream = File.OpenRead(filePath);

        return (People)serializer.Deserialize(stream)!;
    }
}