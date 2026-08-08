using IndexMonths.Classes;
using SpectreConsoleLibrary.Core;
using System.Text;

namespace IndexMonths
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            SpectreConsoleHelpers.WindowTitle(Justify.Center, "Month Indexing");
            AnsiConsole.WriteLine();
            
            var monthContainer = Helpers.RangeDetails(Helpers.MonthNames());
            var table = CreateTable();
            monthContainer.ForEach(x => table.AddRow(x.ItemArray));

            AnsiConsole.Write(new Align(table, HorizontalAlignment.Center, VerticalAlignment.Top));

            SpectreConsoleHelpers.ExitPrompt();
        }
    }
}