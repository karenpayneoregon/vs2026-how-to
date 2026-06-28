# About

Uses `OwnsOne` to mimic a Dictionary as a property.


```csharp
public partial class Dictionary
{
    public int Id { get; set; }

    public DataEntity Data { get; set; }
}

public class DataEntity
{
    public string Key { get; set; }
    public string Value { get; set; }
}
```

**DbContext**

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.HasSequence<int>("seq_test").HasMin(1L);


    modelBuilder.Entity<Dictionary>().OwnsOne(
        owner => owner.Data, ownedNavigationBuilder =>
        {
            ownedNavigationBuilder.ToJson();
        });

    OnModelCreatingPartial(modelBuilder);
}
```
