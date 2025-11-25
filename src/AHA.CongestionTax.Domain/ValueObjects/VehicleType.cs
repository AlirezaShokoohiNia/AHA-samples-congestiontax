namespace AHA.CongestionTax.Domain.ValueObjects
{
    /// <summary>
    /// Represents the supported vehicle categories for congestion tax evaluation.
    /// </summary>
    public enum VehicleType
    {
        /// <summary>Unspecified or unrecognized vehicle type.</summary>
        Unknown = 0,

        /// <summary>Standard passenger car.</summary>
        Car,

        /// <summary>Motorcycle or motorbike.</summary>
        Motorcycle,

        /// <summary>Emergency response vehicle (tax exempt).</summary>
        Emergency,

        /// <summary>Diplomatic vehicle (tax exempt).</summary>
        Diplomat,

        /// <summary>Military vehicle (tax exempt).</summary>
        Military,

        /// <summary>Foreign-registered vehicle (may be exempt depending on rules).</summary>
        Foreign
    }
}
