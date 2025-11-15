# License Management System

## Overview
A microservices-based .NET solution for managing licenses, documents, notifications, payments, and users. The Portal is an MVC/Razor application that acts as a UI and calls the microservices.

## Projects and Target Frameworks
- APIGateway (Ocelot) — APIGateway/APIGateway.csproj (recommended: .NET 8)
- UserService — UserService/UserService/UserService.csproj (net8.0)
- Portal — Portal/Portal.csproj (net6.0)
- LicenseService — LicenseService/LicenseService.csproj (net6.0)
- DocumentService — DocumentService/DocumentService.csproj (net6.0)
- NotificationService — NotificationService/NotificationService.csproj (net6.0)
- PaymentService — PaymentService/PaymentService.csproj (net6.0)

Note: Some projects target .NET 8 while others target .NET 6. Use corresponding SDKs installed on your machine.

## Prerequisites
- .NET 6 SDK and/or .NET 8 SDK (depending on the project)
- SQL Server (or an accessible SQL endpoint)
- dotnet-ef tool (for running EF Core migrations): `dotnet tool install --global dotnet-ef` (if not already installed)

## Running the solution (recommended steps)
1. Restore and build the solution in your IDE or via CLI:
   - dotnet restore
   - dotnet build

2. Configure connection strings and settings
   - Each microservice uses its own connection string in appsettings.json. Update the connection strings for `UserService`, `LicenseService`, `DocumentService`, `NotificationService`, and `PaymentService`.
   - Portal uses cookie + JWT for authentication. Update JWT keys/issuer in Portal's configuration if required.

3. Apply EF Core migrations for services that use a database
   - Example (from solution root):
     dotnet ef database update --project DocumentService/DocumentService.csproj --startup-project DocumentService/DocumentService.csproj
     dotnet ef database update --project LicenseService/LicenseService.csproj --startup-project LicenseService/LicenseService.csproj
     dotnet ef database update --project NotificationService/NotificationService.csproj --startup-project NotificationService/NotificationService.csproj
     dotnet ef database update --project PaymentService/PaymentService.csproj --startup-project PaymentService/PaymentService.csproj
     dotnet ef database update --project UserService/UserService/UserService.csproj --startup-project UserService/UserService/UserService.csproj

4. Run each service
   - Start microservices individually (example):
     dotnet run --project DocumentService/DocumentService.csproj
     dotnet run --project LicenseService/LicenseService.csproj
     dotnet run --project NotificationService/NotificationService.csproj
     dotnet run --project PaymentService/PaymentService.csproj
     dotnet run --project UserService/UserService/UserService.csproj

   - Start the API Gateway (if used):
     dotnet run --project APIGateway/APIGateway.csproj

   - Start the Portal:
     dotnet run --project Portal/Portal.csproj

5. Service base URLs and ports
   - By default, ASP.NET apps choose a port at runtime or use the launchSettings.json configuration. Controllers in the Portal currently reference services using a hard-coded base URI (for example: `http://localhost:5155`) — update these URIs in the Portal controllers or configure the microservices to run on those ports.
   - To set a fixed port, either modify each project's launchSettings.json or run with: `dotnet run --urls "http://localhost:5155" --project LicenseService/LicenseService.csproj`

6. API Gateway (Ocelot)
   - If you use the APIGateway project, ensure `ocelot.json` is present at the gateway project root and contains routes that match the microservice endpoints.
   - Run the gateway after microservices are up so it can route requests correctly.

## Notes and Design Rationale
- Microservices allow independent development, deployment and scaling per bounded context (Users, Licenses, Documents, Notifications, Payments).
- Portal is a server-rendered MVC/Razor application and uses a mix of cookie authentication (for UI) and JWT (for API calls).
- Use EF Core migrations to keep service databases in sync with models.
- Ocelot gateway centralizes routing and can be configured with caching, authentication and rate-limiting.

## Troubleshooting
- If Portal controllers fail calling a service, check the base URI in the controller and confirm the target service is running and reachable on that port.
- For EF migrations, ensure the connection string is valid and the target SQL Server is accessible.

## Future improvements
- Unify target framework versions where feasible (upgrade net6 projects to net8 if compatible).
- Add Docker Compose for local multi-container orchestration.
- Add automated tests and CI/CD pipelines.

## Contributing
- Fork, create feature branches and open pull requests.

---
This README was updated to reflect the actual projects, target frameworks, and runtime considerations in the solution.
