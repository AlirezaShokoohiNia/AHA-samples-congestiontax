namespace AHA.CongestionTax.Application.Queries.Readers
{
    using AHA.CongestionTax.Application.Queries.RuleSets;
    using StackExchange.Redis;
    using System.Threading.Tasks;

    public class RedisRuleSetReader(IConnectionMultiplexer redis)
        : IRuleSetReader
    {
        public Task<RuleSetQueryModel?> GetRulesForCityAsync(string city)
        {
            throw new NotImplementedException();
        }
    }
}