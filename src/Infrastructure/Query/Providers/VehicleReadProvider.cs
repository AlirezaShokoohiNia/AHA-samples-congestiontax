namespace AHA.CongestionTax.Infrastructure.Query.Source1.Providers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Abstractions;
    using AHA.CongestionTax.Application.Abstractions.Queries.Providers;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Mappers;
    using Microsoft.EntityFrameworkCore;

    public sealed class VehicleReadProvider(QueryDbContext queryDbContext)
        : IVehicleReadProvider
    {
        public async Task<QueryResult<VehicleDto>> GetVehicleAsync(
            string licensePlate,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
                return QueryResult.Failure<VehicleDto>("invalid-platenumber");

            try
            {
                var vehicle = await queryDbContext.Vehicles
                    .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate, cancellationToken);
                return vehicle is null
                    ? QueryResult.Failure<VehicleDto>($"Vehicle with plate {licensePlate} not found.")
                    : QueryResult.Success(VehicleReadModelToVehicleDTOMapper.Map(vehicle));
            }
            catch (Exception ex)
            {
                return QueryResult.Failure<VehicleDto>($"Failed to get vehicle with plate '{licensePlate}': {ex.Message}");
            }

        }
    }
}