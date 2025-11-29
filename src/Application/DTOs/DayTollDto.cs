namespace AHA.CongestionTax.Application.Dtos
{
    /// <summary>
    /// DTO representing a day's list of vehicle passes.
    /// This is used by write-side commands to register or update
    /// the list of passes for a given date.
    /// </summary>
    public sealed class DayTollDto
    {
        /// <summary>
        /// The date to which all pass events belong.
        /// </summary>
        public DateOnly Date { get; init; }

        /// <summary>
        /// Collection of pass records for the day.
        /// </summary>
        public IReadOnlyCollection<PassDto> Passes { get; init; }
            = [];
    }
}
