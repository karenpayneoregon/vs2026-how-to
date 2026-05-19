SELECT STRING_AGG(CAST (Id AS VARCHAR (20)), ',') WITHIN GROUP (ORDER BY Id) AS EmployeeIds
FROM   EF_Filters.dbo.Employees
WHERE  IsDeleted = 0;