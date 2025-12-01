namespace AHA.CongestionTax.Application.Mappers.Tests
{
    using AHA.CongestionTax.Application.ReadModels.Queries.RuleSets;
    using AHA.CongestionTax.Domain.ValueObjects;
    using Xunit;

    public class VehicleFreeRuleReadModelToVehicleTypeMapperTests
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
            var readModel = new VehicleFreeRuleReadModel { VehicleType = input };

            var result = VehicleFreeRuleReadModelToVehicleTypeMapper.Map(readModel);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void MapMany_ShouldConvertMultipleReadModels()
        {
            var readModels = new[]
            {
            new VehicleFreeRuleReadModel { VehicleType = "Car" },
            new VehicleFreeRuleReadModel { VehicleType = "Emergency" }
        };

            var results = VehicleFreeRuleReadModelToVehicleTypeMapper.MapMany(readModels).ToList();

            Assert.Equal(2, results.Count);
            Assert.Equal(VehicleType.Car, results[0]);
            Assert.Equal(VehicleType.Emergency, results[1]);
        }
    }

}
