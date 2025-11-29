namespace AHA.CongestionTax.Application.Commands
{
    /// <summary>
    /// Command representing a single checkpoint pass registration.
    /// </summary>
    public sealed record RegisterPassCommand(
        string LicensePlate,
        DateTime Timestamp
    );
}
