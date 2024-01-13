using System;

namespace Moonad
{
    public readonly partial struct Result<TResult, TError> 
        : IEquatable<Result<TResult, TError>> where TResult : notnull where TError : notnull
    {
        public readonly bool IsOk;
        public readonly bool IsError => !IsOk;

        private readonly TResult? ResultValue;
        private readonly TError? ErrorValue;

        private Result(TResult result)
        {
            IsOk = true;
            ResultValue = result;
            ErrorValue = default;
        }

        private Result(TError errorValue)
        {
            IsOk = false;
            ResultValue = default;
            ErrorValue = errorValue;
        }

        public static Result<TResult, TError> Success(TResult result) =>
            new (result);

        public static Result<TResult, TError> Failure(TError error) =>
            new(error);

        public static implicit operator Result<TResult, TError>(TResult result) =>
            new(result);

        public static implicit operator Result<TResult, TError>(TError error) =>
            new(error);

        public TResult Value => IsOk ? ResultValue! : throw new ResultValueException();

        public TError Error => IsError ? ErrorValue! : throw new ResultErrorValueException();
                
        public static implicit operator TResult(Result<TResult, TError> result) =>
            result.Value;

        public static implicit operator bool(Result<TResult, TError> result) =>
            result.IsOk;

        public static bool operator ==(Result<TResult, TError> left, Result<TResult, TError> right) =>
            left.Equals(right);

        public static bool operator !=(Result<TResult, TError> left, Result<TResult, TError> right) =>
            !(left == right);

        public bool Equals(Result<TResult, TError> other)
        {
            if (IsOk && other.IsOk)
                return ResultValue!.Equals(other.ResultValue);

            if (IsError && other.IsError)
                return ErrorValue!.Equals(other.ErrorValue);

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
                ? ResultValue!.GetHashCode()
                : ErrorValue!.GetHashCode();
        }
    }
}
