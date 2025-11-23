#pragma warning disable CS9113 // Nullable reference types (NRT) are disabled for this file
namespace AHA.CongestionTax.Application.Queries.Readers
{
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Queries.RuleSets;

    public class FileRuleSetReader(string basePath)
        : IRuleSetReader
    {
        public Task<RuleSetQueryModel?> GetRulesForCityAsync(string city)
        {
            throw new NotImplementedException();
        }
    }
}
#pragma warning restore CS9113
