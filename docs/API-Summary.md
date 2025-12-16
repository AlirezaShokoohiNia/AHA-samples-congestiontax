# API Summary – Congestion Tax

## Purpose
This document provides a quick reference to the available HTTP endpoints, their usage, and example request/response payloads.  
It complements [Architecture-API.md](./Architecture-API.md) by focusing on external contracts.

---

## Endpoints

### Register a Vehicle

**POST** `/vehicles`

Register a vehicle (entry/exit) in the system.

**Request example:**  
```json
{
  "licensePlate": "ABC-123",
  "VehicleType": "Car"
}
```

**Response:**  
Vehicle Id

--- 

### Get Veihcle
**GET** `/vehicles/{plate}`

**Response:**  
Vehicle Id

--- 

### Record a Vehicle Pass
**POST** `/passes`

**Request example:**  
```json
{
  "vehicleId": 1,
  "licensePlate": "ABC-123",
  "timestamp": "2025-01-01T07:45:00",
  "city": "Gothenburg"
}
```

**Response:**  
Fee amount

--- 

### Get Rule Set
**GET** `//rules/{city}`

Retrieves congestion tax rules for a given city.

**Success response example:**  

```json
{
  "city": "Gothenburg",
  "dailyCap": 60,
  "timeSlots": [
    { "from": "06:00", "to": "06:29", "fee": 8 },
    { "from": "06:30", "to": "06:59", "fee": 13 }
  ],
  "holidays": [ "2025-01-01", "2025-12-25" ]
}
```

---

### Get Daily Tax Records
**GET** `/vehicles/{id}/tax-records?from=yyyy-MM-dd&to=yyyy-MM-dd`

Retrieves daily tax records for a vehicle within a date range.

**Success response example:**  

```json
[
  {
    "date": "2025-01-01",
    "licensePlate": "ABC-123",
    "totalFee": 21
  },
  {
    "date": "2025-01-02",
    "licensePlate": "ABC-123",
    "totalFee": 15
  }
]
```

---

### Get Weekly Total Tax
**GET** `/vehicles/{id}/weekly-tax`

Retrieves the total tax for a vehicle over the past 7 days.

**Success response example:**

```json
{
  "licensePlate": "ABC-123",
  "totalTax": 120
}
```
---

## Notes
**Status codes:** Success returns 200 OK. Failures return appropriate codes (404 Not Found, 400 Bad Request, ...) with error messages.

**Date format:** Dates must be provided in ISO‑8601 format (yyyy-MM-dd).

**Contracts**: Responses use DTOs defined in the Application layer (RuleSetDto, VehicleDailyTaxDto, VehicleTotalTaxDto).

**Testing**: Integration tests validate both happy path and error path scenarios using EndpointTestWebApplicationFactory.
