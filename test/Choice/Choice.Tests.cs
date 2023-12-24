using System.Xml.Serialization;

namespace Moonad.Tests.Choices
{
    public class ChoiceTests
    {
        [Fact]
        public void ImplictT1Conversion()
        {
            //Arrange
            var expected = 123;
            
            //Act
            Choice<int, string> choice = expected;

            //Assert
            Assert.True(expected == choice);
        }

        [Fact]
        public void ImplictT2Conversion()
        {
            //Arrange
            var expected = "Test";

            //Act
            Choice<int, string> choice = expected;

            //Assert
            Assert.Equal(expected, choice);
        }

        [Fact]
        public void CorrespondingChoice()
        {
            //Arrange
            var value1 = 123;
            var value2 = 456;

            Choice<int, string> choice1 = value1;
            Choice<string, int> choice2 = value2;

            //Assert
            Assert.Equal(value1, choice1.Choice1Of2);
            Assert.Equal(value2, choice2.Choice2Of2);
        }

        [Fact]
        public void CorrespondingString()
        {
            //Arrange
            var expected1 = "Choice1 123";
            var expected2 = "Choice2 456";

            //Act
            Choice<int, string> choice1 = 123;
            Choice<string, int> choice2 = 456;

            //Assert
            Assert.Equal(expected1, choice1.ToString());
            Assert.Equal(expected2, choice2.ToString());
        }

        [Fact]
        public void DecidingChoice()
        {
            //Arrange
            var asInt = 1;
            var asString = "This is a Choice!";

            Choice<int, string> Choose(bool returnInt) =>
                returnInt ? asInt : asString;

            //Act
            Choice<int, string> choice1 = Choose(true);
            Choice<int, string> choice2 = Choose(false);

            //Assert
            Assert.IsType<int>(choice1.Choice1Of2);
            Assert.Equal<int>(asInt, choice1);
            Assert.IsType<string>(choice2.Choice2Of2);
            Assert.Equal(asString, choice2);
        }

        [Fact]
        public void InvalidChoice()
        {
            //Arrange
            Choice<int, string> choice1 = 1;
            Choice<int, string> choice2 = "2";

            //Assert
            Assert.Throws<InvalidCastException>(() => choice1.Choice2Of2);
            Assert.Throws<InvalidCastException>(() => choice2.Choice1Of2);
        }
    }
}
