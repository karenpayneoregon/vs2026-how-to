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

    public virtual DbSet<Dictionary> Dictionary { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .LogTo(new DbContextToFileLogger().Log,
                [DbLoggerCategory.Database.Command.Name],
                LogLevel.Information)
            .UseSqlServer(AppConnections.Instance.MainConnection)
            .EnableSensitiveDataLogging();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("seq_test").HasMin(1L);
        modelBuilder.Entity<Dictionary>().OwnsOne(
            owner => owner.Data, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            });
    }
}