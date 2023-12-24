using System;

namespace Moonad
{
    public readonly partial struct ValueOption<T> : IEquatable<ValueOption<T>>
    {
        public static bool operator ==(ValueOption<T> left, ValueOption<T> right) =>
            Equals(left, right);

        public static bool operator !=(ValueOption<T> left, ValueOption<T> right) =>
            !Equals(left, right);

        public bool Equals(ValueOption<T> other)
        {
            if (IsNone && other.IsNone)
                return true;

            if (IsSome && other.IsSome)
                return Get().Equals(other.Get());

            return false;
        }

        public override bool Equals(object obj)
        {
            if(obj is null || obj is not ValueOption<T> other)
                return false;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            if (IsSome)
                return Value.GetHashCode();

            return 0;
        }
    }
}
