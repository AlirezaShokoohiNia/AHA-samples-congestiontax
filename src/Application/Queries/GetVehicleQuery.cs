namespace AHA.CongestionTax.Application.Queries
{
    using AHA.CongestionTax.Application.Abstractions.Queries;

    /// <summary>
    /// Represents a query to retrieve a vehicle by license plate.
    /// </summary>
    /// <param name="LicensePlate">The unique license plate identifier.</param>
    public sealed record GetVehicleQuery(string LicensePlate) : IQuery;
}
