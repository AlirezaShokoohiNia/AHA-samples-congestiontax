# RuleSetReader Architecture

This document explains the design of the city-based rule reader component used by the Application layer.

## Purpose

The Application layer needs a way to fetch congestion-tax rule sets per city:

var rules = await _ruleReader.GetRulesForCityAsync("Gothenburg");

The IRuleSetReader abstraction defines this contract.

## 1. IRuleSetReader (Contract)

```csharp
public interface IRuleSetReader
{
    Task<RuleSetQueryModel?> GetRulesForCityAsync(string city);
}
```
### Responsibilities

- Return a RuleSetQueryModel loaded from a data source
- Return null if the rule file does not exist
- Throw JsonException for invalid JSON
- Perform zero domain logic

## 2. FileRuleSetReader (Default Implementation)

```csharp
public class FileRuleSetReader : IRuleSetReader
{
    ...
}
```
### Why File-Based?

- Best fit for coding assessment
- No infrastructure complexity
- Transparent for reviewers
- Ideal for TDD
- Keeps separation of policy (rules) from behavior (domain)

## 3. Readers Live in Application Layer

Application owns Queries, DTOs, rule loading
Domain owns behavior, invariants, business rules

**Reasons:**

1. Query models are not domain concepts
2. Application orchestrates data fetching
3. Infrastructure reserved for domain persistence
4. Rule loading is not domain persistence

## 4. Future Reader Possibilities

- RedisRuleSetReader
- EfCoreRuleSetReader
- HttpRuleSetReader
- InMemoryRuleSetReader (for tests)

All via DI.

## Summary

The design:

- Simple
- Testable
- Extendable
- Compliant with Clean Architecture
