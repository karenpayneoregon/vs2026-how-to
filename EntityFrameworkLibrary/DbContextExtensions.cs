
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFrameworkLibrary;

public static class DbContextExtensions
{
    /// <param name="context">The <see cref="DbContext"/> instance to inspect.</param>
    extension(DbContext context)
    {
        /// <summary>
        /// Determines whether the specified entity type has a query filter applied in the current <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to check for a query filter.</typeparam>
        /// <returns>
        /// <see langword="true"/> if the specified entity type has a query filter applied; otherwise, <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// This method checks the model metadata of the provided <see cref="DbContext"/> to determine if a query filter
        /// is defined for the specified entity type.
        /// </remarks>
        public bool HasQueryFilter<TEntity>()  where TEntity : class
        {
            var entityType = context.Model.FindEntityType(typeof(TEntity));
            return entityType?.GetDeclaredQueryFilters() != null;
        }

        /// <summary>
        /// Retrieves the collection of query filters applied to the specified entity type in the current <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity for which the query filters are retrieved.</typeparam>
        /// <returns>
        /// A read-only collection of <see cref="IQueryFilter"/> instances representing the query filters applied to the entity type,
        /// or <see langword="null"/> if no query filters are defined.
        /// </returns>
        /// <remarks>
        /// This method inspects the model metadata of the provided <see cref="DbContext"/> to retrieve any query filters
        /// defined for the specified entity type.
        /// </remarks>
        public IReadOnlyCollection<IQueryFilter>? GetQueryFilters<TEntity>() where TEntity : class
        {
            var entityType = context.Model.FindEntityType(typeof(TEntity));
            return entityType?.GetDeclaredQueryFilters();
        }

    }
}

