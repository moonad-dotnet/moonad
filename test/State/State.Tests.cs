namespace Moonad.Tests.State
{
    using System;
    using Xunit;
    using Moonad;

    namespace Moonad.Tests.State
    {
        public class StateMethodsTests
        {
            [Fact]
            public void ImplicitConversion_FromValue_ProducesStateReturningSameState()
            {
                // Arrange
                var initialState = 42;
                var value = "hello";

                // Act
                State<int, string> state = value;
                var (result, stateOut) = state.Function(initialState);

                // Assert
                Assert.Equal(value, result);
                Assert.Equal(initialState, stateOut);
            }

            [Fact]
            public void Constructor_AssignsFunction_ThatTransformsStateAndValue()
            {
                // Arrange
                var initialState = 10;
                var expectedValue = 99;
                Func<int, (int, int)> func = s => (expectedValue, s + 1);

                // Act
                var state = new State<int, int>(func);
                var (value, stateOut) = state.Function(initialState);

                // Assert
                Assert.Equal(expectedValue, value);
                Assert.Equal(initialState + 1, stateOut);
            }

            [Fact]
            public void Function_CanBeInvokedMultipleTimes_IsPureGivenSameInput()
            {
                // Arrange
                var initialState = 5;
                Func<int, (int, int)> func = s => (s * 2, s + 3);
                var state = new State<int, int>(func);

                // Act
                var (v1, s1) = state.Function(initialState);
                var (v2, s2) = state.Function(initialState);

                // Assert
                Assert.Equal(v1, v2);
                Assert.Equal(s1, s2);
                Assert.Equal(initialState * 2, v1);
                Assert.Equal(initialState + 3, s1);
            }

            [Fact]
            public void Function_CanHandleDifferentStates_Independently()
            {
                // Arrange
                Func<int, (string, int)> func = s => ($"state:{s}", s - 1);
                var state = new State<int, string>(func);

                // Act
                var (v1, s1) = state.Function(0);
                var (v2, s2) = state.Function(7);

                // Assert
                Assert.Equal("state:0", v1);
                Assert.Equal(-1, s1);

                Assert.Equal("state:7", v2);
                Assert.Equal(6, s2);
            }

            [Fact]
            public void ImplicitConversion_WorksForValueTypes()
            {
                // Arrange
                var initialState = -3;
                var value = 123;

                // Act
                State<int, int> state = value;
                var (result, stateOut) = state.Function(initialState);

                // Assert
                Assert.Equal(value, result);
                Assert.Equal(initialState, stateOut);
            }

            [Fact]
            public void Function_ReturnsTuple_WithNamedMembersValueAndState()
            {
                // Arrange
                var initialState = 1;
                var state = new State<int, int>(s => (s + 5, s * 2));

                // Act
                var result = state.Function(initialState);

                // Assert
                Assert.Equal(initialState + 5, result.Value);
                Assert.Equal(initialState * 2, result.State);
            }
        }
    }
}
