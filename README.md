# AHA Congestion Tax – Sample Architecture  
A clean, extensible, domain-driven implementation of the *Congestion Tax* problem, demonstrating production-grade architectural practices in a compact and reviewable codebase.

---

## 📘 Overview  
This repository contains a structured, testable, and scalable implementation of a congestion tax calculation service.  
While the domain is simple, the architecture reflects the same principles used in real enterprise systems:

- Domain-Driven Design (DDD)  
- Clean Architecture  
- CQRS (Command–Query Responsibility Segregation)  
- Vertical slicing  
- Automated testing strategy  
- Clear, reviewer-friendly structure  

The goal is not minimalism — it is clarity, correctness, and long-term maintainability.

---

## 🟦 WHAT – The Business Requirement  
The system must model a city congestion tax mechanism that includes:

- Recording vehicle **passes** through toll checkpoints  
- Applying **time-based fee rules**  
- Enforcing domain constraints (free vehicles, time windows, max daily caps)  
- Aggregating passes for a day and calculating the final charge  
- Exposing API endpoints to query reports and submit passes  

This reflects typical municipal congestion pricing scenarios.

---

## 🟩 WHY – Architectural Reasoning  
Although the assignment is small, real companies evaluate:

✔ Engineering maturity  
✔ Architectural clarity  
✔ Domain modeling skill  
✔ Scalability and future-readiness  
✔ Clean coding style  
✔ Testability  

Therefore, the solution uses an architecture that:

- Prevents business rules from leaking into infrastructure  
- Supports evolving policies (new tax rules, exemptions, zones, etc.)  
- Allows unit tests to target the **pure domain model**  
- Keeps dependencies one-directional  
- Makes the solution readable and enjoyable to review  

It is intentionally crafted as a **portfolio-quality demonstration**.

---

## 🟥 HOW – Solution Approach  
The project is structured using:

### **Domain Layer**  
Pure domain logic  
- Entities (`TaxDay`, `VehiclePass`)  
- Value Objects (`TaxRate`, `TimeSlot`)  
- Policies (time-based fee rules, exemptions)  
- Services (daily calculation orchestration)

### **Application Layer**  
Use-cases and orchestration  
- Commands & Queries  
- Command Handlers & Query Handlers  
- DTOs  
- Interfaces for persistence / read models

### **Infrastructure Layer**  
Implementation details  
- EF Core database access (if included)  
- Repositories  
- Background services (optional)

### **API Layer**  
User-facing interface  
- Minimal APIs or Controllers  
- Request/Response models  
- Endpoint routing  

### **Tests**  
- Domain tests (highest value)  
- Application tests  
- API/Integration tests (optional)

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
- .NET 8 SDK  
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

---

## 📡 API Summary (short version)
### Record a vehicle pass  
`POST /passes`

Request:
```json
{
  "vehicleId": "ABC-123",
  "timestamp": "2025-01-01T07:45:00",
  "city": "Gothenburg"
}
```

### Get tax for a vehicle/date  
`GET /tax/{vehicleId}?date=2025-01-01`

Response:
```json
{
  "vehicleId": "ABC-123",
  "date": "2025-01-01",
  "taxAmount": 21
}
```

---

## 🧪 Testing  
Each layer is tested independently:

### **Domain Tests**  
- All rule calculations  
- Daily aggregation  
- Exemption policies  
- Time window logic  

### **Application Tests**  
- Commands & queries  
- Handler orchestration  
- Repository interactions (mocked)

### **API Tests**  
- Endpoint correctness  
- Request validation  

---

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