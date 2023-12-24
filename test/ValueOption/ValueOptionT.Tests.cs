namespace Moonad.Tests.ValueOptions
{
    public class ValueOptionTTests
    {
        [Fact]
        public void ConvertNullableValNullToNone()
        {
            //Arrange
            int? nullableInt = null;

            //Act
            ValueOption<int> option = nullableInt;

            //Assert
            Assert.True(option.IsNone);
        }

        [Fact]
        public void ConvertNullableValToSome()
        {
            //Arrange
            int? nullableInt = 1;

            //Act
            ValueOption<int> option = nullableInt;

            //Assert
            Assert.True(option.IsSome);
            Assert.Equal(nullableInt, option.Get());
        }

        [Theory]
        [InlineData(10, true)]
        [InlineData(null, false)]
        public void ConvertNullableValParam(int? input, bool expected)
        {
            //Act
            var option = input.ToValueOption();

            //Assert
            Assert.Equal(expected, option.IsSome);
        }

        [Theory]
        [InlineData((byte)10, "Some 10")]
        [InlineData(null, "None")]
        public void ValueOptionToString(byte? input, string expected)
        {
            //Arrange
            ValueOption<byte> option = input;

            //Act
            var result = option.ToString();

            //Assert
            Assert.Equal(expected, result);
        }
    }
}