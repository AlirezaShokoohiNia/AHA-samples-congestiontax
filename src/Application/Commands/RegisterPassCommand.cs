namespace AHA.CongestionTax.Application.Commands
{
    using AHA.CongestionTax.Application.Abstractions;

    /// <summary>
    /// Command representing a single checkpoint pass registration.
    /// </summary>
    public sealed record RegisterPassCommand(
        string LicensePlate,
        DateTime Timestamp
        ) : ICommand;
}