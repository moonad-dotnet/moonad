using System;

namespace Moonad
{
    public abstract partial class Option<T>
    {
        internal static OptionException NoneValueException() =>
            new ();
    }

    public sealed class OptionException() : Exception("Instances of 'None' does not hold values.") { }
}
