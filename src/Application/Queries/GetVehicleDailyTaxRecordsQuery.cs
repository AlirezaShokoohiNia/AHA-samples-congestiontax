namespace AHA.CongestionTax.Application.Queries
{
    using AHA.CongestionTax.Application.Abstractions.Queries;

    /// <summary>
    /// Query to retrieve daily tax amounts records for a vehicle
    /// within a specified date range.
    /// </summary>
    /// <param name="VehicleId">The unique identifier of the vehicle.</param>
    /// <param name="FromDate">The start date of the range.</param>
    /// <param name="ToDate">The end date of the range.</param>
    public sealed record GetVehicleDailyTaxRecordsQuery(
        int VehicleId,
        DateOnly FromDate,
        DateOnly ToDate)
        : IQuery;
}