# RuleSetReader Usage Guide

This guide shows how to register and use the file-based rule reader in the Application layer.

## 1. Registering FileRuleSetReader in DI

```csharp
var rulesPath = Path.Combine(AppContext.BaseDirectory, "rules");

services.AddSingleton<IRuleSetReader>(
    new FileRuleSetReader(rulesPath)
);
```
Ensure folder structure:
```
/rules/gothenburg.rules.json
/rules/stockholm.rules.json
```
## 2. Using Rule Reader in a Handler
```csharp
public class CalculateTollCommandHandler
{
    private readonly IRuleSetReader _rules;

    public CalculateTollCommandHandler(IRuleSetReader rules)
    {
        _rules = rules;
    }

    public async Task Handle(CalculateTollCommand command)
    {
        var ruleSet = await _rules.GetRulesForCityAsync(command.City);

        if (ruleSet is null)
            throw new CityNotSupportedException(command.City);

        // pass data to domain service...
    }
}
```
## 3. Testing Support

Test projects copy JSON files under:

AHA.CongestionTax.Application.Tests/TestData/

Then:
```charp
var reader = new FileRuleSetReader(basePath);
```
## 4. Example: Resolving Multiple Cities

Assuming two rule files:
```
rules/gothenburg.rules.json
rules/stockholm.rules.json
```
Handler code remains unchanged:
```csharp
var ruleSet = await _ruleReader.GetRulesForCityAsync("Stockholm");
```
## 5. Notes

- FileRuleSetReader is the default
- New sources (Redis, DB, API) can be added easily
- Domain layer stays clean and independent from rule storage
