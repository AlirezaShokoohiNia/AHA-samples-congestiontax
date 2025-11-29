namespace AHA.CongestionTax.Domain.Services
{
    using AHA.CongestionTax.Domain.DayTollAgg;
    using AHA.CongestionTax.Domain.ValueObjects;
    using AHA.CongestionTax.Domain.Abstractions;

    /// <summary>
    /// Defines the contract for congestion tax calculation services.
    /// </summary>
    public interface ICongestionTaxCalculator
    {
        /// <summary>
        /// Calculates the daily congestion tax fee for a given day toll.
        /// </summary>
        /// <param name="dayToll">Day toll aggregate containing vehicle passages.</param>
        /// <param name="timeSlots">Collection of taxable time slots.</param>
        /// <param name="holidays">Set of holiday dates exempt from tolls.</param>
        /// <param name="tollFreeVehicles">Set of vehicle types exempt from tolls.</param>
        /// <param name="dailyMaxFee">Maximum fee charged per day.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> containing the <see cref="DailyTaxResult"/> if calculation succeeds,
        /// or a failure result with an error message if calculation fails.
        /// </returns>
        Result<DailyTaxResult> CalculateDailyFee(DayToll dayToll,
                                                 IReadOnlyCollection<TimeSlot> timeSlots,
                                                 IReadOnlySet<DateOnly> holidays,
                                                 IReadOnlySet<VehicleType> tollFreeVehicles,
                                                 int dailyMaxFee = 60);
    }
}
