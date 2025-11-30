namespace AHA.CongestionTax.Infrastructure.Data.ReadModels.Providers
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.ReadModels.Queries;
    using AHA.CongestionTax.Application.ReadModels.Queries.RuleSets;
    using AHA.CongestionTax.Application.Abstractions;

    /// <summary>
    /// File-based query provider for rule sets.
    /// Reads JSON files named {city}.rules.json from a base path.
    /// </summary>
    public class RuleSetFileQueryProvider(string basePath)
        : IRuleSetQueries
    {
        private static readonly JsonSerializerOptions _readOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<ReadModelsQueryResult<RuleSetReadModel>> GetRulesForCityAsync(string city)
        {
            try
            {
                var filename = $"{city.ToLowerInvariant()}.rules.json";
                var path = Path.Combine(basePath, filename);

                if (!File.Exists(path))
                {
                    return ReadModelsQueryResult.Failure<RuleSetReadModel>(
                        $"Rule set file not found for city '{city}'.");
                }

                var json = await File.ReadAllTextAsync(path);
                var model = JsonSerializer.Deserialize<RuleSetReadModel>(json, _readOptions);

                if (model is null)
                {
                    return ReadModelsQueryResult.Failure<RuleSetReadModel>(
                        $"Failed to deserialize rule set for city '{city}'.");
                }

                return ReadModelsQueryResult.Success(model);
            }
            catch (JsonException jex)
            {
                return ReadModelsQueryResult.Failure<RuleSetReadModel>(
                    $"Invalid JSON format for city '{city}': {jex.Message}");
            }
            catch (Exception ex)
            {
                return ReadModelsQueryResult.Failure<RuleSetReadModel>(
                    $"Unexpected error reading rule set for city '{city}': {ex.Message}");
            }
        }
    }
}
