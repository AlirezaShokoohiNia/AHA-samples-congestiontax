namespace AHA.CongestionTax.Application.Commands
{
    /// <summary>
    /// Command requesting a congestion tax calculation for a vehicle.
    /// </summary>
    public sealed record CalculateTaxCommand(
        string LicensePlate,
        DateOnly Date
    );
}