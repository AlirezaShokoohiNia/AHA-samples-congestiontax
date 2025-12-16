# API Architecture – Congestion Tax

## Purpose
The API layer exposes the application’s functionality through HTTP endpoints.  
It translates external requests into commands and queries, delegates execution to the Application layer, and maps results back into HTTP responses.  
This layer enforces clear boundaries between transport concerns (routing, serialization) and business orchestration.

---

## Structure

- **Endpoints**  
  Minimal API endpoints grouped by feature.  
  - **PassEndpoints**: `/passes` for registering vehicle passes.  
  - **RuleEndpoints**: `/rules/{city}` for retrieving congestion tax rules.  
  - **TaxEndpoints**: `/vehicles/{id}/tax-records` and `/vehicles/{id}/weekly-tax` for retrieving tax records.  
  - **VehicleEndpoints**: `/vehicles` and `/vehicles/{plate}` for registering and retrieving vehicles.  
  - Endpoints are thin: they construct commands/queries and delegate to handlers.

- **Dependency Injection**  
  Endpoints rely on DI to resolve handlers (`ICommandHandler`, `IQueryHandler`).  
  - Ensures testability and separation of concerns.  
  - Infrastructure services (DbContexts, providers) are registered at startup.

- **Result Mapping**  
  Endpoints translate `CommandResult` / `QueryResult` into HTTP responses.  
  - Success → `200 OK` with value.  
  - Failure → `404 Not Found`, `400 Bad Request`, or `Problem` depending on error type.  

- **Configuration**  
  - Uses `appsettings.json` for runtime values (e.g., RuleSet base path).  
  - In tests, configuration is overridden via `EndpointTestWebApplicationFactory`.

---

## CQRS API

- **Write Side (Commands)**  
  - Endpoints accept DTOs (`RegisterPassCommand`, `RegisterVehicleCommand`).  
  - Handlers enforce domain rules and persist state.  
  - Example: `/passes` → `RegisterPassCommandHandler`.

- **Read Side (Queries)**  
  - Endpoints accept query parameters and delegate to query handlers.  
  - Providers fetch data from heterogeneous sources (database, JSON).  
  - Example: `/rules/{city}` → `GetRuleSetQueryHandler`.

---

## Testing

- **Api.Endpoints.Tests** mirrors the API structure.  
  - **PassEndpointTests**: validate `/passes` happy path and error path.  
  - **RuleEndpointTests**: validate `/rules/{city}` success and not‑found scenarios.  
  - **TaxEndpointTests**: validate `/vehicles/{id}/tax-records` and `/vehicles/{id}/weekly-tax`.  
  - **VehicleEndpointTests**: validate `/vehicles` and `/vehicles/{plate}`.  
- Tests use `EndpointTestWebApplicationFactory` to isolate infrastructure:  
  - InMemory EF Core contexts for write/read sides.  
  - InMemory configuration for RuleSet base path.  
- Ensures endpoints correctly translate results into HTTP responses.

---

## Summary
The API layer is the transport boundary:  
- Endpoints expose commands and queries via HTTP.  
- Dependency injection resolves handlers and providers.  
- Results are mapped into consistent HTTP responses.  
- Configuration and test factories ensure predictable behavior across environments.

---

## Architectural Notes
- Endpoints remain thin: delegate orchestration to Application, persistence to Infrastructure.  
- CQRS separation is preserved: commands for write, queries for read.  
- Result pattern ensures clean HTTP mapping.  
- Test factory isolates infrastructure with InMemory providers, enabling reliable integration tests.  
- API layer does not contain business logic; it is purely a transport adapter.  
- Endpoint grouping by feature improves discoverability and aligns with vertical slice architecture.  

---
