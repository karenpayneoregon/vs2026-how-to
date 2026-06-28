using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EntityFrameworkLibrary;

public static class DatabaseService
{

    /// <summary>
    /// Extracts the database name from a given SQL Server connection string.
    /// </summary>
    /// <param name="connectionString">
    /// The connection string from which the database name will be extracted.
    /// </param>
    /// <returns>
    /// The name of the database specified in the connection string.
    /// </returns>
    public static string DatabaseName(string connectionString)
        => new SqlConnectionStringBuilder(connectionString).InitialCatalog;
}
