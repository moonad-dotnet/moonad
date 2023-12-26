namespace Moonad
{
    public abstract partial class Option<T> where T : notnull
    {
        public bool IsSome => this is Some;
        public bool IsNone => this is None;

        private Option() { }

        public sealed class Some(T value) : Option<T>
        {
            internal T Value { get; } = value;
        }

        public sealed class None : Option<T>
        {

        }

        public static implicit operator Option<T>(T? value)
        {
            if(value is null)
                return new None();

            return new Some(value);
        }

        public static implicit operator T(Option<T> option)
        {
            return option.Get();
        }

        public static implicit operator bool(Option<T> option)
        { 
            return option.IsSome;
        }

        public override string ToString()
        {
            if (this is None)
                return "None";

            return $"Some {Get()}";
        }
    }
}