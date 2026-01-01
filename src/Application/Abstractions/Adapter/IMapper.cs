namespace AHA.CongestionTax.Application.Abstractions.Adapter
{
    /// <summary>
    /// Represents a pure, compile-time mapping contract between two types.
    ///
    /// <para>
    /// <b>Mappers handle all multi‑cardinality transformations:</b>
    /// 1→n, n→1, and n→n.
    /// </para>
    ///
    /// <para>
    /// This interface complements <see cref="ITypeAdapter{TSource,TDest}"/>,
    /// which is reserved exclusively for 1→1 boundary adaptation.
    /// </para>
    ///
    /// <para>
    /// Use mappers for:
    /// aggregations, projections, expansions, flattening, and any transformation
    /// where the output cardinality differs from the input.
    /// </para>
    ///
    /// <para>
    /// This interface replaces the previous unified "mapper" model by making
    /// multi‑cardinality mapping explicit and separating it from 1→1 adaptation.
    /// </para>
    /// </summary>
    /// <typeparam name="TSource">The source type being mapped.</typeparam>
    /// <typeparam name="TDest">The destination type produced by the mapper.</typeparam>
    public interface IMapper<TSource, TDest>
    {
        /// <summary>
        /// Maps a <typeparamref name="TSource"/> instance into a
        /// <typeparamref name="TDest"/> result.
        ///
        /// <para>
        /// This method may represent:
        /// <list type="bullet">
        /// <item><description>1→n transformations</description></item>
        /// <item><description>n→1 aggregations</description></item>
        /// <item><description>n→n projections</description></item>
        /// </list>
        /// </para>
        ///
        /// <para>
        /// For strict 1→1 boundary translation, use
        /// <see cref="ITypeAdapter{TSource,TDest}"/>.
        /// </para>
        /// </summary>
        /// <param name="source">The value or sequence to map.</param>
        /// <returns>The mapped result.</returns>
        static abstract TDest Map(TSource source);
    }
}
