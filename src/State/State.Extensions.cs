using System;

namespace Moonad
{
    public static class StateExtensions
    {
        public static State<S, U> Select<S, T, U>(this State<S, T> state, Func<T, U> selector) =>
            state.Map(selector, state);

        public static State<S, U> SelectMany<S, T, U>(this State<S, T> state, Func<T, State<S, U>> binder) =>
            state.Bind(binder, state);

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
