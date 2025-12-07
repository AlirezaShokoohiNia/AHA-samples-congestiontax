namespace AHA.CongestionTax.Application.Queries.Tests
{
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Abstractions.Query;
    using AHA.CongestionTax.Application.Abstractions.Query.Providers;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Application.Queries;
    using Moq;
    using Xunit;

    public class GetVehicleQueryHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ReturnsSuccess_WhenVehicleFound()
        {
            // Arrange
            var dto = new VehicleDto { VehicleId = 1, LicensePlate = "ABC123", VehicleType = "Car" };
            var mockProvider = new Mock<IVehicleReadProvider>();
            mockProvider
                .Setup(p => p.GetVehicleAsync("ABC123", It.IsAny<CancellationToken>()))
                .ReturnsAsync(QueryResult.Success(dto));

            var handler = new GetVehicleQueryHandler(mockProvider.Object);

            // Act
            var result = await handler.HandleAsync(new GetVehicleQuery("ABC123"));

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("ABC123", result.Value!.LicensePlate);
            mockProvider.Verify(p => p.GetVehicleAsync("ABC123", It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_ReturnsNotFound_WhenVehicleMissing()
        {
            // Arrange
            var mockProvider = new Mock<IVehicleReadProvider>();
            mockProvider
                .Setup(p => p.GetVehicleAsync("MISSING", It.IsAny<CancellationToken>()))
                .ReturnsAsync(QueryResult.Failure<VehicleDto>("Not Found"));

            var handler = new GetVehicleQueryHandler(mockProvider.Object);

            // Act
            var result = await handler.HandleAsync(new GetVehicleQuery("MISSING"));

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Not Found", result.Error);
            Assert.Null(result.Value);
            mockProvider.Verify(p => p.GetVehicleAsync("MISSING", It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_PropagatesFailure()
        {
            // Arrange
            var mockProvider = new Mock<IVehicleReadProvider>();
            mockProvider
                .Setup(p => p.GetVehicleAsync("ERR", It.IsAny<CancellationToken>()))
                .ReturnsAsync(QueryResult.Failure<VehicleDto>("source-error"));

            var handler = new GetVehicleQueryHandler(mockProvider.Object);

            // Act
            var result = await handler.HandleAsync(new GetVehicleQuery("ERR"));

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("source-error", result.Error);
            mockProvider.Verify(p => p.GetVehicleAsync("ERR", It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_PassesCancellationToken_ToProvider()
        {
            // Arrange
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var mockProvider = new Mock<IVehicleReadProvider>();
            mockProvider
                .Setup(p => p.GetVehicleAsync("ABC123", token))
                .ReturnsAsync(QueryResult.Failure<VehicleDto>("Not Found"));

            var handler = new GetVehicleQueryHandler(mockProvider.Object);

            // Act
            await handler.HandleAsync(new GetVehicleQuery("ABC123"), token);

            // Assert
            mockProvider.Verify(p => p.GetVehicleAsync("ABC123", token), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_Throws_WhenQueryIsNull()
        {
            var mockProvider = new Mock<IVehicleReadProvider>();
            var handler = new GetVehicleQueryHandler(mockProvider.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                handler.HandleAsync(null!));
        }
    }

}