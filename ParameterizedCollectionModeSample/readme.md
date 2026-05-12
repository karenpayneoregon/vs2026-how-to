# About

LINQ and SQL translation/Improved translation for [parameterized collection](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew#improved-translation-for-parameterized-collection) sample

Sample logs

```text
info: 5/12/2026 07:44:04.901 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (498ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT [e].[Id], [e].[FirstName], [e].[IsDeleted], [e].[IsManager], [e].[LastName]
      FROM [Employees] AS [e]
      WHERE [e].[Id] IN (1, 2, 3, 8, 10)
----------------------------------------
info: 5/12/2026 07:45:42.192 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (267ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT [e].[Id], [e].[FirstName], [e].[IsDeleted], [e].[IsManager], [e].[LastName]
      FROM [Employees] AS [e]
      WHERE [e].[Id] IN (?, ?, ?, ?, ?)
----------------------------------------
```