namespace AHA.CongestionTax.Application.Queries.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Abstractions.Query;
    using AHA.CongestionTax.Application.Abstractions.Query.Providers;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Application.Queries;
    using Moq;
    using Xunit;

    public class VehicleTaxQueryHandlerTests
    {
        [Fact]
        public async Task WeeklyTotalTaxHandler_ReturnsSuccess()
        {
            var dto = new VehicleTotalTaxDto { LicensePlate = "AAA", TotalTax = 100 };
            var mockProvider = new Mock<IVehicleTaxReadProvider>();
            mockProvider
                .Setup(p => p.GetWeeklyTotalTaxAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(QueryResult.Success(dto));

            var handler = new GetVehicleWeeklyTotalTaxQueryHandler(mockProvider.Object);

            var result = await handler.HandleAsync(new GetVehicleWeeklyTotalTaxQuery(1));

            Assert.True(result.IsSuccess);
            Assert.Equal(100, result.Value!.TotalTax);
        }

        [Fact]
        public async Task WeeklyTotalTaxHandler_Throws_WhenQueryIsNull()
        {
            var mockProvider = new Mock<IVehicleTaxReadProvider>();
            var handler = new GetVehicleWeeklyTotalTaxQueryHandler(mockProvider.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.HandleAsync(null!));
        }

    }

}