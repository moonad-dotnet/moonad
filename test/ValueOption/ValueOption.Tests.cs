namespace Moonad.Tests.ValueOptions
{
    public class ValueOptionTests
    {
        [Fact]
        public void Some()
        {
            //Arrange
            int value = 10;

            //Act
            var option = ValueOption<int>.Some(value);

            //Assert
            Assert.True(option.IsSome);
            Assert.Equal(value, option.Get());
        }

        [Fact]
        public void None()
        {
            //Act
            var option = ValueOption<int>.None();

            //Assert
            Assert.True(option.IsNone);
            Assert.Throws<ValueOptionException>(() => option.Get());
        }
    }
}
