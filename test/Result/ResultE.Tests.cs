namespace Moonad.Tests.Results
{
    public class ResultETests
    {
        [Fact]
        public void IsOk()
        {
            //Act
            var result = Result<string>.Ok();

            //Assert
            Assert.True(result);
            Assert.Throws<ErrorValueException>(() => result.ErrorValue);
        }

        [Fact]
        public void ImplicitOk()
        {
            //Act
            Result<IError> result = true;

            //Assert
            Assert.True(result);
            Assert.True(result.IsOk);
        }

        [Fact]
        public void IsError()
        {
            //Act
            Result<IError> result = new DummyError();

            //Assert
            Assert.False(result);
            Assert.IsType<DummyError>(result.ErrorValue);
        }

        [Fact]
        public void ImplicitError()
        {
            //Arrange
            static void action() { Result<IError> result = false; }

            //Assert
            Assert.Throws<ErrorValueException>(action);
        }
    }

    public interface IError { }
    public readonly struct DummyError : IError { }
}
