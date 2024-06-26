﻿using System;
using System.Collections.Generic;

namespace Moonad
{
    public abstract partial class Option<T>
    {
        public Option<TResult> Bind<TResult>(Func<T, Option<TResult>> binder) where TResult : notnull
        {
            if (this is None)
                return new Option<TResult>.None();

            var result = binder(Get());
            if (result is null or None)
                return new Option<TResult>.None();

            return result;
        }

        public bool Contains(T value)
        {
            if (this is None)
                return false;

            return Get()!.Equals(value);
        }

        public int Count()
        {
            if (this is None)
                return 0;

            return 1;
        }

        public T DefaultValue(T defaultValue)
        {
            if (this is None)
                return defaultValue;

            return Get();
        }

        public T DefaultWith(Func<T> defaultFunc)
        {
            if (this is None)
                return defaultFunc();

            return Get();
        }

        public bool Exists(Func<T, bool> predicate)
        {
            if (this is None)
                return false;

            return predicate(Get());
        }

        public Option<T> Filter(Func<Option<T>, bool> predicate)
        {
            if (this is None || predicate(this))
                return this;

            return new None();
        }

        public TState Fold<TState>(Func<TState, T, TState> folder, TState state)
        {
            if (this is None)
                return state;

            return folder(state, Get());
        }

        public TState FoldBack<TState>(Func<T, TState, TState> folder, TState state)
        {
            if (this is None)
                return state;

            return folder(Get(), state);
        }

        public bool ForAll(Func<T, bool> predicate)
        {
            if (this is None)
                return true;

            return predicate(Get());
        }

        public T Get()
        {
            if (this is Some some)
                return some.Value;

            throw NoneValueException();
        }

        public void Iter(Action<T> action)
        {
            if (this is Some some)
                action(some.Get());
        }

        public Option<TResult> Map<TResult>(Func<T, Option<TResult>> mapping) where TResult : notnull
        {
            if (this is None)
                return Option.None<TResult>();

            return mapping(Get());
        }

        public Option<T> Map(Func<T, Option<T>> mapping)
        {
            return Map<T>(mapping);
        }

        public Option<TResult> Map2<TResult>(Option<T> option2, Func<T, T, Option<TResult>> mapping) where TResult: notnull
        {
            if (this is None || option2 is None)
                return Option.None<TResult>();

            return mapping(Get(), option2.Get());
        }

        public Option<T> Map2(Option<T> option2, Func<T, T, Option<T>> mapping)
        {
            return Map2<T>(option2, mapping);
        }

        public Option<TResult> Map3<TResult>(Option<T> option2, Option<T> option3, Func<T, T, T, Option<TResult>> mapping) where TResult : notnull
        {
            if (this is None || option2 is None || option3 is None)
                return Option.None<TResult>();

            return mapping(Get(), option2.Get(), option3.Get());
        }

        public Option<T> Map3(Option<T> option2, Option<T> option3, Func<T, T, T, Option<T>> mapping)
        {
            return Map3<T>(option2, option3, mapping);
        }

        public void Match(Action<T> some, Action none)
        {
            if (this is Some s)
            {
                some(s.Get());
                return;
            }

            none();
        }

        public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
        {
            if (this is Some s)
                return some(s.Get());

            return none();
        }

        public Option<T> OrElse(Option<T> ifNone)
        {
            if (this is None)
                return ifNone;

            return this;
        }

        public Option<T> OrElseWith(Func<Option<T>> ifNoneFunc)
        {
            if (this is Some)
                return this;

            return ifNoneFunc();
        }

        public T[] ToArray()
        {
            if (this is None)
                return [];

            return [Get()];
        }

        public List<T> ToList()
        {
            if (this is None)
                return [];

            return [Get()];
        }
    }
}
