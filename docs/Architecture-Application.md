# Application Architecture – Congestion Tax

## Purpose
The Application layer coordinates domain logic through commands and queries.  
It defines abstractions, orchestrates workflows, and bridges DTOs with domain value objects.  
This layer enforces CQRS separation and ensures testable, maintainable orchestration.

---

## Structure

- **Abstractions**  
  Contracts for commands, queries, and providers.  
  - **Command**: `ICommand`, `ICommandHandler`, `CommandResult` define the write side contract.  
  - **Query**: `IQuery`, `IQueryHandler`, `QueryResult` define the read side contract.  
  - **Providers**: Interfaces (`IRuleSetReadProvider`, `IVehicleReadProvider`, `IVehicleTaxReadProvider`) abstract read sources.

- **Commands**  
  Application‑level write operations.  
  - Example: `RegisterPassCommand`, `RegisterVehicleCommand`.  
  - Handlers persist changes via repositories and may consult read providers for rule validation.

- **Queries**  
  Application‑level read operations.  
  - Example: `GetRuleSetQuery`, `GetVehicleDailyTaxRecordsQuery`, `GetVehicleWeeklyTotalTaxQuery`.  
  - Handlers fetch data from read providers, supporting multiple sources (database + JSON RuleSets).

- **DTOs**  
  Data transfer objects for external contracts.  
  - Represent rules, vehicles, and tax calculations (`RuleSetDto`, `VehicleDto`, `VehicleDailyTaxDto`, etc.).  
  - Used for serialization and mapping between infrastructure and domain.

- **Mappers**  
  Convert DTOs into domain value objects.  
  - Example: `HolidayRuleDtoToDateOnlyMapper`, `TimeSlotRuleDtoToTimeSlotMapper`.  
  - Ensure correctness and isolation between transport and domain models.

- **Exceptions**  
  Application‑specific error markers.  
  - Provide clarity for invalid operations or rule violations.  
  - Currently placeholder (`.gitkeep`) for future expansion.

---

## CQRS Application

- **Write Side**  
  - Commands directly depend on the Domain layer.  
  - Handlers enforce business rules and persist state.  
  - Example: `RegisterPassCommandHandler` writes via repository and validates against rule sets.

- **Read Side**  
  - Queries abstract data retrieval.  
  - Providers support heterogeneous sources:  
    - Database (aligned with write side).  
    - JSON file (RuleSets).  
  - Enables flexible, isolated read models without coupling to domain internals.

---

## Testing

- **Application.Tests** mirrors the Application structure.  
  - **Commands**: Validate command handlers (`RegisterPassCommandHandlerTests`).  
  - **Queries**: Validate query handlers (`GetVehicleQueryHandlerTests`, etc.).  
  - **Mappers**: Ensure DTO‑to‑domain conversions are correct.  
- Tests enforce correctness, isolation, and maintainability across the Application layer.

---

## Summary
The Application layer is the orchestration hub:  
- Commands enforce domain rules and persist state.  
- Queries provide flexible read models via providers.  
- DTOs and Mappers bridge external contracts with domain value objects.  
- Abstractions define clear boundaries for extensibility and testability.  

---

## Architectural Notes

- Application layer orchestrates domain logic via commands and queries.  
- CQRS separation is enforced: write side depends on Domain, read side uses providers.  
- Read side supports multiple sources (database + JSON RuleSets) without coupling to domain internals.  
- DTOs and mappers isolate transport contracts from domain value objects.  
- Abstractions define clear boundaries for extensibility and testability.  
- Handlers remain thin: delegate business rules to Domain, delegate persistence to Infrastructure.  

---