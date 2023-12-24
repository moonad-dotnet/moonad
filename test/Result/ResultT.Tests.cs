namespace Moonad.Tests.Results
{
    public class ResultTTests
    {
        [Fact]
        public void Success()
        {
            //Arrange
            var result1 = Result<int>.Sucess(10);
            Result<int> result2 = 20;

            //Assert
            Assert.True(result1);
            Assert.False(result1.IsError);
            Assert.Equal(result1.Value, result1);
            Assert.True(result2);
            Assert.False(result2.IsError);
            Assert.Equal(result2.Value, result2);
        }

        [Fact]
        public void Failure()
        {
            //Arrange
            var result = Result<int>.Failure();

            //Assert
            Assert.False(result);
            Assert.True(result.IsError);
            Assert.Throws<ResultValueException>(() => result.Value);
        }
    }
}
