namespace AHA.CongestionTax.Domain.VehicleAgg
{
    using System.Threading;
    using AHA.CongestionTax.Domain.Abstractions;

    /// <summary>
    /// Repository abstraction for managing <see cref="Vehicle"/> aggregates.
    /// </summary>
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        /// <summary>
        /// Asynchronously checks whether a vehicle with the given license plate exists.
        /// </summary>
        /// <param name="licensePlate">The license plate to check.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> containing <c>true</c> if a vehicle with the given plate exists,
        /// <c>false</c> if not, or a failure result with an error message if the query fails.
        /// </returns>
        Task<Result<bool>> ExistsByPlateAsync(string licensePlate, CancellationToken cancellationToken = default);

    }
}
