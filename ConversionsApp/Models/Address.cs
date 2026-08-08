using System;
using System.Collections.Generic;
using System.Text;

namespace ConversionsApp.Models;

public class Address
{
    public Address()
    {
    }

    public Address(string street, string city, string postcode)
    {
        Street = street;
        City = city;
        Postcode = postcode;
    }

    public string Street { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Postcode { get; set; } = string.Empty;
}