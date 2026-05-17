using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QueryFiltersApp.Classes;

public static class DbContextExtensions
{
    extension(DbContext context)
    {
        public (bool success, IReadOnlyCollection<IQueryFilter>? collection) GetQueryFilters<TEntity>() where TEntity : class
        {
            var entityType = context.Model.FindEntityType(typeof(TEntity));
            var filters = entityType?.GetDeclaredQueryFilters();
            return filters is { Count: > 0 } ? (true, filters) : (false, []);
        }

        public IReadOnlyCollection<IQueryFilter> TryGetQueryFilters<TEntity>() where TEntity : class
        {
            var entityType = context.Model.FindEntityType(typeof(TEntity)); 
            var filters = entityType?.GetDeclaredQueryFilters(); 
            return filters ?? [];
        }
        
    }
}