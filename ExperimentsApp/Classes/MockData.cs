using ExperimentsApp.Interfaces;
using ExperimentsApp.Models;

namespace ExperimentsApp.Classes;

internal class MockData
{
    /// <summary>
    /// Retrieves a list of mock people data.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="IPerson"/> objects, representing mock data for people.
    /// </returns>
    /// <remarks>
    /// The returned list includes instances of <see cref="Person"/> and <see cref="Employee"/> 
    /// with predefined properties such as ID, name, birthdate, and address.
    /// </remarks>
    public static List<IPerson> PeopleList() =>
    [
        new Person { Id = 1, FirstName = "Alice", LastName = "Smith", BirthDate = new DateOnly(1990, 5, 15) ,
            Address = new Address { State = "OR", City = "Salem" }},
        
        new Person { Id = 2, FirstName = "Bob", LastName = "Johnson", BirthDate = new DateOnly(1985, 11, 23) },
        new Employee() { Id = 3, FirstName = "Bob", LastName = "Johnson", BirthDate = new DateOnly(1985, 11, 23), Badge = "AA1234"},
        new Person { Id = 4, FirstName = "Charlie", LastName = "Williams", BirthDate = new DateOnly(2001, 1, 1)
        }
    ];
    
    /// <summary>
    /// Retrieves a list of mock monetary values.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="Money"/> objects, representing mock data for monetary values.
    /// </returns>
    /// <remarks>
    /// The returned list includes instances of <see cref="Money"/> with predefined amounts and currency codes.
    /// It can be used for testing or demonstration purposes, such as pattern matching scenarios.
    /// </remarks>
    public static List<Money> MoneyList() =>
    [
        new(0m, "USD"),
        new(25m, "USD"),
        new(125m, "USD"),
        new(50m, "EUR"),
        new(-10m, "USD")
    ];
}

