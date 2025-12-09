# Architecture – Rule Sets

## Purpose
Rule sets define congestion-tax policies per city.  
They are externalized into JSON files to allow multi-city support, easy review, and future extensibility.

---

## Rule Sets Overview
- Each city stores its rules in `/rules/{city}.rules.json`.  
- Examples: `gothenburg.rules.json`, `stockholm.rules.json`.  
- Rule sets contain:  
  - Time-based fee intervals  
  - Toll-free vehicle categories  
  - Local holiday exemptions  

This data is loaded by the Application layer via `IRuleSetReadProvider`.
---

## Why Rules Are Externalized
- Easy for assessors and reviewers to verify correctness.  
- Allows multi-city support without redeployment.  
- Supports future dynamic rule sources (Redis, DB, API).  
- Enables clean separation between policy (rules) and logic (domain).  
---

## JSON Format

### Root Structure

```json
{ 
    "City": "string", 
    "TimeSlots": [ TimeSlotRule ], 
    "Holidays": [ HolidayRule ], 
    "TollFreeVehicles": [ TollFreeVehicleRule ] 
}
```

### TimeSlot Object

```json
{ 
    "StartHour": number, // 0–23 
    "StartMinute": number, // 0–59 
    "EndHour": number, // 0–23 
    "EndMinute": number, // 0–59 
    "Amount": number // Toll amount in SEK 
}
```

### HolidayRule Object
```json
{ 
    "Date": "yyyy-MM-dd", // ISO-8601 date 
    "AppliesToDayBefore": bool // true if the day before is toll-free 
}
```


### TollFreeVehicleRule Object
```json
{ 
    "VehicleType": "string" // e.g. "Emergency", "Motorcycle", "Diplomat" 
}
```

### Example Complete JSON (Gothenburg)

```json
{ 
  "City": "Gothenburg", 
  "TimeSlots": [
     { "StartHour": 6, "StartMinute": 0, "EndHour": 6, "EndMinute": 29, "Amount": 8 }, 
     { "StartHour": 6, "StartMinute": 30, "EndHour": 6, "EndMinute": 59, "Amount": 13 } 
  ], 
  "Holidays": [ 
    { "Date": "2023-01-01", "AppliesToDayBefore": false }, 
    { "Date": "2023-12-25", "AppliesToDayBefore": true } 
  ], 
  "TollFreeVehicles": [ 
    { "VehicleType": "Motorcycle" }, 
    { "VehicleType": "Emergency" }, 
    { "VehicleType": "Diplomat" } 
  ] 
}
```

---

## RuleSetReadProvider Usage

### Registering RuleSetReadFileProvider in DI
```csharp
var rulesPath = Path.Combine(AppContext.BaseDirectory, "rules");

services.AddSingleton<IRuleSetReadProvider>(
    new RuleSetReadFileProvider(rulesPath)
);

```

### Using Provider in a Handler (simplified)
```csharp
public class CalculateTollHandler
{
    private readonly IRuleSetReadProvider _rules;

    public CalculateTollHandler(IRuleSetReadProvider rules)
    {
        _rules = rules;
    }

    public async Task<int> HandleAsync(string city)
    {
        var rulesResult = await _rules.GetRulesForCityAsync(city);
        if (!rulesResult.IsSuccess || rulesResult.Value is null)
            throw new InvalidOperationException($"Ruleset not found for {city}");

        var rules = rulesResult.Value;
        // Pass rules to domain service...
        return 0;
    }
}

```

---
## Architectural Notes
- The Application layer depends only on `IRuleSetReadProvider`.  
- Infrastructure provides `RuleSetReadFileProvider`, which deserializes JSON into `RuleSetReadModel`.  
- Read models (`TimeSlotRuleReadModel`, `HolidayRuleReadModel`, `TollFreeVehicleRuleReadModel`) are mapped into DTOs via `RuleSetReadModelToRuleSetDto`.  
- This design allows swapping the provider implementation (e.g., Redis, DB, API) without changing Application contracts.  

