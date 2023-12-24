namespace Moonad
{
    public readonly struct Result<T> where T : notnull
    {
        private readonly T ResultValue;
        
        public readonly bool IsOk;
        public readonly bool IsError => !IsOk;

        public Result()
        {
            IsOk = false;
            ResultValue = default!;
        }

        private Result(T value)
        {
            IsOk = true;
            ResultValue = value;
        }

        public static implicit operator Result<T>(T value) =>
            new(value);

        public static implicit operator bool(Result<T> result) =>
            result.IsOk;

        public T Value => IsOk ? ResultValue : throw new ResultValueException();

        public static Result<T> Sucess(T value) =>
            new(value);

        public static Result<T> Failure() =>
            new();
    }
}
