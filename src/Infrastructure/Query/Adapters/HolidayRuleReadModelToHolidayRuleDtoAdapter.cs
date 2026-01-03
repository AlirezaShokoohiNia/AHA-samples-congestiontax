namespace AHA.CongestionTax.Infrastructure.Query.Adapters
{
    using AHA.CongestionTax.Application.Abstractions.Adapter;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels;

    public class HolidayRuleReadModelToHolidayRuleDtoAdapter
        : ITypeAdapter<HolidayRuleReadModel, HolidayRuleDto>
    {
        public static HolidayRuleDto Adapt(HolidayRuleReadModel readModel)
            => new()
            {
                Date = readModel.Date,
                AppliesToDayBefore = readModel.AppliesToDayBefore
            };

    }
}