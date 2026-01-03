namespace AHA.CongestionTax.Infrastructure.Query.Mappers
{
    /// <summary>
    /// Defines a contract for mapping read models to DTOs.
    /// </summary>
    /// <typeparam name="TReadModel">
    /// The type of the read model, typically coming from the infrastructure layer.
    /// </typeparam>
    /// <typeparam name="TDto">
    /// The type of the Data Transfer Object (DTO) used in the application layer.
    /// </typeparam>
    /// <remarks>
    /// This interface leverages <b>static abstract interface members</b>, a new technology introduced in C# 11.
    /// It allows compile-time enforcement of static methods across implementing types.
    /// This feature is particularly useful in generic contexts where you want to guarantee
    /// that all mappers provide consistent static mapping methods.
    /// </remarks>
    [Obsolete("This interface is deprecated and will be removed in future versions. using IMapperAdapter And ITypeAdapter instead.")]
    public interface IReadModelToDtoMapper<TReadModel, TDto>
    {
        /// <summary>
        /// Maps a single read model instance to its corresponding DTO.
        /// </summary>
        /// <param name="readModel">The read model to convert.</param>
        /// <returns>A DTO representing the read model.</returns>
        static abstract TDto Map(TReadModel readModel);

        /// <summary>
        /// Maps a collection of read models to their corresponding DTOs.
        /// </summary>
        /// <param name="readModels">The collection of read models to convert.</param>
        /// <returns>A read-only collection of DTOs representing the read models.</returns>
        static abstract IReadOnlyCollection<TDto> MapMany(IEnumerable<TReadModel> readModels);
    }

}
