namespace AHA.CongestionTax.Application.Queries.Readers
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Queries.RuleSets;

    public class FileRuleSetReader(string basePath)
        : IRuleSetReader
    {
        private static readonly JsonSerializerOptions _readOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<RuleSetQueryModel?> GetRulesForCityAsync(string city)
        {
            var filename = $"{city.ToLowerInvariant()}.rules.json";
            var path = Path.Combine(basePath, filename);

            var json = await File.ReadAllTextAsync(path);

            return JsonSerializer.Deserialize<RuleSetQueryModel>(
                json,
                _readOptions
            );

        }
    }
}
