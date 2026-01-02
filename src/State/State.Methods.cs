using System;

namespace Moonad
{
    public sealed partial class State<S, T>
    {
        public (T, S) Run(S initial)
            => Function(initial);

        public T Eval(S initial)
            => Function(initial).Value;

        public S Exec(S initial)
            => Function(initial).State;

        public State<S, U> Apply<U>(State<S, Func<T, U>> sf) =>
            new(s =>
            {
                var (f, s2) = sf.Run(s);
                var (x, s3) = Run(s2);
                return (f(x), s3);
            });

        public State<S, U> Bind<U>(Func<T, State<S, U>> f, State<S, T> state) =>
            new(s =>
            {
                var (value, newState) = state.Run(s);
                return f(value).Run(newState);
            });

        public State<S, T> Delay(Func<State<S, T>> body) =>
            new(s => body().Run(s));

        public State<S, S> Get() =>
            new(s => (s, s));

        public State<S, T> Gets(Func<S, T> f) =>
            new(s => (f(s), s));

        public Func<T, State<S, V>> Kleisli<U, V>(Func<T, State<S, U>> f, Func<U, State<S, V>> g) =>
            t => new State<S, V>(s =>
                 {
                    var (u, i) = f(t).Run(s);
                    var (x, y) = g(u).Run(i);
                    return (x, y);
                 });

        public State<S, U> Lift<U>(Func<T, U> f) =>
            Map(f, this);

        public State<S, V> Lift2<U, V>(Func<T, U, V> f, State<S, T> x, State<S, U> y) =>
            Map2(f, x, y);

        public State<S, U> Map<U>(Func<T, U> f, State<S, T> state) =>
            new(s =>
            {
                var (value, newState) = state.Run(s);
                return (f(value), newState);
            });

        public State<S, V> Map2<U, V>(Func<T, U, V> f, State<S, T> x, State<S, U> y) =>
            new(s =>
            {
                var (a, s2) = x.Run(s);
                var (b, s3) = y.Run(s2);
                return (f(a, b), s3);
            });

        public State<S, W> Map3<U, V, W>(Func<T, U, V, W> f, State<S, T> x, State<S, U> y, State<S, V> z) =>
            new(s =>
            {
                var (a, s2) = x.Run(s);
                var (b, s3) = y.Run(s2);
                var (c, s4) = z.Run(s3);
                return (f(a, b, c), s4);
            });

        public State<S, Unit> Modify(Func<S, S> f) =>
            new(s => (Unit.Value, f(s)));

        public State<S, Unit> Put(S newState) =>
            new(_ => (Unit.Value, newState));

        public State<S, U> SequenceRight<U>(State<S, T> x, State<S, U> y) =>
            new(s => y.Run(x.Run(s).Item2));

        public State<S, T> SequenceLeft<U>(State<S, T> x, State<S, U> y) =>
            new(s =>
            {
                var (vx, s2) = x.Run(s);
                var (_, s3) = y.Run(s2);
                return (vx, s3);
            });

        public State<S, T> Throw(Exception ex) =>
            new(_ => throw ex);

        public State<S, T> TryFinally(State<S, T> body, Action compensation) =>
            new(s =>
            {
                try { return body.Run(s); }
                finally { compensation(); }
            });

        public State<S, T> TryWith(State<S, T> body, Func<Exception, State<S, T>> handler) =>
            new(s =>
            {
                try { return body.Run(s); }
                catch (Exception ex) { return handler(ex).Run(s); }
            });

        public State<S, T> Using<R>(R resource, Func<R, State<S, T>> body) where R : IDisposable =>
            TryFinally(body(resource), () => resource?.Dispose());

        public State<S, (T, U)> Zip<U>(State<S, T> x, State<S, U> y) =>
            new(s =>
            {
                var (a, s2) = x.Run(s);
                var (b, s3) = y.Run(s2);
                return ((a, b), s3);
            });
    }
}
