namespace AHA.CongestionTax.Infrastructure.Data.ReadModels.Providers
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Infrastructure.Data.ReadModels.Queries;
    using AHA.CongestionTax.Infrastructure.Data.ReadModels.Queries.RuleSets;

    public class RuleSetFileQueryProvider(string basePath)
        : IRuleSetQueries
    {
        private static readonly JsonSerializerOptions _readOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<RuleSetReadModel?> GetRulesForCityAsync(string city)
        {
            var filename = $"{city.ToLowerInvariant()}.rules.json";
            var path = Path.Combine(basePath, filename);

            if (!File.Exists(path))
            {
                return null;
            }

            var json = await File.ReadAllTextAsync(path);

            return JsonSerializer.Deserialize<RuleSetReadModel>(
                json,
                _readOptions
            );

        }
    }
}
