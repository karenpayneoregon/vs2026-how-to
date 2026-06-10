using ExperimentsApp.Interfaces;

namespace ExperimentsApp.Models;

public class Employee : IPerson
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Badge { get; set; }
}