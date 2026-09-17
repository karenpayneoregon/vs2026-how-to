using IComparerIEqualityComparerApp.Classes;
using IComparerIEqualityComparerApp.Classes.SystemCode;

namespace IComparerIEqualityComparerApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        Operations.DistinctPeople1();
        Operations.DistinctPeople2();
        Operations.CompareProducts();

        var peopleDataList = Operations.PeopleDataList();
        
        SpectreConsoleHelpers.ExitPrompt();
    }


}
