namespace AHA.CongestionTax.Domain
{
    /// <summary>
    /// Very small base class for domain entities.
    /// </summary>
    /// <remarks>
    /// Only included to show understanding of DDD structure.
    /// For this assessment, no domain events or advanced equality logic are required.
    /// </remarks>
    public abstract class Entity
    {
        public int Id { get; set; }
    }

}