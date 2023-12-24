using System;

namespace Moonad
{
    public abstract class Choice<T1, T2> : IEquatable<Choice<T1, T2>> where T1 : notnull where T2 : notnull
    {
        public bool IsChoice1 => this is Choice1;
        public bool IsChoice2 => this is Choice2;

        public T1 Choice1Of2 => GetChoice1();
        public T2 Choice2Of2 => GetChoice2();

        private Choice() { }

        public sealed class Choice1(T1 value) : Choice<T1, T2>
        {
            internal T1 Value { get; } = value;
        }

        public sealed class Choice2(T2 value) : Choice<T1, T2>
        {
            internal T2 Value { get; } = value;
        }

        public static implicit operator Choice<T1, T2>(T1 choice1) =>
            new Choice1(choice1);

        public static implicit operator Choice<T1, T2>(T2 choice2) => 
            new Choice2(choice2);

        public static implicit operator T1(Choice<T1, T2> choice) =>
            choice.GetChoice1();

        public static implicit operator T2(Choice<T1, T2> choice) =>
            choice.GetChoice2();

        private T1 GetChoice1() =>
            ((Choice1)this).Value;

        private T2 GetChoice2() =>
            ((Choice2)this).Value;

        public static bool operator == (Choice<T1,T2> left, Choice<T1, T2> right) =>
            left.Equals(right);

        public static bool operator !=(Choice<T1, T2> left, Choice<T1, T2> right) =>
            !left.Equals(right);

        public bool Equals(Choice<T1, T2>? other)
        {
            if(other is null) 
                return false;

            if(this is Choice1 && other is Choice1 choice1)
                return GetChoice1().Equals(choice1.Value);

            if (this is Choice2 && other is Choice2 choice2)
                return GetChoice2().Equals(choice2.Value);

            return false;
        }

        public override bool Equals(object? obj)
        {
            if(obj is null || obj is not Choice<T1, T2> other)
                return false;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return this is Choice1
                ? GetChoice1().GetHashCode() 
                : GetChoice2().GetHashCode();
        }

        public override string ToString()
        {
            return this is Choice1
                ? $"{nameof(Choice1)} {GetChoice1()}"
                : $"{nameof(Choice2)} {GetChoice2()}";
        }
    }
}
