using ConsoleConfigurationLibrary.Classes;
using EntityCoreFileLogger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ValueConversionsEncryptProperty.Models;


namespace ValueConversionsEncryptProperty.Data;

public class Context : DbContext
{

    /// <summary>
    /// Configures the model for the database context.
    /// </summary>
    /// <param name="modelBuilder">
    /// Provides a simple API for configuring the shape of entities, the relationships between them, 
    /// and how they map to the database schema.
    /// </param>
    /// <remarks>
    /// This method applies a value conversion to the <see cref="User.Password"/> property, 
    /// ensuring that passwords are hashed before being stored in the database.
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(e => e.Password).HasConversion(
            v => BC.HashPassword(v),
            v => v);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .LogTo(new DbContextToFileLogger().Log,
                [DbLoggerCategory.Database.Command.Name],
                LogLevel.Information)
            .UseSqlServer(AppConnections.Instance.MainConnection)
            .EnableSensitiveDataLogging();
}