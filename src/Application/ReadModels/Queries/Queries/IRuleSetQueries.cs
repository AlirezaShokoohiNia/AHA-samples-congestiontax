namespace AHA.CongestionTax.Application.ReadModels.Queries
{
    using AHA.CongestionTax.Application.ReadModels.Queries.RuleSets;
    using System.Threading.Tasks;

    public interface IRuleSetQueries
    {

        Task<RuleSetReadModel?> GetRulesForCityAsync(string city);
    }

}