namespace AHA.CongestionTax.Application.Abstractions.Queries.Providers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.DTOs;

    /// <summary>
    /// Defines the read provider contract for retrieving vehicle data
    /// from query-optimized sources (e.g., read models, views).
    /// </summary>
    public interface IVehicleReadProvider
    {
        /// <summary>
        /// Retrieves vehicle information by the specified license plate.
        /// </summary>
        /// 
        /// <param name="licensePlate">The unique license plate identifier.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the operation to complete.</param>
        /// 
        /// <returns>
        /// A <see cref="QueryResult{T}"/> containing the vehicle data if found,
        /// or an appropriate status when not found or on failure.
        /// </returns>
        Task<QueryResult<VehicleDto>> GetVehicleAsync(
            string licensePlate,
            CancellationToken cancellationToken = default);
    }
}
