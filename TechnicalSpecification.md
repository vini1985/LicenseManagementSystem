# Technical Specification Document

## Project Overview
The License Management System is a microservices-based application designed to manage licenses, documents, notifications, payments, and user authentication. It includes a Razor Pages-based Portal for user interaction and an API Gateway for routing requests to microservices.

---

## Architecture
### High-Level Design
- **Microservices Architecture**: Each service is responsible for a specific domain (e.g., Users, Licenses, Documents, Notifications, Payments).
- **API Gateway**: Ocelot is used to route requests to the appropriate microservices.
- **Portal**: Razor Pages application for user interaction.
- **Database**: Each microservice has its own database, ensuring data isolation.

### Components
1. **API Gateway**
   - Routes requests to microservices.
   - Provides caching and centralized routing.

2. **Portal**
   - Razor Pages application for user interaction.
   - Uses cookie authentication for UI and JWT for API calls.

3. **Microservices**
   - **UserService**: Manages user authentication and roles.
   - **LicenseService**: Handles license-related operations.
   - **DocumentService**: Manages document uploads and retrievals.
   - **NotificationService**: Sends notifications to users.
   - **PaymentService**: Handles payment processing.

---

## Technologies Used
### Frameworks and Libraries
- **.NET 6 / .NET 8**: Backend framework.
- **Entity Framework Core**: ORM for database operations.
- **Ocelot**: API Gateway.
- **Newtonsoft.Json**: JSON serialization/deserialization.
- **Swashbuckle.AspNetCore**: Swagger for API documentation.

### Frontend
- **Razor Pages**: Server-side rendering.
- **Bootstrap**: UI styling.

### Database
- **SQL Server**: Relational database for all microservices.

### Authentication
- **JWT**: For API authentication.
- **Cookies**: For Portal authentication.

---

## Deployment
### Prerequisites
- .NET 6 SDK and/or .NET 8 SDK.
- SQL Server.
- dotnet-ef tool for migrations.

### Steps
1. **Restore and Build**:
   ```bash
   dotnet restore
   dotnet build
   ```
2. **Apply Migrations**:
   ```bash
   dotnet ef database update --project <Microservice>
   ```
3. **Run Services**:
   ```bash
   dotnet run --project <Microservice>
   ```
4. **Run API Gateway**:
   ```bash
   dotnet run --project APIGateway
   ```
5. **Run Portal**:
   ```bash
   dotnet run --project Portal
   ```

---

## Design Rationale
### Microservices
- **Scalability**: Each service can scale independently.
- **Separation of Concerns**: Each service handles a specific domain.

### API Gateway
- **Centralized Routing**: Simplifies client interaction with multiple services.
- **Caching**: Improves performance.

### Razor Pages
- **Simplicity**: Ideal for server-side rendering.
- **Integration**: Works seamlessly with the .NET ecosystem.

### Authentication
- **JWT**: Secure API communication.
- **Cookies**: Simplifies user authentication in the Portal.

---

## Future Enhancements
- Add Docker support for containerization.
- Implement CI/CD pipelines.
- Enhance logging and monitoring.
- Upgrade all projects to .NET 8 for consistency.

---

## Conclusion
The License Management System is a robust, scalable, and modular application designed to handle complex business requirements efficiently. Its microservices architecture ensures flexibility and maintainability, while the Razor Pages Portal provides a user-friendly interface.