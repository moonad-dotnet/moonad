namespace Moonad;

public static class OptionExtensions
{
    extension<T>(Option<Option<T>> value) where T : notnull
    {
        public Option<T> Flatten() => value.Bind(inner => inner);
    }

    extension<T>(T? value) where T : class
    {
        public Option<T> ToOption() =>
            value is null ? Option.None<T>() : Option.Some(value);
    }

    extension<T>(T? value) where T : struct
    {
        public Option<T> ToOption() =>
            value.HasValue ? Option.Some(value.Value) : Option.None<T>();
    }
}
