using System;
using System.Collections.Generic;
using System.Text;
using ExperimentsApp.Interfaces;
using ExperimentsApp.Models;

namespace ExperimentsApp.Classes;

internal class MockData
{
    public static List<IPerson> PeopleList() =>
    [
        new Person { Id = 1, FirstName = "Alice", LastName = "Smith", BirthDate = new DateOnly(1990, 5, 15) ,
            Address = new Address { State = "OR", City = "Salem" }},
        
        new Person { Id = 2, FirstName = "Bob", LastName = "Johnson", BirthDate = new DateOnly(1985, 11, 23) },
        new Employee() { Id = 3, FirstName = "Bob", LastName = "Johnson", BirthDate = new DateOnly(1985, 11, 23), Badge = "AA1234"},
        new Person { Id = 4, FirstName = "Charlie", LastName = "Williams", BirthDate = new DateOnly(2001, 1, 1)
        }
    ];
    
    public static List<Money> MoneyList() =>
    [
        new(0m, "USD"),
        new(25m, "USD"),
        new(125m, "USD"),
        new(50m, "EUR"),
        new(-10m, "USD")
    ];
}

