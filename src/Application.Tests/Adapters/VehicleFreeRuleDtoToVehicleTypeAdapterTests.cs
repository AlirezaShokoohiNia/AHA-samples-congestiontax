namespace AHA.CongestionTax.Application.Adapters.Tests
{
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Domain.ValueObjects;
    using Xunit;

    public class VehicleFreeRuleDtoToVehicleTypeAdapterTests
    {
        [Theory]
        [InlineData("Car", VehicleType.Car)]
        [InlineData("motorcycle", VehicleType.Motorcycle)]
        [InlineData("Emergency", VehicleType.Emergency)]
        [InlineData("Diplomat", VehicleType.Diplomat)]
        [InlineData("Military", VehicleType.Military)]
        [InlineData("Foreign", VehicleType.Foreign)]
        [InlineData("UnknownType", VehicleType.Unknown)]
        public void Adapt_Should_Convert_String(string input, VehicleType expected)
        {
            var dto = new VehicleFreeRuleDto { VehicleType = input };

            var result = VehicleFreeRuleDtoToVehicleTypeAdapter.Adapt(dto);

            Assert.Equal(expected, result);
        }

    }

}
