using System;

namespace Moonad
{
    public abstract partial class Option<T>
    {
        public static bool operator ==(Option<T> left, Option<T> right) =>
            Equals(left, right);

        public static bool operator !=(Option<T> left, Option<T> right) =>
            !Equals(left, right);

        public override bool Equals(object? obj)
        {
            if (obj is Option<T> other)
                return Equals(other);

            return false;
        }

        public bool Equals(Option<T> other)
        {
            if (this is None && other is None)
                return true;

            if (this is Some left && other is Some right)
                return left.Get().Equals(right.Get());

            return false;
        }

        public override int GetHashCode()
        {
            if (this is Some some)
                return some.GetHashCode();

            return 0;
        }
    }
}
