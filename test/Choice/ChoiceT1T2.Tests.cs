namespace Moonad.Tests.Choices
{
    public class ChoiceT1T2Tests
    {
        [Fact]
        public void ImplictT1Conversion()
        {
            //Arrange
            var expected = 123;
            
            //Act
            Choice<int, string> choice = expected;

            //Assert
            Assert.IsType<Choice<int>>(choice.Chosen);
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
            Assert.Equal<int>(value1, choice1);
            Assert.Equal<int>(value2, choice2);
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
            Assert.IsType<Choice<int>>(choice1.Chosen);
            Assert.Equal<int>(asInt, choice1);
            Assert.IsType<Choice<string>>(choice2.Chosen);
            Assert.Equal(asString, choice2);
        }
    }
}
