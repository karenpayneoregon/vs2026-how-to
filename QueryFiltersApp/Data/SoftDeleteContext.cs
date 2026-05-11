using ConsoleConfigurationLibrary.Classes;
using Microsoft.EntityFrameworkCore;
using QueryFiltersApp.Models;

namespace QueryFiltersApp.Data;

public class SoftDeleteContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(AppConnections.Instance.MainConnection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region FilterConfiguration
        modelBuilder.Entity<Blog>().HasQueryFilter(b => !b.IsDeleted);
        #endregion
    }
    // The following overrides SaveChangesAsync to add logic which goes over all entities which the user deleted, and changes
    // them to be modified instead, setting the IsDeleted property to true.
    #region SaveChangesAsyncOverride
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();

        foreach (var item in ChangeTracker.Entries<Blog>().Where(e => e.State == EntityState.Deleted))
        {
            item.State = EntityState.Modified;
            item.CurrentValues["IsDeleted"] = true;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
    #endregion

    public override int SaveChanges()
        => throw new NotSupportedException("Use SaveChangesAsync instead.");
}

