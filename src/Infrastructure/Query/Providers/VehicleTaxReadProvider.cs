namespace AHA.CongestionTax.Infrastructure.Query.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Abstractions.Adapter;
    using AHA.CongestionTax.Application.Abstractions.Query;
    using AHA.CongestionTax.Application.Abstractions.Query.Providers;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Adapters;
    using AHA.CongestionTax.Infrastructure.Query.Source1;
    using Microsoft.EntityFrameworkCore;

    public sealed class VehicleTaxReadProvider(QueryDbContext queryDbContext)
        : IVehicleTaxReadProvider
    {
        public async Task<QueryResult<IReadOnlyCollection<VehicleDailyTaxDto>>> GetDailyTaxRecordsAsync(
            int vehicleId,
            DateOnly fromDate,
            DateOnly toDate,
            CancellationToken cancellationToken = default)
        {

            try
            {
                var daily = await queryDbContext.DayTolls
                    .Where(x => x.VehicleId == vehicleId &&
                                x.Date >= fromDate &&
                                x.Date <= toDate)
                    .ToListAsync(cancellationToken);

                var dtos = MappingHelper.MapEach(
                        daily,
                        DayTollReadModelToVehicleDailyTaxDtoAdapter.Adapt)
                        .ToHashSet()
                        as IReadOnlyCollection<VehicleDailyTaxDto>;

                return QueryResult.Success(dtos);

            }
            catch (Exception ex)
            {
                return QueryResult.Failure<IReadOnlyCollection<VehicleDailyTaxDto>>($"Failed to get tax record with vehicle id '{vehicleId}' within {fromDate} and {toDate}: {ex.Message}");
            }

        }
        public async Task<QueryResult<VehicleTotalTaxDto>> GetWeeklyTotalTaxAsync(
            int vehicleId,
            CancellationToken cancellationToken = default)
        {
            var oneWeekAgo = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-7));

            try
            {

                // Step 1: Find the vehicle
                var vehicle = await queryDbContext.Vehicles
                    .Where(v => v.VehicleId == vehicleId)
                    .Select(v => new { v.VehicleId, v.LicensePlate })
                    .FirstOrDefaultAsync(cancellationToken);

                if (vehicle is null)
                    return QueryResult.Failure<VehicleTotalTaxDto>($"Vehicle with vehicle id {vehicleId} not found");

                // Step 2: Sum tolls for that vehicle
                var totalFee = await queryDbContext.DayTolls
                    .Where(dt => dt.VehicleId == vehicleId && dt.Date >= oneWeekAgo)
                    .SumAsync(dt => (int?)dt.TotalFee, cancellationToken) ?? 0;

                // Step 3: Return DTO
                var dto = new VehicleTotalTaxDto
                {
                    LicensePlate = vehicle.LicensePlate,
                    TotalTax = totalFee
                };
                return QueryResult.Success(dto);
            }
            catch (Exception ex)
            {
                return QueryResult.Failure<VehicleTotalTaxDto>($"Failed to get vehicle total tax with vehicle id '{vehicleId}': {ex.Message}");
            }
        }
    }
}