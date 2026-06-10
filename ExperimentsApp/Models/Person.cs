using ExperimentsApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ExtensionsLibrary;

namespace ExperimentsApp.Models;


public class Person : IPerson
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public int Age => BirthDate.GetAge();
    public Address? Address { get; set; }
}