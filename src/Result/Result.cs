namespace Moonad
{
    public readonly ref struct Result
    {
        public readonly bool IsOk;
        public readonly bool IsError => !IsOk;

        private Result(bool isOk) =>
            IsOk = isOk;

        public static implicit operator Result(bool isOk) =>
            new(isOk);

        public static implicit operator bool(Result result) =>
            result.IsOk;

        public static Result Sucess() =>
            new (true);

        public static Result Failure() => 
            new (false);
    }
}
