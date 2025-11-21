# Testing Strategy

This project follows **Test-Driven Development (TDD)** principles:

- **Domain Tests:**  
  - Aggregates, policies, daily aggregation, exemptions  
  - Created *before* implementing domain logic to define expected behavior

- **Application Tests:**  
  - Commands & Queries  
  - Handler orchestration  
  - Test first, then implement

- **Infrastructure Tests:**  
  - Repository interactions (mocked or in-memory)  
  - Validation of persistence adapters

- **API / Integration Tests:**  
  - Endpoint validation  
  - Request/response correctness

**Benefits of TDD in this project:**  
- Encourages well-designed, decoupled domain  
- Catches regressions early  
- Improves confidence in refactoring  
- Provides executable documentation of business rules
