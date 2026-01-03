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
    - The architecture allows replacing `RuleSetReadFileProvider` with any other implementation of `IRuleSetReadProvider` (e.g., Redis, REST API, or other technologies) without affecting Application layer contracts. [More detail ...](./Architecture-Rules.md)  
  - **TypeAdapters/Mappers**: Convert read models into DTOs (`VehicleReadModelToVehicleDtoAdapter`, `RuleSetReadModelToRuleSetDtoAdapter`, etc.).

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
  - Providers are interchangeable: `IRuleSetReadProvider` can be backed by JSON, Redis, or other technologies.  
  - TypeAdapters/Mappers isolate DTO contracts from raw read models.

---

## Migrations
- Contained in `Infrastructure.Migrations` project.
- Responsible for schema evolution and view creation.
- Tested via `Infrastructure.Migrations.Tests` to ensure schema artifacts exist as expected.
- Isolated from runtime Infrastructure code to keep migrations reproducible and auditable.

---

## Testing

- **Infrastructure.Tests** mirrors Infrastructure structure.  
  - **Data**: Validate repositories and `AppDbContext` (`DayTollRepositoryTests`, `VehicleRepositoryTests`) in isolation by using In‑memory EF CORE contexts (`AppDbContextTestFactory`).  
  - **Query.Source1**: In‑memory EF CORE contexts (`QueryDbContextTestFactory`) for isolated testing.  
  - **Query.Providers**: Validate file and database providers (`RuleSetReadFileProviderTests`, `VehicleReadProviderTests`).  
  - **Query.Mappers**: Ensure correctness of read model to DTO mapping.
  - **Query.Adapters**: Ensure correctness of read model to DTO adaption.  

---

## Summary
The Infrastructure layer is the persistence and data access backbone:  
- Data folder implements CQRS write side with repositories and `AppDbContext`.  
- Query folder implements CQRS read side with dual sources (database + JSON).  
- Providers and typeadapters/mappers expose clean DTOs to Application layer.  
- Providers are designed to be replaceable, supporting alternative technologies like Redis.  
- Testing ensures correctness of persistence, providers, adaption and mappings.

## Architectural Notes
- Infrastructure layer depends on EF Core and file I/O, but isolates these behind repositories and providers.  
- Write side enforces repository + unit of work pattern for consistency.  
- Read side supports heterogeneous sources (SQL + JSON) without leaking implementation details.  
- Providers are interchangeable: `IRuleSetReadProvider` can be swapped for other technologies (e.g., Redis) without breaking Application contracts.  
- TypeAdapters/Mappers ensure DTO isolation from persistence models.  
- Tests use in‑memory contexts and file providers to validate correctness without external dependencies.  
- TypeAdapters are designed with future alignment to `Application.Abstractions.Adapter.ITypeAdapter`, ensuring consistent adaptation strategy across layers.  
- Mappers are designed with future alignment to `Application.Abstractions.Adapter.IMapper`, ensuring consistent mapping strategy across layers.  


---
