namespace AHA.CongestionTax.Application.Queries.Readers
{
    using AHA.CongestionTax.Application.Queries.RuleSets;
    using System.Threading.Tasks;

    public interface IRuleSetReader
    {

        Task<RuleSetQueryModel?> GetRulesForCityAsync(string city);
    }

}