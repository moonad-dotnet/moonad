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
            Assert.True(result.IsOk);
            Assert.Equal(expected, result.Value);
            Assert.Throws<ResultErrorValueException>(() => result.Error);
        }

        [Fact]
        public void IsError()
        {
            //Arrange
            string expected = "Error";

            //Act
            Result<int, string> result = expected;

            Assert.False(result);
            Assert.True(result.IsError);
            Assert.Equal(expected, result.Error);
            Assert.Throws<ResultValueException>(() => result.Value);
        }
    }
}
