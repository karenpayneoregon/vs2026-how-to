using ExperimentsApp.Interfaces;
using ExtensionsLibrary;

namespace ExperimentsApp.Models;

public class Employee : IPerson
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public required string Badge { get; set; }
    public int Age => BirthDate.GetAge();
}