namespace AHA.CongestionTax.Application.Abstractions.Adapter
{
    /// <summary>
    /// Provides general-purpose, functional utilities for applying
    /// adapter or mapper logic across sequences.
    ///
    /// <para>
    /// <b>MappingHelper is not an adapter and not a mapper.</b>
    /// It does not define any transformation rules itself.
    /// Instead, it composes existing <see cref="ITypeAdapter{TSource,TDest}"/>
    /// or <see cref="IMapper{TSource,TDest}"/> implementations over collections.
    /// </para>
    ///
    /// </summary>
    public static class MappingHelper
    {
        /// <summary>
        /// Applies a pure transformation function to each element in a source
        /// sequence, producing a new sequence of transformed elements.
        ///
        /// <para>
        /// The provided <paramref name="mapFunc"/> must be pure, deterministic,
        /// and side-effect free. It is typically implemented via:
        /// <list type="bullet">
        /// <item><description>
        /// <see cref="ITypeAdapter{TSource,TDest}"/> for 1→1 adaptation applied over many elements
        /// </description></item>
        /// <item><description>
        /// <see cref="IMapper{TSource,TDest}"/> for multi-cardinality mapping
        /// </description></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <typeparam name="TSource">The source element type.</typeparam>
        /// <typeparam name="TDest">The destination element type.</typeparam>
        /// <param name="source">The input sequence to transform.</param>
        /// <param name="mapFunc">
        /// A pure function that transforms a single <typeparamref name="TSource"/>
        /// into a <typeparamref name="TDest"/>.
        /// </param>
        /// <returns>
        /// A lazily-evaluated sequence of transformed <typeparamref name="TDest"/> values.
        /// </returns>
        public static IEnumerable<TDest> MapEach<TSource, TDest>(
            IEnumerable<TSource> source,
            Func<TSource, TDest> mapFunc)
        {
            foreach (var item in source)
                yield return mapFunc(item);
        }
    }
}
