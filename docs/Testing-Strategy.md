# Testing Strategy

This project follows **Test-Driven Development (TDD)** principles.  
Tests are organized by architectural layer to ensure correctness and maintainability.

---

## Domain Tests
- Aggregates, policies, daily aggregation, exemptions  
- Written *before* implementing domain logic to define expected behavior  
- Focus: business rules and invariants  

## Application Tests
- Commands & Queries  
- Handler orchestration and workflow correctness  
- Ensure DTOs and mappers behave as expected  
- Focus: orchestration, not persistence  

## Infrastructure Tests
- Repository interactions (mocked or in-memory)  
- Validation of persistence adapters (`AppDbContext`, `QueryDbContext`)  
- Providers and mappers tested with deterministic data sources  
- Focus: persistence correctness and adapter reliability  

## API / Integration Tests
- Endpoint validation  
- Request/response correctness  
- End-to-end scenarios across layers  
- Use `EndpointTestWebApplicationFactory` for isolated infrastructure  
- Focus: contract validation and system integration  

---

## Benefits of TDD
- Encourages well-designed, decoupled domain  
- Catches regressions early  
- Improves confidence in refactoring  
- Provides executable documentation of business rules  

---

## Notes
- Tests mirror folder structure (`Domain.Tests`, `Application.Tests`, `Infrastructure.Tests`, `API.Tests`).  
- In-memory contexts and JSON files are used for deterministic test data.  
- Each test suite runs in isolation to avoid cross-contamination.   
- Strategy aligns with Clean Architecture: each layer tested in isolation, with integration tests for cross-layer validation.  
