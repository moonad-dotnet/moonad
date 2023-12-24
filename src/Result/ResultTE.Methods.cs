using System;
using System.Collections.Generic;

namespace Moonad
{
    public readonly partial struct Result<TResult, TError>
    {
        public Result<U, TError> Bind<U>(Func<TResult, Result<U, TError>> binder) where U : notnull
        {
            if (IsOk)
                return binder(ResultValue!);

            return new Result<U, TError>(ErrorValue!);
        }

        public bool Contains(TResult value)
        { 
            if(IsError)
                return false;

            return ResultValue!.Equals(value);
        }

        public int Count()
        { 
            return IsOk 
                ? 1
                : 0;
        }

        public TResult DefaultValue(TResult defaultValue) 
        {
            if (IsOk)
                return ResultValue!;

            return defaultValue;
        }

        public TResult DefaultWith(Func<TError, TResult> defaultFunc)
        {
            if (IsOk)
                return ResultValue!;

            return defaultFunc(ErrorValue!);
        }

        public bool Exists(Func<TResult, bool> predicate) 
        { 
            if(IsError)
                return false;

            return predicate(ResultValue!);
        }

        public TState Fold<TState>(Func<TState, TResult, TState> folder, TState state)
        {
            if (IsError)
                return state;

            return folder(state, ResultValue!);
        }

        public TState FoldBack<TState>(Func<TResult, TState, TState> folder, TState state)
        {
            if (IsError)
                return state;

            return folder(ResultValue!, state);
        }

        public bool ForAll(Func<TResult, bool> predicate)
        { 
            if(IsError)
                return true;

            return predicate(ResultValue!);
        }

        public void Iter(Action<TResult> action)
        {
            if(IsError)
                return;

            action(ResultValue!);
        }

        public Result<U, TError> Map<U>(Func<TResult, Result<U, TError>> mapping) where U : notnull
        { 
            if(IsError)
                return new Result<U, TError>(ErrorValue!);

            return mapping(ResultValue!);
        }

        public Result<TResult, U> MapError<U>(Func<TError, Result<TResult, U>> mapping) where U : notnull
        {
            if (IsError)
                return mapping(ErrorValue!);
            
            return new Result<TResult, U>(ResultValue!);
        }

        public Array ToArray()
        { 
            if(IsError)
                return Array.Empty<TResult>();

            return new []{ ResultValue! };
        }

        public List<TResult> ToList() 
        {
            if (IsError)
                return new List<TResult>();

            return new List<TResult> { ResultValue! };
        }

        public Option<TResult> ToOption() 
        { 
            if(IsOk)
                return ResultValue!;

            return Option.None<TResult>();
        }
    }
}
