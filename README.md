# RegiFlow Portal POC

This repository contains a proof‑of‑concept (POC) implementation of a multi‑tenant license/registry management system.  It is intended for the developer **Vinay** and demonstrates a modern design using **minimal APIs** and a **teal**‑themed front‑end.  The project is broken into several microservices along with a lightweight MVC portal.

## Projects

* **Portal** – An ASP.NET Core MVC application that acts as a portal for applicants and agency staff.  The layout uses a teal/dark navy color palette defined in the shared layout file.
* **Licensing.Api** – Provides endpoints for enrollment management.  Endpoints follow the pattern `/api/registry/v1/enrollments/...` and are defined using ASP.NET Core minimal APIs for a vertical slice architecture.
* **Billing.Api** – Handles charges and invoices for license fees.  Routes live under `/api/billing/v1/...`.
* **Files.Api** – Manages file uploads and downloads.  Routes live under `/api/files/v1/...`.
* **Messaging.Api** – Sends notifications to users.  Routes live under `/api/messaging/v1/...`.

## Running the POC

1. Install [.NET 6 SDK](https://dotnet.microsoft.com/download).
2. Restore dependencies: `dotnet restore` from the repository root.
3. Start each service individually, for example:
   ```bash
   cd Licensing.Api
   dotnet run
   ```
4. Start the portal with `cd Portal && dotnet run`.

## Architecture Notes

This POC illustrates a minimal API style for microservices.  Each feature lives in its own folder with a handler method registered in `Program.cs`.  Multi‑tenancy is achieved via a placeholder `ITenantAccessor` that would resolve the current tenant from a claim or subdomain.  Commands and queries are separated by design but consolidated in the same file for simplicity.  You are encouraged to flesh out the domain, persistence, and background processing according to your requirements.
