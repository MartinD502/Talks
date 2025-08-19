# Talks API

A .NET 8 Web API for managing speaker registrations and sessions at conferences/talks. This project demonstrates Clean Architecture principles with CQRS pattern implementation using MediatR.

## Architecture

This project follows Clean Architecture patterns with the following layers:

- **Talks.API** - Web API layer with controllers and configuration
- **Application** - Application services, commands, handlers, and DTOs
- **Domain** - Domain entities and business logic
- **Infrastructure** - Data access, repositories, and Entity Framework context
- **Utilities** - Shared utilities and enums
- **Talks.Tests** - Unit tests using XUnit and Moq

### Key Technologies & Patterns

- **.NET 8** with C# 12
- **Entity Framework Core** (Code-First approach)
- **MediatR** for CQRS implementation
- **Clean Architecture** with dependency inversion
- **XUnit & Moq** for testing
- **SQL Server** for data persistence

## ?? Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (Express edition is sufficient)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation & Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/MartinD502/Talks.git
   cd Talks
   ```

2. **Configure Database Connection**
   
   Update the connection string in `Talks.API/appsettings.json` to match your SQL Server instance:
   ```json
   {
     "ConnectionStrings": {
       "TalksDb": "Server=localhost;Database=TalksDb;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

3. **Install Dependencies**
   ```bash
   dotnet restore
   ```

4. **Create Database & Run Migrations**
   ```bash
   dotnet ef database update --project Infrastructure --startup-project Talks.API
   ```