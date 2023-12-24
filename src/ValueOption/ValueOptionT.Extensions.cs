namespace Moonad
{
    public static class ValueOptionExtensions
    {
        public static ValueOption<T> Flatten<T>(ref this ValueOption<ValueOption<T>> value) where T : struct
        { 
            if(value.IsNone)
                return value;

            return value.Get();
        }
        
        public static ValueOption<T> ToValueOption<T>(ref this T? value) where T : struct
        {
            if (value.HasValue)
                return value.Value;

            return new ValueOption<T>();
        }
    }
}
