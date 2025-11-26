namespace AHA.CongestionTax.Infrastructure.Data.Repositories
{
    using AHA.CongestionTax.Domain.DayTollAgg;

    public class DayTollRepository(AppDbContext context)
        : BaseRepository<DayToll>(context), IDayTollRepository
    {
    }
}