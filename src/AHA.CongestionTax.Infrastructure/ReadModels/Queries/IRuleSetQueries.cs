namespace AHA.CongestionTax.Infrastructure.Data.ReadModels.Queries
{
    using AHA.CongestionTax.Infrastructure.Data.ReadModels.Queries.RuleSets;
    using System.Threading.Tasks;

    public interface IRuleSetQueries
    {

        Task<RuleSetReadModel?> GetRulesForCityAsync(string city);
    }

}