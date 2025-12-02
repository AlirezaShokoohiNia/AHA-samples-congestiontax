namespace AHA.CongestionTax.Application.Queries
{
    using AHA.CongestionTax.Application.Abstractions.Queries;

    /// <summary>
    /// Query to retrieve the total tax for a vehicle
    /// over the last week.
    /// </summary>
    /// <param name="VehicleId">The unique identifier of the vehicle.</param>
    public sealed record GetVehicleWeeklyTotalTaxQuery(int VehicleId)
        : IQuery;
}