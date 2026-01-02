
namespace Moonad.Tests.State
{
    public class StateMethodsTests
    {
        [Fact]
        public void Run_ReturnsValueAndNewState()
        {
            // Arrange
            var st = new State<int, string>(s => ($"v{s}", s + 1));

            // Act
            var (value, state) = st.Run(10);

            // Assert
            Assert.Equal("v10", value);
            Assert.Equal(11, state);
        }

        [Fact]
        public void Eval_ReturnsValueOnly()
        {
            // Arrange
            var st = new State<int, int>(s => (s * 2, s + 3));

            // Act
            var v = st.Eval(5);

            // Assert
            Assert.Equal(10, v);
        }

        [Fact]
        public void Exec_ReturnsStateOnly()
        {
            // Arrange
            var st = new State<int, int>(s => (s * 2, s + 3));

            // Act
            var sOut = st.Exec(5);

            // Assert
            Assert.Equal(8, sOut);
        }

        [Fact]
        public void Apply_AppliesFunctionInsideState()
        {
            // Arrange
            var sf = new State<int, Func<int, int>>(s => (x => x + s, s + 1));
            var xv = new State<int, int>(s => (s * 2, s + 1));

            // Act
            var res = xv.Apply(sf);
            var (value, state) = res.Run(3);

            // Assert
            // sf: f = x => x + 3, s->4; xv: x=4*2=8, s->5; f(x)=8+3=11, final state=5
            Assert.Equal(11, value);
            Assert.Equal(5, state);
        }

        [Fact]
        public void Bind_ChainsComputations_ThreadingState()
        {
            // Arrange
            var a = new State<int, int>(s => (s + 1, s + 2));
            State<int, int> next(int v) => new State<int, int>(s => (v * 3, s + v));

            // Act
            var bound = a.Bind(next, a);
            var (value, state) = bound.Run(1);

            // Assert
            // a: v=2, s=3; next: value=2*3=6, s=3+2=5
            Assert.Equal(6, value);
            Assert.Equal(5, state);
        }

        [Fact]
        public void Delay_DefersExecution()
        {
            // Arrange
            var executed = false;
            State<int, int> body() => new State<int, int>(s =>
            {
                executed = true;
                return (s + 10, s + 1);
            });
            var delayed = new State<int, int>(s => (s, s)).Delay(body);

            // Act
            var (value, state) = delayed.Run(0);

            // Assert
            Assert.True(executed);
            Assert.Equal(10, value);
            Assert.Equal(1, state);
        }

        [Fact]
        public void Get_ReturnsCurrentState()
        {
            // Arrange
            var st = new State<int, int>(s => (s, s));

            // Act
            var (value, state) = st.Get().Run(7);

            // Assert
            Assert.Equal(7, value);
            Assert.Equal(7, state);
        }

        [Fact]
        public void Gets_MapsStateToValueWithoutChangingState()
        {
            // Arrange
            var st = new State<int, int>(s => (s, s));

            // Act
            var (value, state) = st.Gets(s => s * 2).Run(4);

            // Assert
            Assert.Equal(8, value);
            Assert.Equal(4, state);
        }

        [Fact]
        public void Kleisli_ComposesTwoFunctions()
        {
            // Arrange
            State<int, int> f(int t) => new State<int, int>(s => (t + s, s + 1));
            State<int, string> g(int u) => new State<int, string>(s => ($"{u}-{s}", s + u));
            var k = new State<int, int>(s => (s, s)).Kleisli(f, g);

            // Act
            var res = k(5);
            var (value, state) = res.Run(3);

            // Assert
            // f(5): (10,4); g(10): ("10-4", 4+10=14)
            Assert.Equal("10-4", value);
            Assert.Equal(14, state);
        }

        [Fact]
        public void Lift_MapsValue_PreservingState()
        {
            // Arrange
            var st = new State<int, int>(s => (s + 2, s + 3));

            // Act
            var lifted = st.Lift(x => x * 2);
            var (value, state) = lifted.Run(1);

            // Assert
            Assert.Equal((1 + 2) * 2, value);
            Assert.Equal(4, state);
        }

        [Fact]
        public void Map_MapsValue_PreservingState()
        {
            // Arrange
            var st = new State<int, int>(s => (s + 2, s + 3));

            // Act
            var mapped = st.Map(x => x - 1, st);
            var (value, state) = mapped.Run(2);

            // Assert
            Assert.Equal((2 + 2) - 1, value);
            Assert.Equal(5, state);
        }

        [Fact]
        public void Lift2_CombinesTwoStates()
        {
            // Arrange
            var x = new State<int, int>(s => (s + 1, s + 2));
            var y = new State<int, int>(s => (s * 2, s + 3));

            // Act
            var res = x.Lift2((a, b) => a + b, x, y);
            var (value, state) = res.Run(2);

            // Assert
            // x: a=3, s=4; y: b=4*2=8, s=7; value=11, state=7
            Assert.Equal(11, value);
            Assert.Equal(7, state);
        }

        [Fact]
        public void Map2_CombinesTwoStates()
        {
            // Arrange
            var x = new State<int, int>(s => (s + 1, s + 2));
            var y = new State<int, int>(s => (s * 2, s + 3));

            // Act
            var res = x.Map2((a, b) => a * b, x, y);
            var (value, state) = res.Run(3);

            // Assert
            // x: a=4, s=5; y: b=10, s=8; value=40, state=8
            Assert.Equal(40, value);
            Assert.Equal(8, state);
        }

        [Fact]
        public void Map3_CombinesThreeStates()
        {
            // Arrange
            var x = new State<int, int>(s => (s + 1, s + 2));
            var y = new State<int, int>(s => (s * 2, s + 3));
            var z = new State<int, int>(s => (s - 1, s + 4));

            // Act
            var res = x.Map3((a, b, c) => a + b + c, x, y, z);
            var (value, state) = res.Run(2);

            // Assert
            // x: a=3, s=4; y: b=8, s=7; z: c=6, s=11; value=17, state=11
            Assert.Equal(17, value);
            Assert.Equal(11, state);
        }

        [Fact]
        public void Modify_TransformsState_ReturnsUnit()
        {
            // Arrange
            var st = new State<int, int>(s => (s, s));

            // Act
            var res = st.Modify(s => s + 5);
            var (value, state) = res.Run(10);

            // Assert
            Assert.Equal(Unit.Value, value);
            Assert.Equal(15, state);
        }

        [Fact]
        public void Put_SetsNewState_ReturnsUnit()
        {
            // Arrange
            var st = new State<int, int>(s => (s, s));

            // Act
            var res = st.Put(99);
            var (value, state) = res.Run(10);

            // Assert
            Assert.Equal(Unit.Value, value);
            Assert.Equal(99, state);
        }

        [Fact]
        public void SequenceRight_RunsSecondReturningItsResult()
        {
            // Arrange
            var x = new State<int, int>(s => (s, s + 1));
            var y = new State<int, int>(s => (s * 3, s + 2));

            // Act
            var seq = y.SequenceRight(x, y);
            var (value, state) = seq.Run(2);

            // Assert
            Assert.Equal(9, value);
            Assert.Equal(5, state);
        }

        [Fact]
        public void SequenceLeft_RunsBoth_ReturnsFirstValue_FinalStateFromSecond()
        {
            // Arrange
            var x = new State<int, string>(s => ($"x{s}", s + 1));
            var y = new State<int, int>(s => (s * 3, s + 2));

            // Act
            var seq = x.SequenceLeft(x, y);
            var (value, state) = seq.Run(2);

            // Assert
            // x: ("x2",3); y: (9,5) => ("x2",5)
            Assert.Equal("x2", value);
            Assert.Equal(5, state);
        }

        [Fact]
        public void Throw_RethrowsExceptionOnRun()
        {
            // Arrange
            var ex = new InvalidOperationException("boom");
            var st = new State<int, int>(s => (s, s)).Throw(ex);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => st.Run(0));
        }

        [Fact]
        public void TryFinally_RunsCompensation()
        {
            // Arrange
            var called = false;
            var body = new State<int, int>(s => (s + 1, s + 2));
            var wrapped = body.TryFinally(body, () => called = true);

            // Act
            var (value, state) = wrapped.Run(1);

            // Assert
            Assert.True(called);
            Assert.Equal(2, value);
            Assert.Equal(3, state);
        }

        [Fact]
        public void TryWith_CatchesAndHandlesException()
        {
            // Arrange
            var exBody = new State<int, int>(_ => throw new InvalidOperationException("boom"));
            var handled = exBody.TryWith(exBody, _ => new State<int, int>(s => (999, s + 1)));

            // Act
            var (value, state) = handled.Run(0);

            // Assert
            Assert.Equal(999, value);
            Assert.Equal(1, state);
        }

        private sealed class DummyResource : IDisposable
        {
            public bool Disposed { get; private set; }
            public void Dispose() => Disposed = true;
        }

        [Fact]
        public void Using_DisposesResource()
        {
            // Arrange
            var res = new DummyResource();
            var st = new State<int, int>(s => (s + 1, s + 2));
            var wrapped = st.Using(res, _ => st);

            // Act
            var (value, state) = wrapped.Run(3);

            // Assert
            Assert.Equal(4, value);
            Assert.Equal(5, state);
            Assert.True(res.Disposed);
        }

        [Fact]
        public void Zip_RunsBoth_ReturnsTupleValue_AndFinalState()
        {
            // Arrange
            var x = new State<int, string>(s => ($"x{s}", s + 1));
            var y = new State<int, int>(s => (s * 2, s + 3));

            // Act
            var zipped = x.Zip(x, y);
            var ((a, b), state) = zipped.Run(2);

            // Assert
            // x: "x2", s=3; y: 6, s=6
            Assert.Equal("x2", a);
            Assert.Equal(6, b);
            Assert.Equal(6, state);
        }
    }
}