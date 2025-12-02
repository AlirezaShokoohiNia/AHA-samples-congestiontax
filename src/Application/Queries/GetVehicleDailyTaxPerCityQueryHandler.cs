namespace AHA.CongestionTax.Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Abstractions;
    using AHA.CongestionTax.Application.Abstractions.Queries;
    using AHA.CongestionTax.Application.Abstractions.Queries.Providers;
    using AHA.CongestionTax.Application.DTOs;

    /// <summary>
    /// Handles the query for retrieving daily tax per city of a vehicle.
    /// </summary>
    public class GetVehicleDailyTaxPerCityQueryHandler(IVehicleTaxReadProvider provider)
                : IQueryHandler<GetVehicleDailyTaxPerCityQuery, IReadOnlyCollection<VehicleDailyTaxDto>>
    {

        public Task<QueryResult<IReadOnlyCollection<VehicleDailyTaxDto>>> HandleAsync(
            GetVehicleDailyTaxPerCityQuery query,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(query);
            return provider.GetDailyTaxPerCityAsync(
                query.VehicleId,
                query.FromDate,
                query.ToDate,
                cancellationToken);
        }
    }
}
