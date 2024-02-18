namespace Moonad.Tests.Results
{
    public class ResultTETests
    {
        [Fact]
        public void IsOk()
        {
            //Arrange
            int expected = 1;

            //Act
            Result<int, string> result = expected;

            Assert.True(result);
            Assert.Equal<int>(expected, result);
            Assert.Throws<ErrorValueException>(() => result.ErrorValue);
        }

        [Fact]
        public void IsError()
        {
            //Arrange
            string expected = "Error";

            //Act
            Result<int, string> result = expected;

            Assert.False(result);
            Assert.Equal(expected, result);
            Assert.Throws<ResultValueException>(() => result.ResultValue);
        }
    }
}
