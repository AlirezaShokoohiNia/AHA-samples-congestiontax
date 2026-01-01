namespace AHA.CongestionTax.Application.Abstractions.Adapter
{
    /// <summary>
    /// Represents a pure, compile-time adaptation contract between two types.
    /// 
    /// <para>
    /// <b>Adapters are strictly 1→1 transformations.</b>  
    /// They are used when crossing architectural boundaries such as:
    /// DTO → ValueObject, DTO → primitive, ReadModel → DTO, etc.
    /// </para>
    ///
    /// <para>
    /// Implementations must be:
    /// pure, deterministic, side‑effect free, and intention‑revealing.
    /// </para>
    /// </summary>
    /// <typeparam name="TSource">The source type being adapted.</typeparam>
    /// <typeparam name="TDest">The destination type produced by the adapter.</typeparam>
    public interface ITypeAdapter<TSource, TDest>
    {
        /// <summary>
        /// Adapts a single <typeparamref name="TSource"/> instance into a
        /// single <typeparamref name="TDest"/> instance.
        ///
        /// <para>
        /// This method expresses a <b>1→1</b> transformation and must not
        /// perform any multi‑cardinality logic.  
        /// For 1→n, n→1, or n→n transformations, use <see cref="IMapper{TSource,TDest}"/>.
        /// </para>
        /// </summary>
        /// <param name="source">The value to adapt.</param>
        /// <returns>A new <typeparamref name="TDest"/> instance.</returns>
        static abstract TDest Adapt(TSource source);
    }
}
