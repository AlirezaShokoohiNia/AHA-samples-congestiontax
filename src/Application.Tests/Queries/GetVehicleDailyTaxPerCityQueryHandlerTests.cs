namespace AHA.CongestionTax.Application.Queries.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Abstractions;
    using AHA.CongestionTax.Application.Abstractions.Queries.Providers;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Application.Queries;
    using Moq;
    using Xunit;

    public class GetVehicleDailyTaxPerCityQueryHandlerTests
    {

        [Fact]
        public async Task DailyTaxPerCityHandler_ReturnsSuccess()
        {
            var dtoList = new List<VehicleDailyTaxDto>
        {
            new() { LicensePlate = "AAA", Date = DateOnly.FromDateTime(DateTime.Today), City = "Tehran", Tax = 20 }
        };

            var mockProvider = new Mock<IVehicleTaxReadProvider>();
            mockProvider
                .Setup(p => p.GetDailyTaxPerCityAsync(1, It.IsAny<DateOnly>(), It.IsAny<DateOnly>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(QueryResult.Success<IReadOnlyCollection<VehicleDailyTaxDto>>(dtoList));

            var handler = new GetVehicleDailyTaxPerCityQueryHandler(mockProvider.Object);

            var result = await handler.HandleAsync(new GetVehicleDailyTaxPerCityQuery(1, DateOnly.FromDateTime(DateTime.Today.AddDays(-7)), DateOnly.FromDateTime(DateTime.Today)));

            Assert.True(result.IsSuccess);
            Assert.Single(result.Value!);
            Assert.Equal("Tehran", result.Value!.First().City);
        }

        [Fact]
        public async Task DailyTaxPerCityHandler_Throws_WhenQueryIsNull()
        {
            var mockProvider = new Mock<IVehicleTaxReadProvider>();
            var handler = new GetVehicleDailyTaxPerCityQueryHandler(mockProvider.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.HandleAsync(null!));
        }
    }

}