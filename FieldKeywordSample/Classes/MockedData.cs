using FieldKeywordSample.Interfaces;
using FieldKeywordSample.Models;

namespace FieldKeywordSample.Classes;

internal class MockedData
{
    public static List<IPerson> People()
     =>
    [
        new Customer
            {
                Id = 1,
                CustomerId = 100,
                FirstName = "mary",
                LastName = "SMITH",
                BirthDate = new DateOnly(1960,12,2)
            },

            new Customer
            {
                Id = 2,
                CustomerId = 200,
                FirstName = "john",
                LastName = "doe",
                BirthDate = new DateOnly(2000,1,7),
                Addresses =
                [
                    new Address
                    {
                        CustomerId = 2,
                        Street = " 123 Main St  ",
                        City = "Any town",
                        State = "Ca  ",
                        ZipCode = "12345",
                        Country = "USA",
                        Phone = "  555-555-5555"
                    },
                    new Address
                    {
                        CustomerId = 2,
                        Street = "456 Main St",
                        City = "Some town",
                        State = Helpers.GetRandomStateAbbreviation(),
                        ZipCode = "12345",
                        Country = "USA",
                        Phone = "555-555-5566"
                    }
                ]
            },

            new Person
            {
                Id = 3,
                FirstName = "jane",
                LastName = "doe",
                BirthDate = new DateOnly(1978,8,15)
            },

            new Customer
            {
                Id = 4,
                CustomerId = 300,
                FirstName = "robert",
                LastName = "johnsoN",
                BirthDate = new DateOnly(1985,4,23)
            },

            new Customer
            {
                Id = 5,
                CustomerId = 400,
                FirstName = "liNda",
                LastName = "wiLLiams",
                BirthDate = new DateOnly(1992,11,9),
                Addresses =
                [
                    new Address
                    {
                        CustomerId = 400,
                        Street = "789 Oak Ave",
                        City = "Other town",
                        State = "Or",
                        ZipCode = "97035",
                        Country = "USA",
                        Phone = "555-555-5577"
                    }
                ]
            }
    ];
}
