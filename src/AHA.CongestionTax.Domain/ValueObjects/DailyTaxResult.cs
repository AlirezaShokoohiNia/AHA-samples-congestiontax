namespace AHA.CongestionTax.Domain.ValueObjects
{
    /// <summary>
    /// Represents the result of a single day's tax calculation:
    /// total fee and individual pass fees.
    /// </summary>
    public readonly record struct DailyTaxResult(
        int TotalFee,
        IReadOnlyList<int> PassFees
    );
}
