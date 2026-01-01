using System;

namespace Moonad
{
    public sealed partial class State<S, T>(Func<S, (T, S)> f)
    {
        public Func<S, (T Value, S State)> Function { get; private set; } = f;

        public static implicit operator State<S, T>(T value) =>
            new(s => (value, s));
    }
}
