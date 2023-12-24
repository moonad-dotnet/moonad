using System;
using System.Collections.Generic;

namespace Moonad
{
    public readonly partial struct ValueOption<T>
    {
        public ValueOption<TResult> Bind<TResult>(Func<T, ValueOption<TResult>> binder) where TResult : struct
        {
            if (IsNone)
                return new();

            var result = binder(Get());
            if (result.IsSome)
                return result;

            return new();
        }

        public bool Contains(T value)
        {
            if (IsNone)
                return false;

            return Get().Equals(value);
        }

        public int Count()
        {
            if (IsNone)
                return 0;

            return 1;
        }

        public T DefaultValue(T defaultValue)
        {
            if (IsNone)
                return defaultValue;

            return Get();
        }

        public T DefaultWith(Func<T> defaultFunc)
        {
            if (IsNone)
                return defaultFunc();

            return Get();
        }

        public bool Exists(Func<T, bool> predicate)
        {
            if (IsNone)
                return false;

            return predicate(Get());
        }

        public ValueOption<T> Filter(Func<ValueOption<T>, bool> predicate)
        {
            if (IsNone || predicate(this))
                return this;

            return new();
        }

        public TState Fold<TState>(Func<TState, T, TState> folder, TState state)
        {
            if (IsNone)
                return state;

            return folder(state, Get());
        }

        public TState FoldBack<TState>(Func<T, TState, TState> folder, TState state)
        {
            if (IsNone)
                return state;

            return folder(Get(), state);
        }

        public bool ForAll(Func<T, bool> predicate)
        {
            if (IsNone)
                return true;

            return predicate(Get());
        }

        public T Get()
        {
            return Value ?? throw NoneValueException();
        }

        public void Iter(Action<T> action)
        {
            if (IsSome)
                action(Get());
        }

        public ValueOption<T> Map(Func<T, ValueOption<T>> mapping)
        {
            if (IsNone)
                return this;

            return mapping(Get());
        }

        public ValueOption<T> Map2(ValueOption<T> option2, Func<T, T, ValueOption<T>> mapping)
        {
            if (IsNone || option2.IsNone)
                return new ();

            return mapping(Get(), option2.Get());
        }

        public void Match(Action<T> some, Action none)
        {
            if (IsSome)
            {
                some(Get());
                return;
            }

            none();
        }

        public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
        {
            if (IsSome)
                return some(Get());

            return none();
        }

        public ValueOption<T> OrElse(ValueOption<T> ifNone)
        {
            if (IsNone)
                return ifNone;

            return this;
        }

        public ValueOption<T> OrElseWith(Func<ValueOption<T>> ifNoneFunc)
        {
            if (IsSome)
                return this;

            return ifNoneFunc();
        }

        public T[] ToArray()
        {
            if (IsNone)
                return Array.Empty<T>();

            return new T[] { Get() };
        }

        public List<T> ToList()
        {
            if (IsNone)
                return new List<T>();

            return new List<T>() { Get() };
        }
    }
}
