namespace Moonad.Tests.Results
{
    public class ResultTEExtensionsTests
    {
        [Fact]
        public void IsSome()
        {
            //Arrange
            int expected = 1;
            Result<int, string> result = expected;

            //Act
            var valueOption = result.ToValueOption();

            //Assert
            Assert.True(valueOption.IsSome);
            Assert.False(valueOption.IsNone);
            Assert.Equal(expected, valueOption.Get());
        }

        [Fact]
        public void IsNone()
        {
            //Arrange
            var result = Result<int, string>.Failure("Error!");

            //Act
            var valueOption = result.ToValueOption();

            //Assert
            Assert.True(valueOption.IsNone);
            Assert.False(valueOption.IsSome);
            Assert.Throws<ValueOptionException>(() => valueOption.Get());
        }
    }
}
