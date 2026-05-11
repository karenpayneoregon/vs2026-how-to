using Microsoft.EntityFrameworkCore;
using QueryFiltersApp.Classes.Core;
using QueryFiltersApp.Data;
using Spectre.Console;

namespace QueryFiltersApp;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        SpectreConsoleHelpers.WindowTitle(Justify.Left);
        SpectreConsoleHelpers.PinkPill(Justify.Left, "Hello world");

        await SoftDeleteSample();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }


    public static async Task SoftDeleteSample()
    {
        // First, create the database and add some data to it
        using (var context = new SoftDeleteContext())
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            context.Blogs.AddRange(
                new() { Name = "John's blog" },
                new() { Name = "Mary's blog" });
            await context.SaveChangesAsync();
        }

        // Let's delete a blog. Note that although our code seems to delete the blog in the regular way,
        // our override of SaveChangesAsync below will actually modify it instead, setting the IsDeleted property to true.
        using (var context = new SoftDeleteContext())
        {
            var blog = await context.Blogs.FirstAsync(b => b.Name == "John's blog");
            context.Blogs.Remove(blog);
            await context.SaveChangesAsync();
        }

        // Now, let's query out all blogs. The global query filter will ensure that John's blog is not returned, because it has been soft-deleted.
        using (var context = new SoftDeleteContext())
        {
            AnsiConsole.MarkupLine("[cyan]Blogs:[/]");
            await foreach (var blog in context.Blogs)
            {
                Console.WriteLine(blog.Name);
            }
        }

        // Finally, for auditing reasons, let's now query out all blogs, John's blog is returned even though it has been soft-deleted.
        using (var context = new SoftDeleteContext())
        {
            AnsiConsole.MarkupLine("[yellow]Blogs (including soft-deleted ones):[/]");
            #region DisableFilter
            var allBlogs = await context.Blogs.IgnoreQueryFilters().ToListAsync();
            #endregion

            foreach (var blog in allBlogs)
            {
                Console.WriteLine(blog.Name);
            }
        }
    }

}
