using System;

namespace Moonad
{
    public abstract partial class Option<T>
    {
        internal static OptionValueException NoneValueException() =>
            new ();
    }

    public sealed class OptionValueException : Exception
    { 
        public OptionValueException() : base("Instances of 'None' does not hold values.") { }
    }
}
