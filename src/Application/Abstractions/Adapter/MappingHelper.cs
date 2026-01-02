namespace AHA.CongestionTax.Application.Abstractions.Adapter
{
    public static class MappingHelper
    {
        public static IEnumerable<TDest> MapEach<TSource, TDest>(
            IEnumerable<TSource> source,
            Func<TSource, TDest> mapFunc)
        {
            foreach (var item in source)
                yield return mapFunc(item);
        }
    }
}
