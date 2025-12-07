namespace AHA.CongestionTax.Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Abstractions.Query;
    using AHA.CongestionTax.Application.Abstractions.Query.Providers;
    using AHA.CongestionTax.Application.DTOs;

    /// <summary>
    /// Handles the query for retrieving daily tax per city of a vehicle.
    /// </summary>
    public class GetVehicleDailyTaxRecordsQueryHandler(IVehicleTaxReadProvider provider)
                : IQueryHandler<GetVehicleDailyTaxRecordsQuery, IReadOnlyCollection<VehicleDailyTaxDto>>
    {

        public Task<QueryResult<IReadOnlyCollection<VehicleDailyTaxDto>>> HandleAsync(
            GetVehicleDailyTaxRecordsQuery query,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(query);
            return provider.GetDailyTaxRecordsAsync(
                query.VehicleId,
                query.FromDate,
                query.ToDate,
                cancellationToken);
        }
    }
}
