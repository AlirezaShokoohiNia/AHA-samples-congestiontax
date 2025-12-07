namespace AHA.CongestionTax.Application.Abstractions.Query
{
    /// <summary>
    /// Represents a marker interface for application queries
    /// in the CQRS pattern. Queries encapsulate requests for
    /// data retrieval and must not alter system state.
    /// </summary>
    public interface IQuery { }
}
