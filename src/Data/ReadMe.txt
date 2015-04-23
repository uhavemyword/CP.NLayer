Overall:
The project is data access layer which using EntityFramework (Code First) as the ORM.

Notes:
1. By using "Repository" and "Unit of Work" pattern, the instance of "UnitOfWork" is the only channel for other layers to access data.
See http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
2. EF Migrations reference: http://msdn.microsoft.com/en-us/data/jj591621.aspx
Enable-Migrations
Add-Migration
Update-Database
Get-Migrations

