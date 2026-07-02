using Microsoft.EntityFrameworkCore;

namespace HasConversion_Bool_ColorApp.Classes;

public class Helpers
{
    /// <summary>
    /// Deletes the existing database and recreates it along with its schema.
    /// </summary>
    /// <param name="context">
    /// The <see cref="DbContext"/> instance used to interact with the database.
    /// </param>
    /// <remarks>
    /// This method ensures that the database is in a clean state by first deleting it 
    /// (if it exists) and then recreating it. Use this method cautiously as it will 
    /// remove all existing data in the database.
    /// </remarks>
    public static void CleanDatabase(DbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
}