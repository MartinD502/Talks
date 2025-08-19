# Talks API - Speaker Registration System

A .NET 8 Web API application for managing speaker registrations and session approvals for conferences and talks.

## ??? Architecture

This project follows Clean Architecture principles with the following layers:

- **Talks.API** - Web API controllers and configuration
- **Application** - Business logic, commands, handlers, and DTOs
- **Domain** - Core entities and business rules
- **Infrastructure** - Data access, Entity Framework, and repositories
- **Utilities** - Common constants and enums
- **Talks.Tests** - Unit tests using XUnit

## ?? Features

- **Speaker Registration** - Register speakers with validation rules
- **Session Approval** - Automatic approval/rejection of sessions based on content
- **Pricing Service** - Calculate registration fees based on experience
- **Validation Service** - Comprehensive speaker validation logic

## ??? Technologies

- **.NET 8** - Latest .NET framework
- **ASP.NET Core Web API** - RESTful API framework
- **Entity Framework Core** - ORM for data access
- **MediatR** - Command/Query handling pattern
- **SQL Server** - Database
- **XUnit** - Testing framework

## ?? API Endpoints

### Speaker Registration
```
POST /speaker/RegisterSpeaker
```
Register a new speaker with sessions.

### Demo Endpoint
```
GET /speaker/Run
```
Demo endpoint showing speaker registration flow.

## ????? Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Running the Application

1. Clone the repository:
```bash
git clone https://dev.azure.com/martinduncan2000s/Talks/_git/Talks
```

2. Navigate to the project directory:
```bash
cd Talks
```

3. Restore packages:
```bash
dotnet restore
```

4. Update the database connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "TalksDb": "Server=(localdb)\\mssqllocaldb;Database=TalksDb;Trusted_Connection=true;"
  }
}
```

5. Run Entity Framework migrations:
```bash
dotnet ef database update --project Infrastructure --startup-project Talks.API
```

6. Run the application:
```bash
dotnet run --project Talks.API
```

7. Test the API:
- Navigate to: `http://localhost:5121/speaker/Run`
- Or use tools like Postman to test the endpoints

## ?? Testing

Run unit tests:
```bash
dotnet test
```

The project includes comprehensive unit tests for:
- Speaker validation logic
- Session approval rules
- Pricing calculations
- Controller endpoints

## ?? Project Structure

```
Talks/
??? Application/           # Business logic layer
?   ??? Commands/         # CQRS Commands
?   ??? Dtos/            # Data Transfer Objects
?   ??? Handlers/        # Command/Query handlers
?   ??? Services/        # Business services
?   ??? _Interfaces/     # Service interfaces
??? Domain/              # Core domain entities
??? Infrastructure/      # Data access layer
?   ??? Contexts/       # Entity Framework contexts
?   ??? Migrations/     # EF migrations
?   ??? Repositories/   # Data repositories
??? Talks.API/          # Web API layer
??? Talks.Tests/        # Unit tests
??? Utilities/          # Common utilities
```

## ?? Business Rules

### Speaker Validation
- First Name, Last Name, and Email are required
- High experience (>10 years) OR blog OR certifications (>3) require approved employers
- Low experience speakers need valid email domains and modern browsers

### Session Approval
- Sessions with legacy technologies (Cobol, Commodore, Punch Cards, VBScript) are automatically rejected
- Modern technology sessions are approved

### Pricing
- Registration fees calculated based on years of experience

## ?? Author

**Martin Duncan** - Created 19/08/2025

## ?? License

This project is for demonstration purposes.

## ?? Repository

- **Azure DevOps**: https://dev.azure.com/martinduncan2000s/Talks/_git/Talks
- **Public Clone URL**: `https://dev.azure.com/martinduncan2000s/Talks/_git/Talks`

---

*Note: This is a demonstration project showcasing .NET 8, Clean Architecture, Entity Framework Core, and comprehensive unit testing practices.*