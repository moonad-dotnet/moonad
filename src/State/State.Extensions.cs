using System;

namespace Moonad
{
    public static class StateExtensions
    {
        public static State<S, U> Select<S, T, U>(this State<S, T> state, Func<T, U> selector) =>
            state.Map(selector, state);

        public static State<S, U> SelectMany<S, T, U>(this State<S, T> state, Func<T, State<S, U>> binder) =>
            state.Bind(binder, state);

        public static State<S, V> SelectMany<S, T, U, V>(this State<S, T> state, Func<T, State<S, U>> binder, Func<T, U, V> projector)
        {
            return new State<S, V>(s =>
            {
                var (t, s1) = state.Run(s);
                var (u, s2) = binder(t).Run(s1);
                return (projector(t, u), s2);
            });
        }

        public static State<S, T> Where<S, T>(this State<S, T> state, Func<T, bool> predicate) =>
            new (s =>
            {
                var (value, newState) = state.Run(s);
                if (!predicate(value))
                    throw new InvalidOperationException("State.Where predicate failed");
                
                return (value, newState);
            });
    }
}
