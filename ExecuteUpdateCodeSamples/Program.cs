using ExecuteUpdateCodeSamples.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Spectre.Console;
using SpectreConsoleLibrary.Core;

namespace ExecuteUpdateCodeSamples;

internal partial class Program
{
    private static async Task Main(string[] args)
    {
        DisplayBooksTable(10);

        await PromotionUpdaterAsync(10, true);

        DisplayBooksTable(10);
        
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Displays a table of books filtered by the specified rating in the console.
    /// </summary>
    /// <param name="rating">
    /// The rating used to filter the books displayed in the table.
    /// </param>
    /// <remarks>
    /// This method retrieves books from the database with the specified rating, creates a formatted table
    /// using Spectre.Console, and displays the table in the console. The table includes columns for
    /// book ID, name, price, and rating.
    /// </remarks>
    private static void DisplayBooksTable(int rating)
    {

        SpectreConsoleHelpers.PrintPink();

        using var context = new Context();
        var books = context.Books.Where(x => x.Rating == rating).ToList();

        var table = CreateBooksTable();

        foreach (var book in books)
        {
            table.AddRow(book.Id.ToString(), book.BookName, book.Price.ToString("C"), book.Rating.ToString());
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }
    
    /// <summary>
    /// Updates the properties of books in the database based on the specified rating and promotion flag.
    /// </summary>
    /// <param name="rating">
    /// The rating value used to filter books for the update operation.
    /// </param>
    /// <param name="applyPromotion">
    /// A boolean flag indicating whether a promotion should be applied to the book prices.
    /// If <c>true</c>, a discount is applied to the price of the books.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    /// <remarks>
    /// This method uses Entity Framework Core's <c>ExecuteUpdateAsync</c> to perform bulk updates
    /// on the database. If an exception occurs during the operation, it is logged using Serilog.
    /// </remarks>
    private static async Task PromotionUpdaterAsync(int rating, bool applyPromotion)
    {

        SpectreConsoleHelpers.PrintPink();

        await using var context = new Context();
        try
        {

            await context.Books
                .Where(b => b.Rating == rating)
                .ExecuteUpdateAsync(s =>
                {
                    if (applyPromotion)
                    {
                        // apply discount to Price
                        s.SetProperty(b => b.Price, b => b.Price * 0.9m);
                    }

                });
        }
        catch (Exception exception)
        {
            Log.Error(exception, nameof(PromotionUpdaterAsync));
        }
    }   

    /// <summary>
    /// Creates a Spectre.Console table configured to display book details.
    /// </summary>
    /// <returns>
    /// A <see cref="Spectre.Console.Table"/> instance with predefined columns for displaying
    /// book information such as Id, Book Name, Price, and Rating.
    /// </returns>
    /// <remarks>
    /// The table is styled with a rounded border and a title labeled "Books" in HotPink.
    /// Each column is formatted with bold HotPink text.
    /// </remarks>
    private static Table CreateBooksTable()
    {
        var table = new Table();
        table.Title("[HotPink]Books[/]");
        table.Border(TableBorder.Rounded);
        table.AddColumn("[bold HotPink]Id[/]");
        table.AddColumn("[bold HotPink]Book Name[/]");
        table.AddColumn("[bold HotPink]Price[/]");
        table.AddColumn("[bold HotPink]Rating[/]");
        return table;
    }
}
