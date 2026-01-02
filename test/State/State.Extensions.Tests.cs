using Moonad;

namespace Moonad.Tests.State
{
    public class StateExtensionsTests
    {
        [Fact]
        public void Select_ProjectsValue_PreservingState()
        {
            // Arrange
            var st = new State<int, int>(s => (s + 1, s + 2));

            // Act
            var projected = st.Select(x => x * 3);
            var (value, state) = projected.Run(5);

            // Assert
            Assert.Equal((5 + 1) * 3, value);
            Assert.Equal(7, state);
        }

        [Fact]
        public void SelectMany_Binds_ComposesStateCorrectly()
        {
            // Arrange
            var st = new State<int, int>(s => (s + 2, s + 3));
            State<int, string> binder(int x) => new(s => ($"{x}-{s}", s + x));

            // Act
            var bound = st.SelectMany(binder);
            var (value, state) = bound.Run(4);

            // Assert
            // st: x=6, s=7; binder: value="6-7", s=7+6=13
            Assert.Equal("6-7", value);
            Assert.Equal(13, state);
        }

        [Fact]
        public void Where_PassesPredicate_ReturnsSameValueAndState()
        {
            // Arrange
            var st = new State<int, int>(s => (s * 2, s + 1));

            // Act
            var filtered = st.Where(v => v % 2 == 0);
            var (value, state) = filtered.Run(3);

            // Assert
            Assert.Equal(6, value);
            Assert.Equal(4, state);
        }

        [Fact]
        public void Where_FailsPredicate_ThrowsInvalidOperationException()
        {
            // Arrange
            var st = new State<int, int>(s => (s + 1, s));

            // Act & Assert
            var filtered = st.Where(v => v % 2 == 0);
            Assert.Throws<InvalidOperationException>(() => filtered.Run(1));
        }

        [Fact]
        public void Linq_Query_Syntax_WorksWithSelect_SelectMany_Where()
        {
            // Arrange
            var baseState = new State<int, int>(s => (s + 1, s + 2));

            // Act
            var query =
                from a in baseState
                from b in new State<int, int>(s => (s * 2, s + 3))
                where a % 2 == 0
                select $"{a}-{b}";

            var (value, state) = query.Run(3);

            // Assert
            // baseState: a=4, s=5; second: b=10, s=8; where passes; select => "4-10"
            Assert.Equal("4-10", value);
            Assert.Equal(8, state);
        }
    }
}
