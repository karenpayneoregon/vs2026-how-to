# About

Used to set workin folder of the sample from appsettings.json

```json
{
  "AppSettings": {
    "WorkingFolder": "C:\\Work"
  }
}
```

Code responsible for setting working folder and console window position.

`Config`.Configuration.JsonRoot() is an alias set in the .csproj file.

```csharp
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        var workingFolder = Config.Configuration
            .JsonRoot()
            .GetSection(nameof(AppSettings))[nameof(AppSettings.WorkingFolder)];


        if (Directory.Exists(workingFolder))
        {
            Directory.SetCurrentDirectory(workingFolder);
        }

        Console.Title = $"Code sample: Work folder {workingFolder}";

        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
```

## See also

Project `MsBuildWorkingDirApp` which only sets working folder using MSBuild property while running in Visual Studio.

