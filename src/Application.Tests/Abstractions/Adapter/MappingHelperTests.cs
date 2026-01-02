namespace AHA.CongestionTax.Application.Abstractions.Adapter.Tests
{
    public class MappingHelperTests
    {
        [Fact]
        public void MapEach_Should_Transform_Elements_Correctly()
        {
            // Arrange
            var source = new List<int> { 1, 2, 3, 4, 5 };
            Func<int, string> mapFunc = x => $"Number: {x}";

            // Act
            var result = MappingHelper.MapEach(source, mapFunc).ToList();

            // Assert
            var expected = new List<string>
            {
                "Number: 1",
                "Number: 2",
                "Number: 3",
                "Number: 4",
                "Number: 5"
            };

            Assert.Equal(expected, result);
        }
    }
}