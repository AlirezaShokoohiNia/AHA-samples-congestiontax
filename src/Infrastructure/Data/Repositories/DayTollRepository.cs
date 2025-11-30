namespace AHA.CongestionTax.Infrastructure.Data.Repositories
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Domain.Abstractions;
    using AHA.CongestionTax.Domain.DayTollAgg;
    using AHA.CongestionTax.Domain.VehicleAgg;
    using Microsoft.EntityFrameworkCore;

    public class DayTollRepository(AppDbContext context)
        : BaseRepository<DayToll>(context), IDayTollRepository
    {
        public async Task<Result<DayToll>> GetByVehicleAndCityAndDateAsync(
            int vehicleId,
            string city,
            DateOnly passDate,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var dayToll = await _dbContext.DayTolls
                    .FirstOrDefaultAsync(dt => dt.Vehicle.Id == vehicleId
                                               && dt.City == city
                                               && dt.Date == passDate, cancellationToken);

                return dayToll is null
                    ? Result.Failure<DayToll>($"DayToll with vehicleid {vehicleId}, city{city} and passDate {passDate} not found.")
                    : Result.Success(dayToll);
            }
            catch (Exception ex)
            {
                return Result.Failure<DayToll>($"Failed to get daytoll with vehicleid {vehicleId}, city{city} and passDate {passDate}: {ex.Message}");
            }

        }

    }
}