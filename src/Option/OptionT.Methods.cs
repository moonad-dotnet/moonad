using System;
using System.Collections.Generic;

namespace Moonad;

public partial union Option<T> where T : notnull
{
    public T Get() => Value is T v ? v : throw new OptionException();

    public bool TryGet(out T value)
    {
        if (Value is T v)
        {
            value = v;
            return true;
        }

        value = default!;
        return false;
    }

    public Option<TResult> Bind<TResult>(Func<T, Option<TResult>> binder) where TResult : notnull =>
        Value is T v ? binder(v) : Option.None<TResult>();

    public bool Contains(T value) =>
        Value is T v && EqualityComparer<T>.Default.Equals(v, value);

    public int Count() => IsSome ? 1 : 0;

    public T DefaultValue(T defaultValue) => Value is T v ? v : defaultValue;

    public T DefaultWith(Func<T> defaultFunc) => Value is T v ? v : defaultFunc();

    public bool Exists(Func<T, bool> predicate) => Value is T v && predicate(v);

    public Option<T> Filter(Func<T, bool> predicate) =>
        Value is T v && !predicate(v) ? Option.None<T>() : this;

    public TState Fold<TState>(Func<TState, T, TState> folder, TState state) =>
        Value is T v ? folder(state, v) : state;

    public TState FoldBack<TState>(Func<T, TState, TState> folder, TState state) =>
        Value is T v ? folder(v, state) : state;

    public bool ForAll(Func<T, bool> predicate) => Value is not T v || predicate(v);

    public void Iter(Action<T> action)
    {
        if (Value is T v)
            action(v);
    }

    public Option<TResult> Map<TResult>(Func<T, Option<TResult>> mapping) where TResult : notnull =>
        Value is T v ? mapping(v) : Option.None<TResult>();

    public Option<TResult> Map2<TResult>(Option<T> option2, Func<T, T, Option<TResult>> mapping) where TResult : notnull =>
        Value is T a && option2.Value is T b ? mapping(a, b) : Option.None<TResult>();

    public Option<TResult> Map3<TResult>(Option<T> option2, Option<T> option3, Func<T, T, T, Option<TResult>> mapping) where TResult : notnull =>
        Value is T a && option2.Value is T b && option3.Value is T c
            ? mapping(a, b, c)
            : Option.None<TResult>();

    public void Match(Action<T> some, Action none)
    {
        if (Value is T v)
            some(v);
        else
            none();
    }

    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none) =>
        Value is T v ? some(v) : none();

    public Option<T> OrElse(Option<T> ifNone) => IsSome ? this : ifNone;

    public Option<T> OrElseWith(Func<Option<T>> ifNoneFunc) => IsSome ? this : ifNoneFunc();

    public T[] ToArray() => Value is T v ? [v] : [];

    public List<T> ToList() => Value is T v ? [v] : [];

    public override string ToString() => Value is T v ? $"Some {v}" : "None";
}