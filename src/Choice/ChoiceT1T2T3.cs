using System;

namespace Moonad
{
    public readonly struct Choice<T1, T2, T3> : IEquatable<Choice<T1, T2, T3>>
        where T1 : notnull where T2 : notnull where T3 : notnull
    {
        public readonly IChoice Chosen;

        public Choice(T1 choice) =>
            Chosen = new Choice<T1>(choice);

        public Choice(T2 choice) =>
            Chosen = new Choice<T2>(choice);

        public Choice(T3 choice) =>
            Chosen = new Choice<T3>(choice);

        public static implicit operator Choice<T1, T2, T3>(T1 choice) =>
            new(choice);

        public static implicit operator Choice<T1, T2, T3>(T2 choice) =>
            new(choice);

        public static implicit operator Choice<T1, T2, T3>(T3 choice) =>
            new(choice);

        public static implicit operator T1(Choice<T1, T2, T3> choice) =>
            (Choice<T1>)choice.Chosen;

        public static implicit operator T2(Choice<T1, T2, T3> choice) =>
            (Choice<T2>)choice.Chosen;

        public static implicit operator T3(Choice<T1, T2, T3> choice) =>
            (Choice<T3>)choice.Chosen;

        public static bool operator ==(Choice<T1, T2, T3> left, Choice<T1, T2, T3> right) =>
            left.Equals(right);

        public static bool operator !=(Choice<T1, T2, T3> left, Choice<T1, T2, T3> right) =>
            !left.Equals(right);

        public bool Equals(Choice<T1, T2, T3> other)
        {
            if (Chosen is Choice<T1> choice1 && other.Chosen is Choice<T1>)
                return choice1.Equals(other.Chosen);

            if (Chosen is Choice<T2> choice2 && other.Chosen is Choice<T2>)
                return choice2.Equals(other.Chosen);

            if (Chosen is Choice<T3> choice3 && other.Chosen is Choice<T3>)
                return choice3.Equals(other.Chosen);

            return false;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not Choice<T1, T2, T3> other)
                return false;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Chosen switch
            {
                Choice<T1> choice => choice.GetHashCode(),
                Choice<T2> choice => choice.GetHashCode(),
                Choice<T3> choice => choice.GetHashCode(),
                _ => throw new ChoiceValueException()
            };
        }
    }
}
