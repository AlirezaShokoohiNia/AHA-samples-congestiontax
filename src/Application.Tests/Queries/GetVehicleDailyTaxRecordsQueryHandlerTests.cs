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

    public class GetVehicleDailyTaxRecordsQueryHandlerTests
    {

        [Fact]
        public async Task DailyTaxRecordsQueryHandler_ReturnsSuccess()
        {
            var dtoList = new List<VehicleDailyTaxDto>
        {
            new() { LicensePlate = "AAA", Date = DateOnly.FromDateTime(DateTime.Today), City = "Tehran", Tax = 20 }
        };

            var mockProvider = new Mock<IVehicleTaxReadProvider>();
            mockProvider
                .Setup(p => p.GetDailyTaxRecordsAsync(1, It.IsAny<DateOnly>(), It.IsAny<DateOnly>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(QueryResult.Success<IReadOnlyCollection<VehicleDailyTaxDto>>(dtoList));

            var handler = new GetVehicleDailyTaxRecordsQueryHandler(mockProvider.Object);

            var result = await handler.HandleAsync(new GetVehicleDailyTaxRecordsQuery(1, DateOnly.FromDateTime(DateTime.Today.AddDays(-7)), DateOnly.FromDateTime(DateTime.Today)));

            Assert.True(result.IsSuccess);
            Assert.Single(result.Value!);
            Assert.Equal("Tehran", result.Value!.First().City);
        }

        [Fact]
        public async Task DailyTaxRecordsQueryHandler_Throws_WhenQueryIsNull()
        {
            var mockProvider = new Mock<IVehicleTaxReadProvider>();
            var handler = new GetVehicleDailyTaxRecordsQueryHandler(mockProvider.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.HandleAsync(null!));
        }
    }

}