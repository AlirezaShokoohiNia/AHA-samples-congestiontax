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

        [Fact]
        public void Adapt_Should_Return_Unknown_For_Null_Or_Whitespace()
        {
            var dtoNull = new VehicleFreeRuleDto { VehicleType = null! };
            var dtoWhitespace = new VehicleFreeRuleDto { VehicleType = "   " };

            var resultNull = VehicleFreeRuleDtoToVehicleTypeAdapter.Adapt(dtoNull);
            var resultWhitespace = VehicleFreeRuleDtoToVehicleTypeAdapter.Adapt(dtoWhitespace);

            Assert.Equal(VehicleType.Unknown, resultNull);
            Assert.Equal(VehicleType.Unknown, resultWhitespace);
        }
    }

}
