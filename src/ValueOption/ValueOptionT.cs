namespace Moonad
{
    public readonly partial struct ValueOption<T> where T : struct
    {
        private readonly T? Value;

        public bool IsSome => Value is not null;
        public bool IsNone => !IsSome;

        private ValueOption(T? value) => 
            Value = value;

        public static implicit operator ValueOption<T>(T? value)
        { 
            if(value.HasValue)
                return new(value.Value);

            return new(null);
        }

        public static implicit operator T(ValueOption<T> option)
        {
            return option.Get();
        }

        public static implicit operator bool (ValueOption<T> option) 
        {
            return option.IsSome;
        }

        public static ValueOption<T> Some(T value)
        {
            return new(value);
        }

        public static ValueOption<T> None()
        { 
            return new();
        }

        public override string ToString()
        {
            if (IsNone)
                return "None";

            return $"Some {Get()}";
        }
    }
}
