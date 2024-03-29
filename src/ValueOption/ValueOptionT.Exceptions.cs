using System;

namespace Moonad
{
    public readonly partial struct ValueOption<T>
    {
        internal static ValueOptionException NoneValueException() =>
            new("This instance has no value.");
    }

    public class ValueOptionException(string message) : Exception(message) { }
}