using System;

namespace Moonad
{
    public static class OptionExtensions
    {
        public static Option<T> Flatten<T>(this Option<Option<T>> value) where T : notnull
        {
            if (value is Option<T>.None)
                return value;

            return value.Get();
        }

        public static Option<T> ToOption<T>(this T? value) where T : class
        {
            if (value is null)
                return new Option<T>.None();

            return new Option<T>.Some(value);
        }

        public static Option<T> ToOption<T>(ref this T? value) where T : struct
        {
            if (value.HasValue)
                return new Option<T>.Some(value.Value);
            
            return new Option<T>.None();
        }
    }
}
