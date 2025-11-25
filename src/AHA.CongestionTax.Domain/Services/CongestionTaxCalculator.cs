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

            // Weekend or holiday → zero
            if (IsWeekend(dayToll.Date) || holidays.Contains(dayToll.Date))
                return new DailyTaxResult(0, []);

            // No passes → zero
            if (dayToll.Passes.Count == 0)
                return new DailyTaxResult(0, []);

            var ordered = dayToll.Passes
                           .OrderBy(p => p.Time)
                           .ToList();

            var fees = new List<int>();
            var currentWindowStart = ordered.First().Time;
            var maxFeeInWindow = GetFee(ordered.First().Time, timeSlots);

            fees.Add(maxFeeInWindow);

            for (int i = 1; i < ordered.Count; i++)
            {
                var thisPass = ordered[i];
                var fee = GetFee(thisPass.Time, timeSlots);

                if (IsWithin60Min(currentWindowStart, thisPass.Time))
                {
                    // Apply single charge rule: highest fee wins
                    if (fee > maxFeeInWindow)
                    {
                        maxFeeInWindow = fee;
                        fees[^1] = fee;// replace last with higher fee
                    }
                }
                else
                {
                    // New window
                    currentWindowStart = thisPass.Time;
                    maxFeeInWindow = fee;
                    fees.Add(fee);
                }
            }

            var total = fees.Sum();

            // Apply daily max rule
            if (total > dailyMaxFee)
                total = dailyMaxFee;

            return new DailyTaxResult(total, fees);
        }

        #region Private Methods

        private static bool IsWeekend(DateOnly date)
        {
            var day = date.DayOfWeek;
            return day is DayOfWeek.Saturday or DayOfWeek.Sunday;
        }

        private static int GetFee(TimeOnly time, IReadOnlyCollection<TimeSlot> slots)
        {
            foreach (var s in slots)
                if (s.Contains(time))
                    return s.Fee;

            return 0; // Default if unmatched
        }

        private static bool IsWithin60Min(TimeOnly start, TimeOnly next) =>
            next.ToTimeSpan() - start.ToTimeSpan() <= TimeSpan.FromMinutes(60);


        #endregion
    }
}
