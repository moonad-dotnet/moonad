namespace Moonad.Tests.ValueOptions
{
    public class ValueOptionTEquatableTests
    {
        [Theory]
        [InlineData(10, 10, true)]
        [InlineData(20, 0, false)]
        [InlineData(null, null, true)]
        [InlineData(null, 0, false)]
        public void CompareEqualsSuccessfully(int? left, int? right, bool expected)
        {
            //Act
            var leftToCompare = left.ToValueOption();
            var rightToCompare = right.ToValueOption();

            //Assert
            Assert.Equal(expected, leftToCompare == rightToCompare);
        }
    }
}
