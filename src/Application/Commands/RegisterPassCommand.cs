namespace AHA.CongestionTax.Application.Commands
{
    using AHA.CongestionTax.Application.Abstractions;

    /// <summary>
    /// Command representing a single checkpoint pass registration in a city.
    /// </summary>
    public sealed record RegisterPassCommand(
        string LicensePlate,
        DateTime Timestamp,
        string City
        ) : ICommand;
}