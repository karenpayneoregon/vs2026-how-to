# About

EF Core SqlServerPropertyBuilderExtensions.[UseIdentityColumn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.sqlserverpropertybuilderextensions.useidentitycolumn?view=efcore-10.0) Method

Example of using the `UseIdentityColumn` method in the `OnModelCreating` method of a `DbContext` class to configure an identity column for the `CustomerId` property of the `Customer` entity.

- First id will be 10, and then it will increment by 10 for each new record.

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Customer>(entity =>
    {
        entity.ToTable("Customer");

        entity.Property(e => e.CustomerId)
            .UseIdentityColumn(seed: 10, increment: 10);
        
        entity.Property(e => e.LastName).IsRequired();
        entity.Property(e => e.LastName).IsRequired();
        
        
    });

    OnModelCreatingPartial(modelBuilder);
}
```
