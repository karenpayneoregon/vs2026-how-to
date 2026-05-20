using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace EntityFrameworkLibrary;

/// <summary>
/// Provides utility methods for working with SQL Server databases in the context of Entity Framework Core.
/// </summary>
/// <remarks>
/// This class includes methods to check the existence of a database, verify the presence of tables,
/// and perform comprehensive checks on the database associated with a given <see cref="DbContext"/>.
/// </remarks>
public static class SqlServerContextHelpers
{
    /// <param name="context">The database context to check for the existence of the database.</param>
    /// <typeparam name="TContext">The type of the database context, which must derive from <see cref="DbContext"/>.</typeparam>
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
        public bool DatabaseExists()
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            var creator = context.GetService<IDatabaseCreator>();
            if (creator is null)
                throw new InvalidOperationException("IDatabaseCreator service is not available on the DbContext.");

            if (creator is not RelationalDatabaseCreator relationalCreator)
                throw new InvalidOperationException("A relational database creator is required to check database existence.");

            return relationalCreator.Exists();
        }

        /// <summary>
        /// Determines whether the database associated with the provided <see cref="DbContext"/> contains any tables.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the database contains one or more tables; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the relational database creator service is not available in the provided context.
        /// </exception>
        public bool HasTables() =>
            context.GetService<IRelationalDatabaseCreator>() is not RelationalDatabaseCreator databaseCreator ?
                throw new InvalidOperationException("Database creator service is not available.") :
                databaseCreator.HasTables();
    }

    /// <param name="context">The database context to check for the presence of the specified tables.</param>
    extension(DbContext context)
    {
        /// <summary>
        /// Checks whether the specified tables exist in the database associated with the provided <see cref="DbContext"/>.
        /// </summary>
        /// <param name="tableNames">An array of table names to check for existence in the database.</param>
        /// <returns>
        /// <c>true</c> if all specified tables exist in the database; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="context"/> or <paramref name="tableNames"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the model metadata in the provided context is not properly configured.
        /// </exception>
        public bool TablesExist(params string[] tableNames)
        {

            var existingTables = new HashSet<string>(
                context.Model.GetEntityTypes()
                    .Select(t => t.GetTableName())
                    .Where(t => !string.IsNullOrEmpty(t))
                    .Cast<string>()
            );

            return existingTables.IsSupersetOf(tableNames);
        }
    }

    /// <summary>
    /// Performs a comprehensive check on the database associated with the provided <see cref="DbContext"/>.
    /// This includes verifying the existence of the database, the presence of existence of specific tables
    /// specified by their names.
    /// </summary>
    /// <typeparam name="TContext">The type of the database context, which must derive from <see cref="DbContext"/>.</typeparam>
    /// <param name="context">The database context to perform the checks on.</param>
    /// <param name="tableNames">An array of table names to verify their existence in the database.</param>
    /// <returns>
    /// <c>true</c> if the database exists, contains tables, and all specified table names are present; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the required database services are not available in the provided context.
    /// </exception>
    public static bool FullCheck<TContext>(this TContext context, params string[] tableNames) where TContext : DbContext
        => DatabaseExists(context) && HasTables(context) && TablesExist(context, tableNames);
}
