# About


Use AI to read JSON data from colums.

- Reverse engineer with `EF Core Power Tools` to create models and DbContext
- Modified DbContext to use owned types for JSON columns using ChatGPT
- Create `(localdb)\MSSQLLocalDB\EF.JsonHybrid` database
  - Run `SQL\Populate.sql` to create and populate the Applications table

## Prompt

Attached Data\Context.cs

```text

C# EF Core
    - MailSettings and GeneralSettings contain JSON
    - create needed models
    - modify OnModelCreating

Sample JSON
SELECT ApplicationId,
    ApplicationName,
    ContactName,****
    JSON_VALUE(MailSettings, '$.FromAddress') AS FromAddress,
    JSON_VALUE(MailSettings, '$.Host') AS Host,
    JSON_VALUE(GeneralSettings, '$.MainDatabaseConnection') AS Connection,
    JSON_VALUE(GeneralSettings, '$.ServicePath') AS [ServicePath]
FROM dbo.Applications;[ 
{
    "ApplicationId": 1,
    "ApplicationName": "ACED",
    "ContactName": "Kim Jenkins",
    "MailSettings": "{\"FromAddress\":\"FromAddressAced\",\"Host\":\"AcedHost\",\"PickupFolder\":\"C:\\\\MailDrop\",\"Port\":15,\"TimeOut\":2000}",
    "GeneralSettings": "{\"MainDatabaseConnection\":\"Data Source=.\\\\sqlexpress;Initial Catalog=WorkingWithDate;Integrated Security=True;Encrypt=False\",\"ServicePath\":\"http://localhost:11111/api/\"}"
},
{
    "ApplicationId": 2,
    "ApplicationName": "SIDES",
    "ContactName": "Mike Adams",
    "MailSettings": "{\"FromAddress\":\"FromAddressSides\",\"Host\":\"SidesHost\",\"PickupFolder\":\"C:\\\\MailDrop\",\"Port\":15,\"TimeOut\":2000}",
    "GeneralSettings": "{\"MainDatabaseConnection\":\"Data Source=.\\\\sqlexpress;Initial Catalog=WorkingWithTime;Integrated Security=True;Encrypt=False\",\"ServicePath\":\"http://localhost:22222/api/\"}"
}
]

```

## Response

Below is the clean EF Core approach: model MailSettings and GeneralSettings as owned JSON objects instead of leaving them as raw string values.

Your current context has Applications as the entity and currently marks GeneralSettings and MailSettings as required scalar properties . Replace that scalar mapping with owned JSON mappings.

**Application model**

```csharp
#nullable disable

namespace EF_JsonHybridSample.Models;

public class Applications
{
    public int ApplicationId { get; set; }

    public string ApplicationName { get; set; }

    public string ContactName { get; set; }

    public MailSettings MailSettings { get; set; } = new();

    public GeneralSettings GeneralSettings { get; set; } = new();
}
```

**MailSettings model**

```csharp

#nullable disable

namespace EF_JsonHybridSample.Models;

public class MailSettings
{
    public string FromAddress { get; set; }

    public string Host { get; set; }

    public string PickupFolder { get; set; }

    public int Port { get; set; }

    public int TimeOut { get; set; }
}
```

**GeneralSettings model**

```csharp

#nullable disable

namespace EF_JsonHybridSample.Models;

public class GeneralSettings
{
    public string MainDatabaseConnection { get; set; }

    public string ServicePath { get; set; }
}
```

**Modified OnModelCreating**

Replace your existing OnModelCreating entity configuration with this:

```csharp

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Applications>(entity =>
    {
        entity.HasKey(e => e.ApplicationId);

        entity.Property(e => e.ApplicationName)
            .IsRequired();

        entity.Property(e => e.ContactName)
            .IsRequired();

        entity.OwnsOne(e => e.MailSettings, mail =>
        {
            mail.ToJson();

            mail.Property(e => e.FromAddress)
                .HasColumnName(nameof(MailSettings.FromAddress));

            mail.Property(e => e.Host)
                .HasColumnName(nameof(MailSettings.Host));

            mail.Property(e => e.PickupFolder)
                .HasColumnName(nameof(MailSettings.PickupFolder));

            mail.Property(e => e.Port)
                .HasColumnName(nameof(MailSettings.Port));

            mail.Property(e => e.TimeOut)
                .HasColumnName(nameof(MailSettings.TimeOut));
        });

        entity.OwnsOne(e => e.GeneralSettings, general =>
        {
            general.ToJson();

            general.Property(e => e.MainDatabaseConnection)
                .HasColumnName(nameof(GeneralSettings.MainDatabaseConnection));

            general.Property(e => e.ServicePath)
                .HasColumnName(nameof(GeneralSettings.ServicePath));
        });

        entity.Navigation(e => e.MailSettings)
            .IsRequired();

        entity.Navigation(e => e.GeneralSettings)
            .IsRequired();
    });

    OnModelCreatingPartial(modelBuilder);
}
```

**Example usage**


```csharp

await using var context = new Context();

var applications = await context.Applications
    .AsNoTracking()
    .Select(app => new
    {
        app.ApplicationId,
        app.ApplicationName,
        app.ContactName,
        app.MailSettings.FromAddress,
        app.MailSettings.Host,
        app.GeneralSettings.MainDatabaseConnection,
        app.GeneralSettings.ServicePath
    })
    .ToListAsync();
    ```
    