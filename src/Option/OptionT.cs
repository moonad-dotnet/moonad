using System;
using System.Collections.Generic;

namespace Moonad;

public partial union Option<T>(T, None) where T : notnull
{
    public bool IsSome => Value is T;
    public bool IsNone => !IsSome;

    public static implicit operator T(Option<T> option) => option.Get();
}
