using System.Diagnostics;
using EntityFrameworkLibrary;
using EnumHasConversion.Classes;
using EnumHasConversion.Models;
using Microsoft.EntityFrameworkCore;
using static ConfigurationLibrary.Classes.ConfigurationHelper;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

#pragma warning disable CS8618

namespace EnumHasConversion.Data;

public class WineContext : DbContext
{
    public DbSet<Wine> Wines { get; set; }
    //public DbSet<WineTypes> WineTypes { get; set; }

    /// <summary>
    /// Configures the database context options for the <see cref="WineContext"/> class.
    /// </summary>
    /// <param name="optionsBuilder">
    /// An instance of <see cref="DbContextOptionsBuilder"/> used to configure the database context.
    /// </param>
    /// <remarks>
    /// This method sets up the SQL Server connection using a connection string and enables logging of database commands
    /// to a file using <see cref="DbContextToFileLogger"/>. The logging level is set to <see cref="LogLevel.Information"/>.
    /// </remarks>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(ConnectionString())
            .LogTo(new DbContextToFileLogger().Log,
                [DbLoggerCategory.Database.Command.Name],
                LogLevel.Information);

    /// <summary>
    /// Configures the model for the <see cref="WineContext"/> class.
    /// </summary>
    /// <param name="modelBuilder">
    /// An instance of <see cref="ModelBuilder"/> used to configure the entity mappings and relationships.
    /// </param>
    /// <remarks>
    /// This method sets up the following configurations:
    /// <list type="bullet">
    /// <item>
    /// Conversion of the <see cref="Wine.WineType"/> property to and from an integer for database storage.
    /// </item>
    /// <item>
    /// Seed data for the <see cref="WineTypes"/> and <see cref="Wine"/> entities using the <see cref="MockedData"/> class.
    /// </item>
    /// </list>
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder
            .Entity<Wine>()
            .Property(e => e.WineType)
            .HasConversion<int>();

        modelBuilder.Entity<WineTypes>().HasData(MockedData.WineTypes());
        modelBuilder.Entity<Wine>().HasData(MockedData.Wines());
        
        
    }
}