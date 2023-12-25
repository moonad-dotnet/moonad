namespace Moonad.Tests.Options
{
    public class OptionTTests
    {
        [Fact]
        public void ConvertNullableRefToSome()
        {
            //Arrange
            string? nullableString = "Test";

            //Act
            Option<string> option = nullableString;

            //Assert
            Assert.True(option.IsSome);
            Assert.Equal(nullableString, option.Get());
        }

        [Fact]
        public void ConvertNullableRefNullToNone()
        {
            //Arrange
            string? nullableString = null;

            //Act
            Option<string> option = nullableString;

            //Assert
            Assert.True(option.IsNone);
        }

        [Fact]
        public void ConvertNullableValNullToNone()
        {
            //Arrange
            static int? GetValueAsNull() => null;

            //Act
            Option<int> option = GetValueAsNull().ToOption();

            //Assert
            Assert.True(option.IsNone);
        }

        [Fact]
        public void ConvertNullableValToSome()
        {
            //Arrange
            int? nullableInt = 1;

            //Act
            Option<int> option = nullableInt;

            //Assert
            Assert.True(option.IsSome);
            Assert.Equal(nullableInt, option.Get());
        }

        [Theory]
        [InlineData("Test", true)]
        [InlineData(null, false)]
        public void ConvertNullableRefParam(string? input, bool expected)
        {
            //Act
            Option<string> option = input;

            //Assert
            Assert.Equal(expected, option.IsSome);
        }

        [Theory]
        [InlineData(10, true)]
        [InlineData(null, false)]
        public void ConvertNullableValParam(int? input, bool expected)
        {
            //Act
            Option<int> option = input.ToOption();

            //Assert
            Assert.Equal(expected, option.IsSome);
        }

        [Theory]
        [InlineData("Test", "Some Test")]
        [InlineData(null, "None")]
        public void OptionToString(string? input, string expected)
        {
            //Arrange
            Option<string> option = input;

            //Act
            var result = option.ToString();

            //Assert
            Assert.Equal(expected, result);
        }
    }
}