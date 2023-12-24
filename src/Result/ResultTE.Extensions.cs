namespace Moonad
{
    public static class ResultExtensions
    {
        public static ValueOption<TResult> ToValueOption<TResult, TError>(this Result<TResult, TError> value) 
            where TResult : struct where TError : notnull
        {
            if (value.IsOk)
                return ValueOption<TResult>.Some(value);

            return ValueOption<TResult>.None();
        }
    }
}
