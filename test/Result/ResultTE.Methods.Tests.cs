namespace Moonad.Tests.Results
{
    public class ResultTEMethodsTests
    {
        [Fact]
        public void Bind()
        {
            //Arrange
            static Result<int, string> binder(int x) => Result<int, string>.Ok(x);
            
            int expectedSuccess = 2;
            string expectedError = "Error";

            var success = Result<int, string>.Ok(2);
            var failure = Result<int, string>.Error(expectedError);

            //Assert
            Assert.Equal<int>(expectedSuccess, success.Bind(binder));
            Assert.Equal(expectedError, failure.Bind(binder));
        }

        [Fact]
        public void Contains()
        {
            //Arrange
            string expected = "Ok!";
            
            var successContains = Result<string, string>.Ok(expected);
            var successNotContains = Result<string, string>.Ok("Whatever!");
            var failure = Result<string, string>.Error("Error!");

            //Assert
            Assert.True(successContains.Contains(expected));
            Assert.False(successNotContains.Contains(expected));
            Assert.False(failure.Contains(expected));
        }

        [Fact]
        public void Count()
        {
            //Arrange
            var success = Result<string, string>.Ok("Ok!");
            var failure = Result<string, string>.Error("Error!");

            //Assert
            Assert.Equal(1, success.Count());
            Assert.Equal(0, failure.Count());
        }

        [Fact]
        public void DefaultValue()
        {
            //Arrange
            int defaultValue = 20;
            
            var success = Result<int, string>.Ok(10);
            var failure = Result<int, string>.Error("Error!");

            //Act
            var successValue = success.DefaultValue(defaultValue);
            var failureValue = failure.DefaultValue(defaultValue);

            //Assert
            Assert.Equal<int>(success, successValue);
            Assert.Equal(defaultValue, failureValue);
        }

        [Fact]
        public void DefaultWith()
        { 
            //Arrange
            static int predicate(byte errorCode) => errorCode * 2;
            int expectedSuccess = 10;
            int expectedFailure1 = 40;
            int expectedFailure2 = 60;

            var success = Result<int, byte>.Ok(10);
            var failure1 = Result<int, byte>.Error(20);
            var failure2 = Result<int, byte>.Error(30);

            //Assert
            Assert.Equal(expectedSuccess, success.DefaultWith(predicate));
            Assert.Equal(expectedFailure1, failure1.DefaultWith(predicate));
            Assert.Equal(expectedFailure2, failure2.DefaultWith(predicate));
        }

        [Fact]
        public void Exists()
        {
            //Arrange
            static bool predicate(int x) => x > 0;

            var successExists = Result<int, string>.Ok(10);
            var successNotExists = Result<int, string>.Ok(0);
            var failure = Result<int, string>.Error("Error!");

            //Assert
            Assert.True(successExists.Exists(predicate));
            Assert.False(successNotExists.Exists(predicate));
            Assert.False(failure.Exists(predicate));
        }

        [Fact]
        public void Fold()
        {
            //Arrange
            static int folder(int x, byte y) => x + y;
            
            var success = Result<byte, string>.Ok(10);
            var failure = Result<byte, string>.Error("Error!");

            //Act
            var successFold = success.Fold(folder, 5);
            var failureFold = failure.Fold(folder, 5);

            //Assert
            Assert.Equal(15, successFold);
            Assert.Equal(5, failureFold);
        }

        [Fact]
        public void FoldBack()
        {
            //Arrange
            static string folder(string x, string y) => $"{y}: {x}";

            var state = "Technique";
            var expected = "Technique: Hadouken!";

            var success = Result<string, int>.Ok("Hadouken!");
            var failure = Result<string, int>.Error(0x123456);

            //Act
            var successFold = success.FoldBack(folder, state);
            var failureFold = failure.FoldBack(folder, state);

            //Assert
            Assert.Equal(expected, successFold);
            Assert.Equal(state, failureFold);
        }

        [Fact]
        public void ForAll()
        { 
            //Arrange
            static bool predicate(int x) => x > 0;

            var success = Result<int, string>.Ok(10);
            var successInvalid = Result<int, string>.Ok(0);
            var failure = Result<int, string>.Error("Error!");

            //Assert
            Assert.True(success.ForAll(predicate));
            Assert.False(successInvalid.ForAll(predicate));
            Assert.True(failure.ForAll(predicate));
        }

        [Fact]
        public void Iter() 
        
        {
            //Arrange
            int valueSuccess = 10;
            int expectedSuccess = 20;
            int valueFailure = 30;
            int expectedFailure = 30;

            void actionSuccess(int x) => valueSuccess += x;
            void actionFailure(int x) => valueFailure += x;

            var success = Result<int, string>.Ok(valueSuccess);
            var failure = Result<int, string>.Error("Error!");

            //Act
            success.Iter(actionSuccess);
            failure.Iter(actionFailure);

            //Assert
            Assert.Equal(expectedSuccess, valueSuccess);
            Assert.Equal(expectedFailure, valueFailure);
        }

        [Fact]
        public void Map()
        {
            //Arrange
            string expectedSuccess = "Success!";
            byte expectedFailure = 127;

            Result<string, byte> mapping(int x) => (x > 0) ? expectedSuccess : expectedFailure;

            Result<int, byte> success = 10;
            Result<int, byte> failure = 0;

            //Assert
            Assert.Equal(expectedSuccess, success.Map(mapping));
            Assert.Equal<byte>(expectedFailure, failure.Map(mapping));
        }

        [Fact]
        public void MapError()
        {
            //Arrange
            int expectedSuccess = 0x000;
            string expectedFailure = "Error!";

            Result<int, string> mapping(int x) => Result<int, string>.Error(expectedFailure);

            var success = Result<int, int>.Ok(expectedSuccess);
            var failure = Result<int, int>.Error(0xFFF);

            //Assert
            Assert.Equal<int>(expectedSuccess, success.MapError(mapping));
            Assert.Equal(expectedFailure, failure.MapError(mapping));
        }

        [Fact]
        public void ToArray()
        {
            //Arrange
            var success = Result<string, string>.Ok("Ok!");
            var failure = Result<string, string>.Error("Error!");

            //Act
            var filledList = success.ToArray();
            var emptyList = failure.ToArray();

            //Assert
            Assert.Single(filledList);
            Assert.Empty(emptyList);
        }

        [Fact]
        public void ToList()
        {
            //Arrange
            var success = Result<string, string>.Ok("Ok!");
            var failure = Result<string, string>.Error("Error!");

            //Act
            var filledList = success.ToList();
            var emptyList = failure.ToList();

            //Assert
            Assert.Single(filledList);
            Assert.Empty(emptyList);
        }

        [Fact]
        public void ToOption()
        {
            //Arrange
            var success = Result<int, string>.Ok(1);
            var failure = Result<int, string>.Error("Error!");

            //Act
            var some = success.ToOption();
            var none = failure.ToOption();

            //Assert
            Assert.True(some.IsSome);
            Assert.Equal<int>(some, success);
            Assert.True(none.IsNone);
        }
    }
}
