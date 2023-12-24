namespace Moonad
{
    public class Option
    {
        public static Option<T> None<T>() where T : notnull
        {
            return new Option<T>.None();
        }

        public static Option<T> Some<T>(T value) where T : notnull
        {
            return new Option<T>.Some(value);
        }
    }
}
