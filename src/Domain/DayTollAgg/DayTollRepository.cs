namespace AHA.CongestionTax.Domain.DayTollAgg
{
    using AHA.CongestionTax.Domain.Abstractions;

    /// <summary>
    /// Repository abstraction for managing <see cref="DayToll"/> aggregates. 
    /// </summary>
    public interface IDayTollRepository : IRepository<DayToll>
    {
        /// <summary>
        /// Asynchronously get a DayToll with the given vehicle id, city name and date.
        /// </summary>
        /// <param name="vehicleId">The vehicle unique id to get.</param>
        /// <param name="city">The city name to get.</param>
        /// <param name="passDate">The date to get.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> containing the daytoll instacnce if found,
        /// or a failure result with an error message if not found.
        /// </returns>
        Task<Result<DayToll>> GetByVehicleAndCityAndDateAsync(
            int vehicleId,
            string city,
            DateOnly passDate,
            CancellationToken cancellationToken = default);
    }
}