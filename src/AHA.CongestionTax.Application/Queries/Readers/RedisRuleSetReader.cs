namespace AHA.CongestionTax.Application.Queries.Readers
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Queries.RuleSets;
    using StackExchange.Redis;

    public class RedisRuleSetReader(IConnectionMultiplexer redis)
        : IRuleSetReader
    {
        public async Task<RuleSetQueryModel?> GetRulesForCityAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City must be provided", nameof(city));

            var db = redis.GetDatabase();
            string key = $"toll:rules:city:{city}";

            var json = await db.StringGetAsync(key);

            return json.IsNullOrEmpty ? null : JsonSerializer.Deserialize<RuleSetQueryModel>(json!);
        }
    }
}