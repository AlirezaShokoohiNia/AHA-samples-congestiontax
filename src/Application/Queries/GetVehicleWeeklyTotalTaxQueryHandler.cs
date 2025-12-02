namespace AHA.CongestionTax.Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Abstractions;
    using AHA.CongestionTax.Application.Abstractions.Queries;
    using AHA.CongestionTax.Application.Abstractions.Queries.Providers;
    using AHA.CongestionTax.Application.DTOs;

    /// <summary>
    /// Handles the query for retrieving weekly total tax of a vehicle.
    /// </summary>
    public class GetVehicleWeeklyTotalTaxQueryHandler(IVehicleTaxReadProvider provider)
                : IQueryHandler<GetVehicleWeeklyTotalTaxQuery, VehicleTotalTaxDto>
    {

        public Task<QueryResult<VehicleTotalTaxDto>> HandleAsync(
            GetVehicleWeeklyTotalTaxQuery query,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(query);
            return provider.GetWeeklyTotalTaxAsync(query.VehicleId, cancellationToken);
        }
    }

}
