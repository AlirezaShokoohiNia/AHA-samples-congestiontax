namespace AHA.CongestionTax.Infrastructure.Query.Adapters
{
    using AHA.CongestionTax.Application.Abstractions.Adapter;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels;

    public class TimeSlotRuleReadModelToTimeSlotRuleDtoAdapter
        : ITypeAdapter<TimeSlotRuleReadModel, TimeSlotRuleDto>
    {
        public static TimeSlotRuleDto Adapt(TimeSlotRuleReadModel readModel)
            => new()
            {
                StartHour = readModel.StartHour,
                StartMinute = readModel.StartMinute,
                EndHour = readModel.EndHour,
                EndMinute = readModel.EndMinute,
                Amount = readModel.Amount
            };

    }
}
