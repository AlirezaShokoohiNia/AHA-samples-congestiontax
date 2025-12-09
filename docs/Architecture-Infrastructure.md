# Infrastructure Architecture – Congestion Tax

## Purpose
The Infrastructure layer provides persistence and data access implementations.  
It supports CQRS by separating write‑side repositories from read‑side providers.  
This layer bridges the Application abstractions with actual storage (SQL database + JSON files).

---

## Structure

- **Data**  
  Implements the CQRS write side via repository pattern.  
  - **AppDbContext**: EF Core context implementing `IUnitOfWork`.  
  - **Repositories**: Concrete persistence for aggregates (`DayTollRepository`, `VehicleRepository`).  
  - **Configurations**: Entity type configurations (`DayTollConfiguration`, `VehicleConfiguration`, etc.).  
  - **BaseRepository**: Shared repository logic for consistency.

- **Query**  
  Implements the CQRS read side.  
  - **Source1 (Database)**:  
    - `QueryDbContext` with EF Core read models (`DayTollReadModel`, `VehicleReadModel`, `VehicleTypeReadModel`).  
    - Configurations for read models.  
    - Acts as the primary read source aligned with write side.  
  - **Source2 (JSON File)**:  
    - Read models for rule sets (`RuleSetReadModel`, `HolidayRuleReadModel`, `TimeSlotRuleReadModel`, `VehicleFreeRuleReadModel`).  
    - Provides external rule definitions independent of database.  
  - **Providers**: Concrete implementations (`RuleSetReadFileProvider`, `VehicleReadProvider`, `VehicleTaxReadProvider`) that expose read models to Application layer.  
  - **Mappers**: Convert read models into DTOs (`VehicleReadModelToVehicleDtoMapper`, `RuleSetReadModelToRuleSetDto`, etc.).

---

## CQRS Application

- **Write Side**  
  - Uses `AppDbContext` and repositories to persist aggregates and entities.  
  - Repository pattern enforces consistency and testability.  
  - Configurations ensure correct schema mapping.

- **Read Side**  
  - Source1: Database read models for queries aligned with persisted state.  
  - Source2: JSON file read models for rule sets and exemptions.  
  - Providers abstract both sources, enabling flexible query handling in Application layer.  
  - Mappers isolate DTO contracts from raw read models.

---

## Testing

- **Infrastructure.Tests** mirrors Infrastructure structure.  
  - **Data**: Validate repositories and `AppDbContext` (`DayTollRepositoryTests`, `VehicleRepositoryTests`).  
  - **Query.Source1**: In‑memory SQLite contexts (`SqliteInMemoryQueryDbContextFactory`, `TestQueryDbContext`) for isolated testing.  
  - **Query.Providers**: Validate file and database providers (`RuleSetReadFileProviderTests`, `VehicleReadProviderTests`).  
  - **Query.Mappers**: Ensure correctness of read model to DTO conversions.

---

## Summary
The Infrastructure layer is the persistence and data access backbone:  
- Data folder implements CQRS write side with repositories and `AppDbContext`.  
- Query folder implements CQRS read side with dual sources (database + JSON).  
- Providers and mappers expose clean DTOs to Application layer.  
- Testing ensures correctness of persistence, providers, and mappings.

## Architectural Notes
- Infrastructure layer depends on EF Core and file I/O, but isolates these behind repositories and providers.  
- Write side enforces repository + unit of work pattern for consistency.  
- Read side supports heterogeneous sources (SQL + JSON) without leaking implementation details.  
- Mappers ensure DTO isolation from persistence models.  
- Tests use in‑memory contexts and file providers to validate correctness without external dependencies.  
