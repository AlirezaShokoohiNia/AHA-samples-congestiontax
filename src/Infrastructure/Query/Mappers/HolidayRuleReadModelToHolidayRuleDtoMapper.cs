namespace AHA.CongestionTax.Infrastructure.Query.Mappers
{
    using System.Collections.Generic;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels;

    /// <summary>
    /// Maps a HolidayRule read model into a HolidayRule dto.
    /// </summary>
    [Obsolete("This mapper is deprecated and will be removed in future versions. using HolidayRuleReadModelToHolidayRuleDtoAdapter instead.")]
    public class HolidayRuleReadModelToHolidayRuleDtoMapper
        : IReadModelToDtoMapper<HolidayRuleReadModel, HolidayRuleDto>
    {
        /// <summary>
        /// Converts a single HolidayRule read model into a HolidayRule DTO.
        /// </summary>
        public static HolidayRuleDto Map(HolidayRuleReadModel readModel)
            => new()
            {
                Date = readModel.Date,
                AppliesToDayBefore = readModel.AppliesToDayBefore
            };

        /// <summary>
        /// Converts a collection of HolidayRule read models into HolidayRule DTOs.
        /// </summary>
        public static IReadOnlyCollection<HolidayRuleDto> MapMany(IEnumerable<HolidayRuleReadModel> readModels)
            => [.. readModels.Select(Map)];
    }
}