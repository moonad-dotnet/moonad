using System;

namespace Moonad
{
    public readonly struct Result<TError>: IEquatable<Result<TError>> where TError : notnull
    {
        public readonly TError _errorValue;

        public readonly TError ErrorValue =>
            !IsOk
                ? _errorValue
                : throw new ErrorValueException();

        public readonly bool IsOk;
        public readonly bool IsError => !IsOk;

        private Result(bool isOk, TError resultError = default!) =>
            (IsOk, _errorValue) = (isOk, resultError);

        public static Result<TError> Ok() =>
            new(true);

        public static Result<TError> Error(TError error) =>
            new(false, error);

        public static implicit operator Result<TError>(bool isOk) =>
            isOk 
                ? new(isOk) 
                : throw new ErrorValueException("A Result<TError> must be initialized with an error");
        
        public static implicit operator Result<TError>(TError error) =>
            Error(error);

        public static implicit operator TError(Result<TError> result) =>
            result.ErrorValue;

        public static implicit operator bool(Result<TError> result) =>
            result.IsOk;

        public static bool operator ==(Result<TError> left, Result<TError> right) =>
            left.Equals(right);

        public static bool operator !=(Result<TError> left, Result<TError> right) =>
            !(left == right);

        public bool Equals(Result<TError> other)
        {
            if (IsOk && other.IsOk)
                return true;

            return false;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not Result<TError> other)
                return false;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return IsOk 
                ? base.GetHashCode()
                : ErrorValue.GetHashCode();
        }
    }
}