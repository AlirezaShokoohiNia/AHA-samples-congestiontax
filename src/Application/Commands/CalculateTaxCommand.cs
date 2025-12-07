namespace AHA.CongestionTax.Application.Commands
{
    using AHA.CongestionTax.Application.Abstractions.Commands;

    /// <summary>
    /// Command requesting a congestion tax calculation for a vehicle.
    /// </summary>
    public sealed record CalculateTaxCommand(
        string LicensePlate,
        DateOnly Date
    ) : ICommand;
}