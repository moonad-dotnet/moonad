using System;

namespace Moonad
{
    public readonly partial struct Result<TResult, TError> 
        : IEquatable<Result<TResult, TError>> where TResult : notnull where TError : notnull
    {
        public readonly TResult _resultValue;
        public readonly TError _resultError;

        public readonly TResult ResultValue => 
            IsOk
                ? _resultValue 
                : throw new ResultValueException();

        public readonly TError ErrorValue => 
            !IsOk 
                ? _resultError 
                : throw new ErrorValueException();

        public readonly bool IsOk;
        public readonly bool IsError => !IsOk;

        private Result(bool isOk, TResult resultValue, TError resultError) =>
            (IsOk, _resultValue, _resultError) = (isOk, resultValue, resultError);

        private Result(TResult resultValue) : this(true, resultValue, default!) { }

        private Result(TError error) : this(false, default!, error) { }

        public static Result<TResult, TError> Ok(TResult result) =>
            new(result);

        public static Result<TResult, TError> Error(TError error) =>
            new(error);

        public static implicit operator Result<TResult, TError>(TResult result) =>
            Ok(result);

        public static implicit operator Result<TResult, TError>(TError error) =>
            Error(error);

        public static implicit operator TResult(Result<TResult, TError> result) =>
            result.ResultValue;

        public static implicit operator TError(Result<TResult, TError> result) =>
            result.ErrorValue;

        public static implicit operator bool(Result<TResult, TError> result) =>
            result.IsOk;

        public static bool operator ==(Result<TResult, TError> left, Result<TResult, TError> right) =>
            left.Equals(right);

        public static bool operator !=(Result<TResult, TError> left, Result<TResult, TError> right) =>
            !(left == right);

        public bool Equals(Result<TResult, TError> other)
        {
            if (IsOk && other.IsOk)
                return ResultValue.Equals(other.ResultValue);

            if (!IsOk && !other.IsOk)
                return ResultValue.Equals(other.ErrorValue);

            return false;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not Result<TResult, TError> other)
                return false;

            return Equals(other);
        }

        public override int GetHashCode() 
        {
            return IsOk 
                ? ResultValue.GetHashCode() 
                : ErrorValue.GetHashCode();
        }
    }
}
