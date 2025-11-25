namespace AHA.CongestionTax.Domain.Services
{
    using AHA.CongestionTax.Domain.VehicleAgg;
    using AHA.CongestionTax.Domain.DayTollAgg;
    using AHA.CongestionTax.Domain.ValueObjects;

    public interface ICongestionTaxCalculator
    {
        DailyTaxResult CalculateDailyFee(DayToll dayToll,
                                         IReadOnlyCollection<TimeSlot> timeSlots,
                                         IReadOnlySet<DateOnly> holidays,
                                         IReadOnlySet<VehicleType> tollFreeVehicles,
                                         int dailyMaxFee = 60);
    }
}
