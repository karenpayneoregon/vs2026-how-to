using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace EntityFrameworkLibrary;

/// <summary>
/// Provides helper methods for working with <see cref="DbContext"/> instances in Entity Framework Core.
/// </summary>
/// <remarks>
/// This static class includes extension methods to check the existence of a database and determine
/// whether the database contains any tables. It relies on services provided by Entity Framework Core
/// to perform these operations.
/// </remarks>
internal static class DbContextHelpers
{
    /// <param name="context">The database context to check for the existence of the database.</param>
    extension<TContext>(TContext context) where TContext : DbContext
    {
        /// <summary>
        /// Checks if the database associated with the provided <see cref="DbContext"/> exists.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the database exists; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the database creator service is not available in the provided context.
        /// </exception>
        [DebuggerStepThrough]
        public bool DatabaseExists() =>
            context.GetService<IDatabaseCreator>() is not RelationalDatabaseCreator databaseCreator ?
                throw new InvalidOperationException("Database creator service is not available.") :
                databaseCreator.Exists();
    }

    /// <summary>
    /// Determines whether the database associated with the provided <see cref="DbContext"/> contains any tables.
    /// </summary>
    /// <typeparam name="TContext">The type of the database context, which must derive from <see cref="DbContext"/>.</typeparam>
    /// <param name="context">The database context to check for the presence of tables.</param>
    /// <returns>
    /// <c>true</c> if the database contains one or more tables; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the relational database creator service is not available in the provided context.
    /// </exception>
    [DebuggerStepThrough]
    public static bool HasTables<TContext>(this TContext context) where TContext : DbContext =>
        context.GetService<IRelationalDatabaseCreator>() is not RelationalDatabaseCreator databaseCreator ?
            throw new InvalidOperationException("Database creator service is not available.") :
            databaseCreator.HasTables();

    [DebuggerStepThrough]
    public static bool TablesExist(this DbContext context, params string[] tableNames)
    {

        var existingTables = new HashSet<string>(
            context.Model.GetEntityTypes()
                .Select(t => t.GetTableName())
                .Where(t => !string.IsNullOrEmpty(t))
                .Cast<string>()
        );

        return existingTables.IsSupersetOf(tableNames);
    }

    [DebuggerStepThrough]
    public static bool FullCheck<TContext>(this TContext context, params string[] tableNames) where TContext : DbContext
        => DatabaseExists(context) && HasTables(context) && TablesExist(context, tableNames);
    
    
}
