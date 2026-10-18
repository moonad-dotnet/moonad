using System;

namespace Moonad;

public readonly record struct None
{
    public override string ToString() => "None";
}

public sealed class OptionException() : Exception("Instances of 'None' does not hold values.");

public static class Option
{
    public static Option<T> None<T>() where T : notnull => default(None);

    public static Option<T> Some<T>(T value) where T : notnull => value;
}
