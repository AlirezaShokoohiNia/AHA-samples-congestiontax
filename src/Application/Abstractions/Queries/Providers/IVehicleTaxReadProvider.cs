namespace AHA.CongestionTax.Application.Abstractions.Queries.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.DTOs;

    /// <summary>
    /// Defines the contract for retrieving tax information for vehicles.
    /// </summary>
    public interface IVehicleTaxReadProvider
    {
        /// <summary>
        /// Retrieves the total tax for the given vehicle over the last week.
        /// </summary>
        Task<QueryResult<VehicleTotalTaxDto>> GetWeeklyTotalTaxAsync(
            int vehicleId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the daily tax amounts per city for the given vehicle
        /// within the specified date range.
        /// </summary>
        Task<QueryResult<IReadOnlyCollection<VehicleDailyTaxDto>>> GetDailyTaxPerCityAsync(
            int vehicleId,
            DateOnly fromDate,
            DateOnly toDate,
            CancellationToken cancellationToken = default);
    }
}
