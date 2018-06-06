# DevTest

A simple .NET Core App which will save an email address and password to a database.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Installing

Clone the project to your local machine.

Open the project in Visual Studio and rebuild.

If you do not already have a local MSSQLLocalDB in your environment, create one with default settings.

Execute the following SQL command on your local MSSQLLocalDB:

```sql
CREATE DATABASE Members;
GO
USE Members;
GO
Create table Users(    
    Id int IDENTITY(1,1) PRIMARY KEY,    
    Email nvarchar(256) NULL,    
    Password nvarchar(450) NULL
)
```

The project is now ready and connected to your local database.

## Built With

* [ASP.NET Core 2.0](https://docs.microsoft.com/en-us/aspnet/core/)
* [ADO.NET](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/)
* [Nuget](https://www.nuget.org/)


## Authors

* **Danny Parker** - *Initial work* - [Danny-UKDM](https://github.com/Danny-UKDM)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details