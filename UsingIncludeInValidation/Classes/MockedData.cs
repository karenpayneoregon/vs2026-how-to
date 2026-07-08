using UsingIncludeInValidation.Interfaces;
using UsingIncludeInValidation.Models;

namespace UsingIncludeInValidation.Classes;

internal class MockedData
{
    /// <summary>
    /// Retrieves a list of mocked people data.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="IPerson"/> objects representing mocked people data.
    /// </returns>
    /// <remarks>
    /// The returned list includes instances of <see cref="Person"/> and <see cref="Citizen"/> 
    /// with predefined properties such as <c>Id</c>, <c>FirstName</c>, <c>LastName</c>, 
    /// <c>BirthDate</c>, and <c>Address</c>.
    /// </remarks>
    public static List<IPerson> People()
    {
        List<IPerson> people =
        [
            new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateOnly(1790, 12, 1),
                Address = new Address
                {
                    Line1 = "123 Main St",
                    Line2 = "Apt 101",
                    Town = "Any town",
                    Country = "USA",
                    Postcode = "12345"
                }
            },

            new Citizen
            {
                Id = 1,
                FirstName = "Anne",
                LastName = "Doe",
                BirthDate = new DateOnly(1969, 1, 11),
                Since = new DateOnly(2020, 1, 1),
                Address = new Address
                {
                    Line2 = "Apt 101",
                    Town = "Any town",
                    Country = "USA"
                }
            },

            new Person
            {
                Id = 2,
                FirstName = "Mary",
                LastName = "Clime",
                BirthDate = new DateOnly(2000, 12, 1),
                Address = new Address
                {
                    Line1 = "555 Orange St",
                    Town = "Any place",
                    Country = "USA",
                    Postcode = "12343"
                }
            }

        ];
        
        return people;
    }
}
