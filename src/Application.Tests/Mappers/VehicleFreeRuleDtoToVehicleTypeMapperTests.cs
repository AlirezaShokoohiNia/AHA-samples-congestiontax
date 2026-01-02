namespace AHA.CongestionTax.Application.Mappers.Tests
{
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Domain.ValueObjects;
    using Xunit;

    [Obsolete("Use VehicleFreeRuleDtoToVehicleTypeAdapterTests instead")]
    public class VehicleFreeRuleDtoToVehicleTypeMapperTests
    {
        [Theory]
        [InlineData("Car", VehicleType.Car)]
        [InlineData("motorcycle", VehicleType.Motorcycle)]
        [InlineData("Emergency", VehicleType.Emergency)]
        [InlineData("Diplomat", VehicleType.Diplomat)]
        [InlineData("Military", VehicleType.Military)]
        [InlineData("Foreign", VehicleType.Foreign)]
        [InlineData("UnknownType", VehicleType.Unknown)]
        public void Map_ShouldConvertStringToEnum(string input, VehicleType expected)
        {
            var dto = new VehicleFreeRuleDto { VehicleType = input };

            var result = VehicleFreeRuleDtoToVehicleTypeMapper.Map(dto);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void MapMany_ShouldConvertMultipleDtos()
        {
            var dtos = new[]
            {
            new VehicleFreeRuleDto { VehicleType = "Car" },
            new VehicleFreeRuleDto { VehicleType = "Emergency" }
        };

            var results = VehicleFreeRuleDtoToVehicleTypeMapper.MapMany(dtos).ToList();

            Assert.Equal(2, results.Count);
            Assert.Equal(VehicleType.Car, results[0]);
            Assert.Equal(VehicleType.Emergency, results[1]);
        }
    }

}
