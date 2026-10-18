using System;
using System.Collections.Generic;

namespace Moonad;

public partial union Option<T> where T : notnull
{
    public static bool operator ==(Option<T> left, Option<T> right) => left.Equals(right);

    public static bool operator !=(Option<T> left, Option<T> right) => !left.Equals(right);

    public bool Equals(Option<T> other) =>
        Value is T a
            ? other.Value is T b && EqualityComparer<T>.Default.Equals(a, b)
            : other.Value is not T;

    public override bool Equals(object? obj) => obj is Option<T> other && Equals(other);

    public override int GetHashCode() => Value is T v ? v.GetHashCode() : 0;
}
