# About

Update `NET9` to `NET10` as a VS2026 external tool.

This tool is intended to be used as an external tool in `Visual Studio 2026`.

It will update the target framework of the selected .NET project in `solution explorer` from `NET9` to `NET10`.

## External Tool Command configuration

**Command:** `Path\UpdateFrameworkApp.exe`

**Arguments:** `$(ProjectDir)$(ProjectFileName)`

**Initial directory:** `$(ProjectDir)`