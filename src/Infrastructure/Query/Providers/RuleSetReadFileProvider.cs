namespace AHA.CongestionTax.Infrastructure.Query.Providers
{
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Abstractions.Query;
    using AHA.CongestionTax.Application.Abstractions.Query.Providers;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Adapters;
    using AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels;

    /// <summary>
    /// Provides congestion tax rule sets by reading JSON files from a configured base path.
    /// </summary>
    /// <remarks>
    /// Each rule set must follow the convention <c>{city}.rules.json</c>.
    /// This provider enforces convention-based lookup to ensure consistency across environments.
    /// Initializes a new instance of the <see cref="RuleSetReadFileProvider"/>.
    /// </remarks>
    /// <param name="basePath">The directory path containing rule set files.</param>
    public class RuleSetReadFileProvider(string basePath)
        : IRuleSetReadProvider
    {
        private static readonly JsonSerializerOptions _readOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        /// <inheritdoc />
        public async Task<QueryResult<RuleSetDto>> GetRulesForCityAsync(
            string city,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var filename = $"{city.ToLowerInvariant()}.rules.json";
                var path = Path.Combine(basePath, filename);

                if (!File.Exists(path))
                {
                    return QueryResult.Failure<RuleSetDto>(
                        $"Rule set file not found for city '{city}'.");
                }

                var json = await File.ReadAllTextAsync(path, cancellationToken);
                var model = JsonSerializer.Deserialize<RuleSetReadModel>(json, _readOptions);

                if (model is null)
                {
                    return QueryResult.Failure<RuleSetDto>(
                        $"Failed to deserialize rule set for city '{city}'.");
                }

                return QueryResult.Success(RuleSetReadModelToRuleSetDtoAdapter.Adapt(model));
            }
            catch (JsonException jex)
            {
                return QueryResult.Failure<RuleSetDto>(
                    $"Invalid JSON format for city '{city}': {jex.Message}");
            }
            catch (Exception ex)
            {
                return QueryResult.Failure<RuleSetDto>(
                    $"Unexpected error reading rule set for city '{city}': {ex.Message}");
            }

        }
    }
}