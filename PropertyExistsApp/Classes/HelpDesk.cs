#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace PropertyExistsApp.Classes;

public class HelpDesk
{
    public string Phone { get; set; }
    public string Email { get; set; }
}