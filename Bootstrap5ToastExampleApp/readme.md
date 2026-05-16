# About

A demonstration of a Bootstrap toast that reads settings from `appsettings.json` using dependency injection.

:bulb: Setting are read using the `IOptions<T>` pattern while other options are hard coded in a page.

## Settings model

```csharp
public class ToastOptions
{
    public string? ToastMessage { get; set; }

    public string? ToastTitle { get; set; }

    public int ToastDelay { get; set; }
}
```

appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ToastOptions": {
    "ToastMessage": "Failed to open database!",
    "ToastTitle": "Error",
    "ToastDelay": 10000
  }
}
```

## Dependency injection - Program.cs

```csharp
builder.Services.Configure<ToastOptions>(builder.Configuration.GetSection(nameof(ToastOptions)));

builder.Services.AddScoped<ReadToast>();
```

## Index page

- Uses primary constructor injection to read the toast options.

```csharp
public class IndexModel(ReadToast readToast) : PageModel
```

- Form post `asp-page-handler="ShowToast"`

```csharp
public IActionResult OnPostShowToast()
{
    var options = readToast.GetToastOptions();
    ToastTitle = options.ToastTitle;
    ToastMessage = options.ToastMessage;
    ToastDelay = options.ToastDelay;

    return RedirectToPage();
        
}
```
