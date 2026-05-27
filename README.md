# SpaceX API

ASP.NET Core Web API used as a backend service for the SpaceX Launches application.

## Features

- User account creation
- Authentication and authorization flow
- Login, logout and refresh token support
- Email verification
- Forgot password and reset password flow
- Change password
- Latest launch endpoint
- Paginated launch list endpoint
- Launch details endpoint
- Rocket details endpoint
- Launchpad and landpad details endpoints
- Crew member details endpoint
- Capsule details endpoint
- Ship details endpoint
- SpaceX external API integration
- Global exception handling
- Request cancellation support
- Swagger/OpenAPI documentation
- Unit and integration tests

## Tech Stack

- .NET 8
- ASP.NET Core Web API
- C#
- Swagger/OpenAPI
- HttpClient
- SQL Server
- Entity Framework Core

## Solution Structure

```
spacex-api/
 ├── Application Core/
 │    ├── Interfaces/
 │    │    ├── SpaceX.Core.Services.Interfaces/
 │    │    └── SpaceX.Infrastructure.Interfaces/
 │    ├── SpaceX.Core.Domain/
 │    └── SpaceX.Core.Services/
 │
 ├── Application Infrastructure/
 │    ├── SpaceX.Infrastructure.Database/
 │    ├── SpaceX.Infrastructure.Email/
 │    └── SpaceX.Infrastructure.ExternalApis/
 │         └── SpaceX/
 │              ├── Constants/
 │              ├── Contracts/
 │              ├── DependencyInjection/
 │              ├── Mappings/
 │              ├── Options/
 │              └── SpaceXApiClient.cs
 │
 ├── IoC Container/
 │    └── SpaceX.Ioc/
 │
 ├── Outer Layer/
 │    └── SpaceX.WebApi/
 │
 └── Tests/
      ├── SpaceX.UnitTests/
      └── SpaceX.IntegrationTests/
```

## Architecture

The API is organized using a layered architecture:

- **Outer Layer**
  - Web API controllers
  - Web API contracts
  - Request/response models
  - Swagger configuration
  - Middleware registration

- **Application Core**
  - Domain models
  - Service abstractions
  - Application services
  - Business logic

- **Application Infrastructure**
  - Database implementation
  - Data models
  - Email implementation
  - External SpaceX API integration
  - SpaceX external API contracts

- **IoC Container**
  - Dependency injection registration

- **Tests**
  - Unit tests
  - Integration tests

## Prerequisites

- .NET 8 SDK
- SQL Server

## Running the Application

```bash
dotnet restore
dotnet run
```

Application starts on:

```
https://localhost:7019
```

## Swagger

Swagger UI is available at:

```
https://localhost:7019/swagger
```

## API Endpoints

### Account

```http
POST /account
```

### Authentication

```http
POST /authentication/login
GET  /authentication/authorize
POST /authentication/refresh-token
POST /authentication/logout
POST /authentication/verify
POST /authentication/{email}/resend-verification-email
POST /authentication/{email}/forgot-password
POST /authentication/{email}/resend-forgot-password
POST /authentication/reset-password
POST /authentication/change-password
GET  /authentication/check-email/{email}
```

### Launch

```http
GET /launch/latest
GET /launch/list
GET /launch/{launchId}
GET /launch/rocket/{rocketId}
GET /launch/launchpad/{launchpadId}
GET /launch/landpad/{landpadId}
GET /launch/crew-member/{crewMemberId}
GET /launch/capsule/{capsuleId}
GET /launch/ship/{shipId}
```

## Authentication

Protected endpoints require authentication. After login, the API issues tokens used by the client application to authorize requests.

## External API Integration

The API consumes data from the public SpaceX API through the infrastructure external API layer.

External SpaceX API contracts are located inside the `SpaceX.Infrastructure.ExternalApis` project and are kept separate from Web API contracts, domain models and data models.

## Error Handling

Global exception handling is used to return consistent API responses.

## Testing

The solution contains unit and integration tests.

```bash
dotnet test
```

## Notes

- Web API contracts are separated from domain models.
- Domain models are separated from data models.
- External API contracts are encapsulated inside the external API integration layer.
- Cancellation tokens are supported across API requests.
