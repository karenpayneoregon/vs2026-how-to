using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using ConsoleApp1.Classes;

namespace ConsoleApp1;

internal class Program
{
    static void Main()
    {
        string firstName = EnviromentHelpers.GetCurrentUserFirstName();

        Console.WriteLine($"Hello, {firstName}!");
        Console.ReadLine();
    }


}