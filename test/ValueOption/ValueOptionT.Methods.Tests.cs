namespace Moonad.Tests.ValueOptions
{
    public class ValueOptionTMethodsTests
    {
        [Theory]
        [InlineData(35, true)]
        [InlineData(null, false)]
        public void Bind(int? input, bool expected)
        {
            //Arrange
            static Option<byte> BindByte(int input) => (byte)input;

            //Act
            Option<int> option = input.ToOption();
            var result = option.Bind(BindByte);

            //Assert
            Assert.Equal(expected, option.IsSome);
        }

        [Theory]
        [InlineData(10, true)]
        [InlineData(0, false)]
        [InlineData(null, false)]
        public void Contains(int? input, bool expect)
        {
            //Arrange
            int value = 10;
            Option<int> option = input.ToOption();

            //Act
            var contains = option.Contains(value);

            //Assert
            Assert.Equal(expect, contains);
        }

        [Theory]
        [InlineData((byte)10, 1)]
        [InlineData(null, 0)]
        public void Count(byte? input, int expected)
        {
            //Arrange
            ValueOption<byte> option = input;

            //Act
            var count = option.Count();

            //Assert
            Assert.Equal(expected, count);
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(null, 0)]
        public void DefaultValue(int? input, int expected)
        {
            //Arrange
            ValueOption<int> option = input;

            //Act
            var result = option.DefaultValue(expected);

            //Arrange
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(null, 99)]
        public void DefaultWith(int? input, int expected)
        {
            //Arrange
            static int DefaultFunc() => 99;
            ValueOption<int> option = input.ToValueOption();

            //Act
            var result = option.DefaultWith(DefaultFunc);

            //Arrange
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((short)1, true)]
        [InlineData((short)0, false)]
        [InlineData(null, false)]
        public void Exists(short? input, bool expected)
        {
            //Arrange
            ValueOption<short> option = input;

            //Act
            var result = option.Exists(s => s > 0);

            //Arrange
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((double)1, true)]
        [InlineData((double)0, false)]
        [InlineData(null, false)]
        public void Filter(double? input, bool expected)
        {
            //Arrange
            ValueOption<double> option = input;

            //Act
            var result = option.Filter(s => s > 0);

            //Arrange
            Assert.Equal(expected, result.IsSome);
        }

        [Theory]
        [InlineData(10, "Fold", "Fold 10")]
        [InlineData(null, "Fold", "Fold")]
        public void Fold(int? input, string state, string expected)
        {
            //Arrange
            static string folder(string state, int value) => $"{state} {value}";
            var option = input.ToValueOption();

            //Act
            var result = option.Fold(folder, state);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, "FoldBack", "10 FoldBack")]
        [InlineData(null, "FoldBack", "FoldBack")]
        public void FoldBack(int? input, string state, string expected)
        {
            //Arrange
            static string folder(int value, string state) => $"{value} {state}";
            var option = input.ToValueOption();

            //Act
            var result = option.FoldBack(folder, state);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, true)]
        [InlineData(1, false)]
        [InlineData(null, true)]
        public void ForAll(int? input, bool expected)
        {
            //Arrange
            ValueOption<int> option = input.ToValueOption();

            //Act
            var result = option.ForAll(i => i > 5);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetFromSome()
        {
            //Act
            ValueOption<int> option = 10;

            //Assert
            Assert.Equal(10, option.Get());
        }

        [Fact]
        public void GetFromNone()
        {
            //Arrange
            byte? nullable = null;

            //Act
            ValueOption<byte> option = nullable;

            //Assert
            Assert.Throws<ValueOptionException>(() => option > 0);
        }

        [Theory]
        [InlineData(10, 110)]
        [InlineData(null, 100)]
        public void Iter(int? input, int expected)
        {
            //Arrange
            int sum = 100;
            ValueOption<int> option = input.ToValueOption();

            //Act
            option.Iter(i => sum += i);

            //Assert
            Assert.Equal(expected, sum);
        }

        [Fact]
        public void MapSome()
        {
            //Arrange
            var expected = 20;
            ValueOption<int> option = 10;
            
            //Act
            var result = option.Map(i => i * 2);

            //Assert
            Assert.True(result.IsSome);
            Assert.Equal(expected, result.Get());
        }

        [Fact]
        public void MapNone()
        {
            //Arrange
            var option = ValueOption<int>.None();

            //Act
            var result = option.Map(i => i ^ 2);

            //Assert
            Assert.True(result.IsNone);
        }

        [Fact]
        public void Map2Some()
        {
            //Arrange
            ValueOption<int> option = 10;
            ValueOption<int> option2 = 10;

            //Act
            var result = option.Map2(option2, (x, y) => x * y);

            //Assert
            Assert.True(result.IsSome);
            Assert.Equal(100, result.Get());
        }

        [Theory]
        [InlineData(10, null)]
        [InlineData(null, 10)]
        [InlineData(null, null)]
        public void Map2None(int? input1, int? input2)
        {
            //Arrange
            var option1 = input1.ToValueOption();
            var option2 = input2.ToValueOption();

            //Act
            var result = option1.Map2(option2, (x, y) => x * y);

            //Asset
            Assert.True(result.IsNone);
        }

        [Theory]
        [InlineData(3, 13)]
        [InlineData(null, -1)]
        public void MatchByAction(int? input, int expected)
        {
            //Arrange
            int value = 10;

            //Act
            var option = input.ToValueOption();
            option.Match(v => value += v, () => { value = -1; });

            //Assert
            Assert.Equal(expected, value);
        }

        [Theory]
        [InlineData(3, 13)]
        [InlineData(null, -1)]
        public void MatchByFunc(int? input, int expected)
        {
            //Arrange
            int value = 10;

            //Act
            var option = input.ToValueOption();
            var result = option.Match((v) => value + v, () => -1);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 20, false)]
        [InlineData(null, 30, true)]
        public void OrElseSome(int? input, int ifNoneValue, bool expected)
        {
            //Arrange
            var option = input.ToValueOption();
            ValueOption<int> ifNone = ifNoneValue;

            //Act
            var result = option.OrElse(ifNone);

            //Assert
            Assert.True(result.IsSome);
            Assert.Equal(expected, result == ifNone);
        }

        [Fact]
        public void OrElseNone()
        {
            //Arrange
            var option = ValueOption<int>.None();
            var ifNone = ValueOption<int>.None();

            //Act
            var result = option.OrElse(ifNone);

            //Assert
            Assert.True(result.IsNone);
        }

        [Theory]
        [InlineData(10, null, 10)]
        [InlineData(null, 20, 20)]
        public void OrElseWithSome(int? input, int? ifNoneValue, int expected)
        {
            //Arrange
            var option = input.ToValueOption();
            var ifNone = ifNoneValue.ToValueOption();

            //Act
            var result = option.OrElseWith(() => ifNone);

            //Assert
            Assert.True(result.IsSome);
            Assert.Equal(expected, result.Get());
        }

        [Fact]
        public void OrElseWithNone()
        {
            //Arrange
            int? optionValue = null;
            var option = optionValue.ToValueOption();
            
            //Act
            var result = option.OrElseWith(() => ValueOption<int>.None());

            //Assert
            Assert.True(result.IsNone);
        }

        [Theory]
        [InlineData(10, 1)]
        [InlineData(null, 0)]
        public void ToArray(int? input, int expected)
        {
            //Arrage
            var option = input.ToValueOption();

            //Act
            var result = option.ToArray();

            //Assert
            Assert.Equal(expected, result.Length);
        }

        [Theory]
        [InlineData(10, 1)]
        [InlineData(null, 0)]
        public void ToList(int? input, int expected)
        {
            //Arrage
            var option = input.ToValueOption();

            //Act
            var result = option.ToList();

            //Assert
            Assert.Equal(expected, result.Count);
        }
    }
}
