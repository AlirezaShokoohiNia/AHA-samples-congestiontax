# Rules JSON Format

This document defines the JSON structure used to describe congestion-tax rules per city.
Each rule set is stored in a file named:

{city}.rules.json

Example: gothenburg.rules.json

## Root Structure

```json
{
  "City": "string",
  "TimeSlots": [ TimeSlot ],
  "Holidays": [ "yyyy-MM-dd" ],
  "TollFreeVehicles": [ "string" ]
}
```
## TimeSlot Object

```json
{
  "StartMinutes": number,
  "EndMinutes": number,
  "Amount": number
}
```
- All times are represented as minutes from midnight (0–1439)
- Amount is an integer tax value (SEK)

## Holidays

```json
[
  "2023-01-01",
  "2023-12-25"
]
```
- ISO-8601 yyyy-MM-dd
- Domain layer converts these into rich domain types

## TollFreeVehicles

Example:
```json
[
  "Motorcycle",
  "Diplomat",
  "Emergency"
]
```
Represents vehicle categories that should not be charged.

## Example Complete JSON (Gothenburg)

```json
{
  "City": "Gothenburg",
  "TimeSlots": [
    { "StartMinutes": 360, "EndMinutes": 389, "Amount": 8 },
    { "StartMinutes": 390, "EndMinutes": 419, "Amount": 13 },
    { "StartMinutes": 420, "EndMinutes": 479, "Amount": 18 },
    { "StartMinutes": 480, "EndMinutes": 509, "Amount": 13 },
    { "StartMinutes": 510, "EndMinutes": 899, "Amount": 8 },
    { "StartMinutes": 900, "EndMinutes": 929, "Amount": 13 },
    { "StartMinutes": 930, "EndMinutes": 1019, "Amount": 18 },
    { "StartMinutes": 1020, "EndMinutes": 1079, "Amount": 13 },
    { "StartMinutes": 1080, "EndMinutes": 1109, "Amount": 8 },
    { "StartMinutes": 1110, "EndMinutes": 1439, "Amount": 0 }
  ],
  "Holidays": [
    "2023-01-01",
    "2023-12-25",
    "2023-12-26"
  ],
  "TollFreeVehicles": [
    "Motorcycle",
    "Tractor",
    "Emergency",
    "Diplomat",
    "Military",
    "Foreign"
  ]
}
```
## Purpose of JSON Format

- Easy for assessment review
- Human-editable
- Independent of infrastructure
- Ready for TDD and system evolution
