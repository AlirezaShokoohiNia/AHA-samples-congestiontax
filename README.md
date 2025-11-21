# AHA Congestion Tax – Sample Architecture  
A clean, extensible, domain-driven implementation of the *Congestion Tax* problem, demonstrating production-grade architectural practices in a compact and reviewable codebase.

---

## 📘 Overview  
This repository contains a structured, testable, and scalable implementation of a congestion tax calculation service.  
The goal is clarity, correctness, and maintainability.

---

## 🟦 WHAT – The Business Requirement  

Brief summary: record vehicle passes, calculate fees, enforce exemptions, daily caps.  

For detailed business rules, see [Domain Model](./docs/Domain-Model.md).

---

## 🟩 WHY – Architectural Reasoning

Designed to showcase: clean architecture, DDD, CQRS, testability, and scalability.

**Test-Driven Development (TDD):**  
- Domain logic is first specified through unit tests, ensuring correctness before implementation.  
- Application services, command/query handlers, and infrastructure adapters are validated via tests first.  
- This approach enforces clear boundaries, maintainable design, and early bug detection.

Full architecture details are in [Architecture](./docs/Architecture.md).

---

## 🟥 HOW – Solution Approach  
The project is structured using:

- Layers: Domain, Application, Infrastructure, API  
- Commands & Queries for orchestration  
- Automated tests (Domain, Application, API)  

For detailed architecture and patterns see [Architecture](./docs/Architecture.md).  
For API endpoints see [API Summary](./docs/API-Summary.md).  
For testing approach see [Testing Strategy](./docs/Testing-Strategy.md).

---

## 📂 Folder Structure  
Aligned with your choices:

```
src/
 ├── AHA.Samples.CongestionTax.sln
 ├── AHA.CongestionTax.Api/
 ├── AHA.CongestionTax.Application/
 ├── AHA.CongestionTax.Domain/
 ├── AHA.CongestionTax.Infrastructure/
 ├── AHA.CongestionTax.Contracts/        # Request/response DTOs
 ├── AHA.CongestionTax.Seedwork/         # Shared abstractions
 └── Tests/
     ├── AHA.CongestionTax.Api.Tests/
     ├── AHA.CongestionTax.Application.Tests/
     ├── AHA.CongestionTax.Domain.Tests/
     └── AHA.CongestionTax.Infrastructure.Tests/
```

Namespaces follow:  
`AHA.CongestionTax.*`  
and tests:  
`AHA.CongestionTax.*.Tests`

---

## 🚀 Getting Started

### **Requirements**
- .NET 9 SDK  
- Git  
- Optional: Docker (if API containerization is added)

### **Build**
```bash
dotnet build src/AHA.Samples.CongestionTax.sln
```

### **Run API**
```bash
cd src/AHA.CongestionTax.Api
dotnet run
```

## 📘 Readers Guide  
Understanding the Journey from **WHAT → WHY → HOW**

### WHAT  
A congestion tax system that records passes and calculates fees.

### WHY  
To demonstrate engineering maturity, architecture, testability, and readability.

### HOW  
Using Clean Architecture, DDD, CQRS, layered boundaries, domain policies, application pipelines, and a structure that scales.

---

## 📝 Notes  
These samples are built for **learning and demonstration** purposes only.  
They are **standalone**, not intended for production integration.  
**"AHA"** is my personal software brand used across all public repositories.