namespace Moonad.Tests.Options
{
    public class OptionExtensionsTests
    {
        [Fact]
        public void FlattenSome()
        {
            //Arrange
            Option<Option<int>> option = Option.Some(10);

            //Act
            var result = option.Flatten();

            //Assert
            Assert.Equal(10, result.Get());
        }

        [Fact]
        public void FlattenNone()
        {
            //Arrange
            Option<Option<string>> option = Option.None<string>();

            //Act
            var result = option.Flatten();

            //Assert
            Assert.True(result.IsNone);
        }

        [Fact]
        public void ToOptionReferenceType()
        {
            //Arrange
            int? input1 = 10;
            int? input2 = null;

            //Act
            Option<int> option1 = input1.ToOption();
            Option<int> option2 = input2.ToOption();

            //Assert
            Assert.True(option1.IsSome);
            Assert.True(option2.IsNone);
        }

        [Fact]
        public void ToOptionValueType()
        {
            //Arrange
            int? input1 = 10;
            int? input2 = null;

            //Act
            Option<int> option1 = input1.ToOption();
            Option<int> option2 = input2.ToOption();

            //Assert
            Assert.True(option1.IsSome);
            Assert.True(option2.IsNone);
        }
    }
}
