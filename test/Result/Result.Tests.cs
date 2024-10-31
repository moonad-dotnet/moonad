namespace Moonad.Tests.Results
{
    public class ResultTests
    {
        [Fact]
        public void Success() 
        {
            //Arrange
            var result1 = Result.Ok();
            Result result2 = true;

            //Assert
            Assert.True(result1);
            Assert.False(result1.IsError);
            Assert.True(result2);
            Assert.False(result2.IsError);
        }

        [Fact]
        public void Failure()
        {
            //Arrange
            var result1 = Result.Error();
            Result result2 = false;

            //Assert
            Assert.False(result1);
            Assert.True(result1.IsError);
            Assert.False(result2);
            Assert.True(result2.IsError);
        }

        [Fact]
        public async Task SuccessWithTask()
        {
            // Arrange
            Result result1 = Result.Ok();
            Task<Result> taskResult() => Task.FromResult(result1);

            // Act
            var result2 = await taskResult();

            //Assert
            Assert.True(result2);
            Assert.False(result2.IsError);
        }

        [Fact]
        public async Task FailureWithTask()
        {
            // Arrange
            Result result1 = Result.Error();
            Task<Result> taskResult() => Task.FromResult(result1);

            // Act
            var result2 = await taskResult();

            //Assert
            Assert.False(result2);
            Assert.True(result2.IsError);
        }
    }
}
