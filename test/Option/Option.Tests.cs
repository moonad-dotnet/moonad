namespace Moonad.Tests.Options
{ 
    public class OptionTests
    {
        [Fact]
        public void Some()
        {
            //Arrange
            int value = 10;

            //Act
            var option = Option.Some(value);

            //Assert
            Assert.True(option.IsSome);
            Assert.Equal(value, option.Get());
        }

        [Fact]
        public void None()
        {
            //Act
            var option = Option.None<int>();

            //Assert
            Assert.True(option.IsNone);
            Assert.Throws<OptionException>(() => option.Get());
        }
    }
}
