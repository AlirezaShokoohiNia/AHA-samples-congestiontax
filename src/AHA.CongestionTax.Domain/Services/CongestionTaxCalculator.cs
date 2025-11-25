namespace AHA.CongestionTax.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using AHA.CongestionTax.Domain.DayTollAgg;
    using AHA.CongestionTax.Domain.ValueObjects;

    /// <summary>
    /// Concrete implementation of <see cref="ICongestionTaxCalculator"/>.
    /// </summary>
    public class CongestionTaxCalculator : ICongestionTaxCalculator
    {
        /// <summary>
        /// Calculates the daily congestion tax according to domain rules.
        /// </summary>
        public DailyTaxResult CalculateDailyFee(DayToll dayToll,
            IReadOnlyCollection<TimeSlot> timeSlots,
            IReadOnlySet<DateOnly> holidays,
            IReadOnlySet<VehicleType> tollFreeVehicles,
            int dailyMaxFee = 60)
        {
            // Free vehicle → zero
            if (tollFreeVehicles.Contains(dayToll.Vehicle.VehicleType))
                return new DailyTaxResult(0, []);

            throw new NotImplementedException();
        }
    }
}
