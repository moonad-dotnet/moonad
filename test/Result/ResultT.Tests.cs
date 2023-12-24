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
            Assert.Equal<int>(result1.Value, result1);
            Assert.True(result2);
            Assert.False(result2.IsError);
            Assert.Equal<int>(result2.Value, result2);
        }

        [Fact]
        public void Failure()
        {
            //Arrange
            static Result<int> CreateFailure() => Result<int>.Failure();
            
            //Act
            var result = CreateFailure();

            //Assert
            Assert.True(result.IsError);
            Assert.False(result);
            Assert.Throws<ResultValueException>(() => CreateFailure().Value);
        }
    }
}
