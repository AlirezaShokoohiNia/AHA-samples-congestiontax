namespace AHA.CongestionTax.Domain
{
    /// <summary>
    /// Marker base class for aggregate roots.
    /// </summary>
    /// <remarks>
    /// Explicit type exists only to clarify boundaries of domain aggregates.
    /// </remarks>
    public abstract class AggregateRoot : Entity
    {
    }

}
