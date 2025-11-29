namespace AHA.CongestionTax.Infrastructure.Data.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Domain.Abstractions;
    using AHA.CongestionTax.Domain.VehicleAgg;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// EF Core implementation of <see cref="IVehicleRepository"/>.
    /// Provides persistence operations for <see cref="Vehicle"/> aggregates.
    /// </summary>
    public class VehicleRepository(AppDbContext context)
        : BaseRepository<Vehicle>(context), IVehicleRepository
    {
        public async Task<Result<bool>> ExistsByPlateAsync(string licensePlate, CancellationToken cancellationToken = default)
        {
            try
            {
                var exists = await _dbContext.Vehicles
                    .AnyAsync(v => v.LicensePlate == licensePlate, cancellationToken);

                return Result.Success(exists);
            }
            catch (Exception ex)
            {
                return Result.Failure<bool>($"Failed to check existence of vehicle with plate '{licensePlate}': {ex.Message}");
            }
        }
    }
}