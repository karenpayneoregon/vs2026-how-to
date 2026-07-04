#nullable disable
using ConsoleConfigurationLibrary.Classes;
using EntityCoreFileLogger;
using HasConversionDictionary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HasConversionDictionary.Data;

public partial class DictionaryContext : DbContext
{
    public DictionaryContext() { }

    public DictionaryContext(DbContextOptions<DictionaryContext> options) : base(options) { }

    public virtual DbSet<DictionaryItem> Dictionary { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .LogTo(new DbContextToFileLogger().Log,
                [DbLoggerCategory.Database.Command.Name],
                LogLevel.Information)
            .UseSqlServer(AppConnections.Instance.MainConnection)
            .EnableSensitiveDataLogging();

    /// <summary>
    /// Configures the model for the context during the creation of the model.
    /// </summary>
    /// <param name="modelBuilder">
    /// An instance of <see cref="ModelBuilder"/> used to configure the model for the context.
    /// </param>
    /// <remarks>
    /// This method is overridden to define custom configurations for the Entity Framework Core model.
    /// It sets up a sequence named <c>seq_test</c> with a minimum value of 1 and configures the 
    /// <see cref="Dictionary"/> entity to store its <see cref="DataEntity"/> property as JSON in the database.
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.HasSequence<int>("seq_test").HasMin(1L);

        modelBuilder.Entity<DictionaryItem>().OwnsOne(
            owner => owner.Data, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            });

    }
}