# Talks API

A .NET 8 Web API for managing speaker registrations and sessions at conferences/talks. This project demonstrates Clean Architecture principles with CQRS pattern implementation using MediatR.

## ??? Architecture

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

5. **Build the Solution**
   ```bash
   dotnet build
   ```

6. **Run the Application**
   ```bash
   dotnet run --project Talks.API
   ```

   The API will be available at: `http://localhost:5121`

## ?? API Endpoints

### Speaker Registration

#### POST `/speaker/RegisterSpeaker`
Registers a new speaker with their sessions.

**Request Body:**
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "employer": "Tech Corp",
  "yearsOfExperience": 5,
  "blogUrl": "https://johndoe.blog",
  "isBlog": true,
  "certifications": ["Azure", "AWS"],
  "webBrowser": {
    "name": "Chrome",
    "majorVersion": 120
  },
  "sessions": [
    {
      "title": "Introduction to .NET 8",
      "description": "Overview of new features"
    }
  ]
}
```

**Response:**
Returns registration result status (Success, EmailRequired, FirstNameRequired, etc.)

#### GET `/speaker/Run`
Example endpoint that demonstrates speaker registration with sample data.

**Usage:** Navigate to `http://localhost:5121/speaker/Run` to test the registration flow.

## ?? Testing

Run the unit tests:
```bash
dotnet test
```

The test project includes:
- Unit tests for services and handlers
- Mocking with Moq framework
- XUnit test framework

## ??? Project Structure

```
Talks/
??? Application/              # Application layer
?   ??? Commands/            # CQRS commands
?   ??? Handlers/            # Command/Query handlers
?   ??? Services/            # Application services
?   ??? Dtos/               # Data transfer objects
?   ??? _Interfaces/        # Service interfaces
??? Domain/                  # Domain layer
?   ??? Entities/           # Domain entities
??? Infrastructure/          # Infrastructure layer
?   ??? Contexts/           # EF DbContext
?   ??? Repositories/       # Data repositories
?   ??? Migrations/         # EF migrations
??? Talks.API/              # Web API layer
?   ??? Controllers/        # API controllers
??? Talks.Tests/            # Test project
??? Utilities/              # Shared utilities
??? README.md
```

## ?? Business Rules

The API implements several sophisticated business rules for speaker registration:

### 1. **Required Fields Validation:**
- Email is required
- First name and last name are required
- At least one session must be provided

### 2. **Speaker Qualification Rules:**

**High-Quality Speakers (Auto-Approved):**
- 10+ years of experience AND valid employer (Google, Microsoft, Apple, etc.) OR
- Has a blog AND valid employer OR  
- 3+ certifications AND valid employer

**Standard Speakers:**
- Less than 10 years experience
- Must use modern email domains (not aol.com, compuserve.com, prodigy.com)
- Cannot use Internet Explorer version < 9

### 3. **Session Approval:**
- Sessions are automatically evaluated for approval based on content
- Only speakers with at least one approved session can be registered

### 4. **Registration Fees:**
- Calculated based on years of experience
- Applied during the registration process

### 5. **Browser Compatibility Checks:**
- Tracks speaker's browser information
- Legacy browser users (IE < 9) may be rejected

## ??? Database Schema

The application uses Entity Framework Core with the following main entities:

- **Speaker** - Speaker information and registration details
- **Session** - Individual talk sessions proposed by speakers  
- **Certification** - Speaker certifications and qualifications

## ?? Registration Results

The API returns the following registration statuses:

- `Success` - Speaker registered successfully
- `EmailRequired` - Email address is missing
- `FirstNameRequired` - First name is missing
- `LastNameRequired` - Last name is missing
- `NoSessionsProvided` - No sessions were submitted
- `NoSessionsApproved` - None of the submitted sessions were approved
- `SpeakerDoesNotMeetStandards` - Speaker validation failed

## ?? Troubleshooting

### Common Issues

1. **Database Connection Issues**
   - Verify SQL Server is running
   - Check connection string in appsettings.json
   - Ensure the database exists (run migrations)

2. **Migration Issues**
   ```bash
   dotnet ef migrations add InitialCreate --project Infrastructure --startup-project Talks.API
   dotnet ef database update --project Infrastructure --startup-project Talks.API
   ```

3. **Build Errors**
   - Ensure .NET 8 SDK is installed
   - Run `dotnet restore` to restore packages

4. **Speaker Registration Failures**
   - Check that all required fields are provided
   - Verify email domain is not on the legacy list
   - Ensure browser compatibility requirements are met

## ?? Example Usage

### Quick Test with Sample Data

1. Start the application
2. Navigate to `http://localhost:5121/speaker/Run`
3. This endpoint creates a sample speaker with the following data:
   - Name: Helen Jones
   - Email: helene.jones@Email.com
   - Employer: Google
   - Experience: 11 years
   - Sessions: ASP.NET, Commodore, VBScript

### Manual Testing with Postman/curl

```bash
curl -X POST "http://localhost:5121/speaker/RegisterSpeaker" \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "Jane",
    "lastName": "Developer",
    "email": "jane@gmail.com",
    "employer": "Microsoft",
    "yearsOfExperience": 5,
    "isBlog": true,
    "certifications": ["Azure", "C#"],
    "webBrowser": {
      "name": "Chrome",
      "majorVersion": 120
    },
    "sessions": [
      {
        "title": "Modern Web Development",
        "description": "Building apps with .NET 8"
      }
    ]
  }'
```

## ?? Development Notes

- **Author:** MD (Martin D)
- **Created:** 19/08/2025
- **Architecture:** Clean Architecture with CQRS
- **Testing:** Unit tests with XUnit/Moq (integration tests with in-memory database were considered but not implemented due to persistence requirements)
- **Note:** The `/speaker/Run` endpoint provides a "crude approach" for quick testing as noted in the code

## ?? Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## ?? License

This project is for demonstration purposes. Please add appropriate licensing as needed.

---

For questions or issues, please open an issue on the GitHub repository.

## ?? Key Features Demonstrated

- **Clean Architecture** implementation
- **CQRS pattern** with MediatR
- **Complex business logic** with multiple validation layers
- **Entity Framework Core** with migrations
- **Comprehensive unit testing** with high coverage
- **Dependency injection** throughout all layers
- **RESTful API design** with proper HTTP responses