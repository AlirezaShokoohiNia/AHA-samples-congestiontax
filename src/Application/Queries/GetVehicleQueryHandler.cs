namespace AHA.CongestionTax.Application.Queries
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Abstractions.Query;
    using AHA.CongestionTax.Application.Abstractions.Query.Providers;
    using AHA.CongestionTax.Application.DTOs;

    /// <summary>
    /// Handles the <see cref="GetVehicleQuery"/> by delegating to the read provider
    /// and returning the resulting <see cref="QueryResult{T}"/>.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the handler with the specified provider.
    /// </remarks>
    /// <param name="vehicleReadProvider">The vehicle read provider.</param>
    public class GetVehicleQueryHandler(IVehicleReadProvider vehicleReadProvider)
            : IQueryHandler<GetVehicleQuery, VehicleDto>
    {

        /// <inheritdoc />
        public Task<QueryResult<VehicleDto>> HandleAsync(
            GetVehicleQuery query,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(query);
            return vehicleReadProvider.GetVehicleAsync(query.LicensePlate, cancellationToken);
        }
    }
}
