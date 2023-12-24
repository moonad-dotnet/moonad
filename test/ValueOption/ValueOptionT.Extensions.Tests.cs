namespace Moonad.Tests.ValueOptions
{
    public class ValueOptionExtensionsTests
    {
        [Fact]
        public void FlattenSome()
        {
            //Arrange
            ValueOption<ValueOption<int>> option = ValueOption<int>.Some(10);

            //Act
            var result = option.Flatten();

            //Assert
            Assert.Equal(10, result.Get());
        }

        [Fact]
        public void FlattenNone()
        {
            //Arrange
            ValueOption<ValueOption<byte>> option = ValueOption<byte>.None();

            //Act
            var result = option.Flatten();

            //Assert
            Assert.True(result.IsNone);
        }

        [Fact]
        public void ToOption()
        {
            //Arrange
            int? input1 = 10;
            int? input2 = null;

            //Act
            var option1 = input1.ToValueOption();
            var option2 = input2.ToValueOption();

            //Assert
            Assert.True(option1.IsSome);
            Assert.True(option2.IsNone);
        }
    }
}
