namespace AHA.CongestionTax.Infrastructure.Query.Source1.Providers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Abstractions;
    using AHA.CongestionTax.Application.Abstractions.Queries.Providers;
    using AHA.CongestionTax.Application.DTOs;

    public sealed class VehicleReadProvider(QueryDbContext queryDbContext)
        : IVehicleReadProvider
    {
        public Task<QueryResult<VehicleDto>> GetVehicleAsync(
            string licensePlate,
            CancellationToken cancellationToken = default)
        {
            _ = queryDbContext;
            throw new NotImplementedException();
        }
    }
}