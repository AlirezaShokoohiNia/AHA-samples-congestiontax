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

    public class GetRuleSetQueryHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ReturnsSuccess_WhenRuleSetFound()
        {
            // Arrange
            var dto = new RuleSetDto { City = "Tehran" };
            var mockProvider = new Mock<IRuleSetReadProvider>();
            mockProvider
                .Setup(p => p.GetRulesForCityAsync("Tehran", It.IsAny<CancellationToken>()))
                .ReturnsAsync(QueryResult.Success(dto));

            var handler = new GetRuleSetQueryHandler(mockProvider.Object);

            // Act
            var result = await handler.HandleAsync(new GetRuleSetQuery("Tehran"));

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Tehran", result.Value!.City);
            mockProvider.Verify(p => p.GetRulesForCityAsync("Tehran", It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_ReturnsNotFound_WhenRuleSetMissing()
        {
            // Arrange
            var mockProvider = new Mock<IRuleSetReadProvider>();
            mockProvider
                .Setup(p => p.GetRulesForCityAsync("MISSING", It.IsAny<CancellationToken>()))
                .ReturnsAsync(QueryResult.Failure<RuleSetDto>("Not Found"));

            var handler = new GetRuleSetQueryHandler(mockProvider.Object);

            // Act
            var result = await handler.HandleAsync(new GetRuleSetQuery("MISSING"));

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Not Found", result.Error);
            Assert.Null(result.Value);
            mockProvider.Verify(p => p.GetRulesForCityAsync("MISSING", It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_PropagatesFailure()
        {
            // Arrange
            var mockProvider = new Mock<IRuleSetReadProvider>();
            mockProvider
                .Setup(p => p.GetRulesForCityAsync("ERR", It.IsAny<CancellationToken>()))
                .ReturnsAsync(QueryResult.Failure<RuleSetDto>("source-error"));

            var handler = new GetRuleSetQueryHandler(mockProvider.Object);

            // Act
            var result = await handler.HandleAsync(new GetRuleSetQuery("ERR"));

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("source-error", result.Error);
            mockProvider.Verify(p => p.GetRulesForCityAsync("ERR", It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_PassesCancellationToken_ToProvider()
        {
            // Arrange
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var mockProvider = new Mock<IRuleSetReadProvider>();
            mockProvider
                .Setup(p => p.GetRulesForCityAsync("Tehran", token))
                .ReturnsAsync(QueryResult.Failure<RuleSetDto>("Not Found"));

            var handler = new GetRuleSetQueryHandler(mockProvider.Object);

            // Act
            await handler.HandleAsync(new GetRuleSetQuery("Tehran"), token);

            // Assert
            mockProvider.Verify(p => p.GetRulesForCityAsync("Tehran", token), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_Throws_WhenQueryIsNull()
        {
            var mockProvider = new Mock<IRuleSetReadProvider>();
            var handler = new GetRuleSetQueryHandler(mockProvider.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                handler.HandleAsync(null!));
        }

    }

}
