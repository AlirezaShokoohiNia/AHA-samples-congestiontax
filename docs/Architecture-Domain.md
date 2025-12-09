# Domain Architecture – Congestion Tax

## Purpose
The Domain Layer encapsulates the business rules of the congestion tax system.
It contains aggregates, entities, value objects, and domain services responsible
for enforcing policies such as the 60-minute rule, holiday exemptions, and
vehicle exemptions.

---

## Aggregates

### **DayToll**
Represents the toll history of a single vehicle on a specific date in a
specific city.

- Vehicle
- City
- Date
- TotalFee
- Passes (collection)
- Behavior:
  - AddPass(time)
  - ApplyCalculatedFee(dailyFee)

### **Vehicle**
A lightweight entity used internally for type classification and
license-plate identity.

---

## Entities

### **PassRecord**
Represents a single timestamped toll passage.

- Time (TimeOnly)

---

## Value Objects

### **TimeSlot**
Represents a rule-based fee interval:
- Start
- End
- Fee

### **DailyTaxResult**
Returned by domain service (fee + per-pass breakdown).

---

## Domain Services

### **CongestionTaxCalculator**
Pure domain logic:
- Determines per-pass fee from time slots
- Applies toll-free vehicle rules
- Applies weekend / holiday rules
- Enforces 60-minute single-charge rule
- Enforces daily maximum fee cap (default 60 SEK)
- Produces `DailyTaxResult`

This service does not depend on infrastructure and is deterministic.

---

## Domain Policies

### **1. Single Charge Rule**
If multiple passes occur within 60 minutes, only the **highest** fee in that
window is charged.

### **2. Toll Free Vehicles**
Emergency, Diplomat, Military, Foreign, etc.

### **3. Holidays / Weekends**
All tolls waived.

### **4. Daily Max Fee**
Total cannot exceed 60 SEK.

---

## Exceptions

### **DomainException**
Thrown when an invariant is broken (negative fee, invalid state).

---

## Architectural Notes

- Domain layer is pure C#, no external dependencies.
- All rules come from *Application Layer* (IRuleSetReadeProvider).
- Domain service is stateless and reusable.
- Aggregate remains small and focused (only state + behavior).

---
