namespace Moonad.Tests.Results
{
    public class ResultTests
    {
        [Fact]
        public void Success() 
        {
            //Arrange
            var result1 = Result.Sucess();
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
            var result1 = Result.Failure();
            Result result2 = false;

            //Assert
            Assert.False(result1);
            Assert.True(result1.IsError);
            Assert.False(result2);
            Assert.True(result2.IsError);
        }
    }
}
