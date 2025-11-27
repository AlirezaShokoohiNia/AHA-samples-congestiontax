namespace AHA.CongestionTax.Infrastructure.Data.Repositories
{
    using AHA.CongestionTax.Domain.VehicleAgg;

    public class VehicleRepository(AppDbContext context)
        : BaseRepository<Vehicle>(context), IVehicleRepository
    {
    }
}