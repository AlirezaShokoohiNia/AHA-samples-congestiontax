# Rule Sets Overview

This project supports city-based congestion-tax rule sets.
Each city stores its rules inside a dedicated JSON file located in /rules/{city}.rules.json

Examples:
gothenburg.rules.json
stockholm.rules.json

## What Is a Rule Set?

A rule set contains all information needed to calculate a congestion tax for a city:

- Time-based fee intervals
- Toll-free vehicle categories
- Local holiday exemptions

This data is loaded by the application layer via IRuleSetReader.

## Why Rules Are Externalized

- Easy for assessors and reviewers to verify correctness
- Allows multi-city support without redeployment
- Supports future dynamic rule sources (Redis, DB, API)
- Enables clean separation between policy (rules) and logic (domain)

## Files in This Folder

- Rules-JSON-Format.md → Defines JSON structure
- Architecture-RuleSetReader.md → Explains design choices
- RuleSetReader-Usage.md → How to register and use a rule reader

## City File Naming

{city}.rules.json → lowercase

Example:
gothenburg.rules.json
malmo.rules.json

## Notes

- These rule files are not meant for production
- They serve as deterministic, testable data sources
- Rule readers can be swapped without modifying domain logic
