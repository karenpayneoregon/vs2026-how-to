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

    /// <param name="context">The database context to check for the presence of tables.</param>
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
        [DebuggerStepThrough]
        public bool DatabaseExists() =>
            context.GetService<IDatabaseCreator>() is not RelationalDatabaseCreator databaseCreator ?
                throw new InvalidOperationException("Database creator service is not available.") :
                databaseCreator.Exists();
        
        /// <summary>
        /// Deletes the database if it exists, then creates it using the current EF Core model.
        /// </summary>
        /// <typeparam name="TContext">
        /// The type of the database context, which must derive from <see cref="DbContext"/>.
        /// </typeparam>
        /// <param name="context">The database context to check for the presence of tables.</param>
        /// <returns>
        /// A tuple indicating whether the operation succeeded and a related message.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the relational database creator service is not available in the provided context.
        /// </exception>
        [DebuggerStepThrough]
        public (bool success, string message) CreateDatabase()
        {
            if (context.GetService<IDatabaseCreator>() is not RelationalDatabaseCreator databaseCreator)
            {
                throw new InvalidOperationException("Relational database creator service is not available.");
            }

            try
            {
                var deleted = databaseCreator.EnsureDeleted();
                var created = databaseCreator.EnsureCreated();

                if (!created)
                {
                    return (false, "Database was not created. It may already exist.");
                }

                var message = deleted
                    ? "Existing database was deleted and recreated."
                    : "Database did not exist. A new database was created.";

                return (true, message);
            }
            catch (Exception exception)
            {
                return (false, exception.Message);
            }
        }


        /// <summary>
        /// Asynchronously deletes the database if it exists, then creates it using the current EF Core model.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
        /// </param>
        /// <typeparam name="TContext">
        /// The type of the database context, which must derive from <see cref="DbContext"/>.
        /// </typeparam>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a tuple indicating 
        /// whether the operation succeeded and a related message.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the relational database creator service is not available in the provided context.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<(bool success, string message)> CreateDatabaseAsync(CancellationToken cancellationToken = default)
        {
            if (context.GetService<IDatabaseCreator>() is not RelationalDatabaseCreator databaseCreator)
            {
                throw new InvalidOperationException("Relational database creator service is not available.");
            }

            try
            {
                var deleted = await databaseCreator.EnsureDeletedAsync(cancellationToken);
                var created = await databaseCreator.EnsureCreatedAsync(cancellationToken);

                if (!created)
                {
                    return (false, "Database was not created. It may already exist.");
                }

                var message = deleted
                    ? "Existing database was deleted and recreated."
                    : "Database did not exist. A new database was created.";

                return (true, message);
            }
            catch (Exception exception)
            {
                return (false, exception.Message);
            }
        }        /// <summary>
                 /// Determines whether the database associated with the provided <see cref="DbContext"/> contains any tables.
                 /// </summary>
                 /// <returns>
                 /// <c>true</c> if the database contains one or more tables; otherwise, <c>false</c>.
                 /// </returns>
                 /// <exception cref="InvalidOperationException">
                 /// Thrown when the relational database creator service is not available in the provided context.
                 /// </exception>
        [DebuggerStepThrough]
        public bool HasTables() =>
            context.GetService<IRelationalDatabaseCreator>() is not RelationalDatabaseCreator databaseCreator ?
                throw new InvalidOperationException("Database creator service is not available.") :
                databaseCreator.HasTables();

        /// <summary>
        /// Asynchronously determines whether the database associated with the given context contains any tables.
        /// </summary>
        /// <typeparam name="TContext">
        /// The type of the database context, which must derive from <see cref="DbContext"/>.
        /// </typeparam>
        /// <param name="context">The database context to check for the presence of tables.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result is <c>true</c> if the database contains any tables; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the relational database creator service is not available in the provided context.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<bool> HasTablesAsync(CancellationToken cancellationToken = default)
        {
            if (context.GetService<IRelationalDatabaseCreator>() is not RelationalDatabaseCreator databaseCreator)
            {
                throw new InvalidOperationException("Relational database creator service is not available.");
            }

            return await databaseCreator.HasTablesAsync(cancellationToken);
        }
        /// <summary>
        /// Determines whether a connection to the database associated with the provided <see cref="DbContext"/> 
        /// can be established.
        /// </summary>
        /// <typeparam name="TContext">
        /// The type of the database context, which must derive from <see cref="DbContext"/>.
        /// </typeparam>
        /// <returns>
        /// <c>true</c> if a connection to the database can be established; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Not thrown when the database creator service is not available in the provided context.
        /// </exception>
        public bool CanConnect()
        {
            return context.GetService<IDatabaseCreator>() is not RelationalDatabaseCreator databaseCreator ?
                throw new InvalidOperationException("Database creator service is not available.") :
                databaseCreator.CanConnect();
        }
        /// <summary>
        /// Asynchronously determines whether the database associated with the provided <see cref="DbContext"/> 
        /// can be connected to.
        /// </summary>
        /// <typeparam name="TContext">
        /// The type of the database context, which must derive from <see cref="DbContext"/>.
        /// </typeparam>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains 
        /// <c>true</c> if a connection to the database can be established; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Not thrown when the database creator service is not available in the provided context.
        /// </exception>
        public Task<bool> CanConnectAsync()
        {
            return context.GetService<IDatabaseCreator>() is not RelationalDatabaseCreator databaseCreator ?
                throw new InvalidOperationException("Database creator service is not available.") :
                databaseCreator.CanConnectAsync();
        }

        /// <summary>
        /// Determines whether the specified tables exist in the database associated with the given <see cref="DbContext"/>.
        /// </summary>
        /// <param name="tableNames">An array of table names to check for existence in the database.</param>
        /// <returns>
        /// <c>true</c> if all specified table names exist in the database; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method checks the database schema defined in the <see cref="DbContext.Model"/> to determine
        /// the existence of the specified tables. It does not execute a query against the database but instead
        /// relies on the metadata provided by Entity Framework Core.
        /// </remarks>
        [DebuggerStepThrough]
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

        /// <summary>
        /// Performs a comprehensive check on the database associated with the provided <see cref="DbContext"/>.
        /// </summary>
        /// <param name="tableNames">
        /// An array of table names to verify their existence in the database.
        /// </param>
        /// <returns>
        /// <c>true</c> if the database exists, contains tables, and includes all specified table names; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the required database services are not available in the provided context.
        /// </exception>
        [DebuggerStepThrough]
        public bool FullCheck(params string[] tableNames) => DatabaseExists(context) && HasTables(context) && TablesExist(context, tableNames);
    }
    
}
