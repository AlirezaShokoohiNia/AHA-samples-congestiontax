namespace AHA.CongestionTax.Domain.DayTollAgg.Tests
{
    public class PassRecordTests
    {
        [Fact]
        public void PassRecord_ShouldStoreTime()
        {
            //Arrange

            //Act
            var pass = new PassRecord(new TimeOnly(9, 15));

            //Assert
            Assert.Equal(new TimeOnly(9, 15), pass.Time);
        }
    }

}