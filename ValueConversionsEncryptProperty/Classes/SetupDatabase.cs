using Microsoft.EntityFrameworkCore;

namespace ValueConversionsEncryptProperty.Classes;

internal class SetupDatabase
{

    /// <summary>
    /// Deletes the existing database and recreates it.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/> instance used to interact with the database.</param>
    /// <remarks>
    /// This method ensures that the database is in a clean state by first deleting it and then recreating it.
    /// Use this method with caution, as it will remove all existing data in the database.
    /// </remarks>
    public static void CleanDatabase(DbContext context)
    {

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

    }

}