# API Summary

## Record a Vehicle Pass
POST /passes

## Get Tax for Vehicle/Date
GET /tax/{vehicleId}?date=yyyy-MM-dd

Request/Response examples...
Request:
```json
{
  "vehicleId": "ABC-123",
  "timestamp": "2025-01-01T07:45:00",
  "city": "Gothenburg"
}
```

Response:
```json
{
  "vehicleId": "ABC-123",
  "date": "2025-01-01",
  "taxAmount": 21
}
```
