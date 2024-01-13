using System;

namespace Moonad
{
    public readonly struct Choice<T1, T2, T3> : IChoice, IEquatable<Choice<T1, T2, T3>>
        where T1 : notnull where T2 : notnull where T3 : notnull
    {
        public readonly IChoice Choosed;

        public Choice(T1 choice) =>
            Choosed = new Choice<T1>(choice);

        public Choice(T2 choice) =>
            Choosed = new Choice<T2>(choice);

        public Choice(T3 choice) =>
            Choosed = new Choice<T3>(choice);

        public static implicit operator Choice<T1, T2, T3>(T1 choice) =>
            new(choice);

        public static implicit operator Choice<T1, T2, T3>(T2 choice) =>
            new(choice);

        public static implicit operator Choice<T1, T2, T3>(T3 choice) =>
            new(choice);

        public static implicit operator T1(Choice<T1, T2, T3> choice) =>
            (Choice<T1>)choice.Choosed;

        public static implicit operator T2(Choice<T1, T2, T3> choice) =>
            (Choice<T2>)choice.Choosed;

        public static implicit operator T3(Choice<T1, T2, T3> choice) =>
            (Choice<T3>)choice.Choosed;

        public static bool operator ==(Choice<T1, T2, T3> left, Choice<T1, T2, T3> right) =>
            left.Equals(right);

        public static bool operator !=(Choice<T1, T2, T3> left, Choice<T1, T2, T3> right) =>
            !left.Equals(right);

        public bool Equals(Choice<T1, T2, T3> other)
        {
            if (Choosed is Choice<T1> choice1 && other.Choosed is Choice<T1>)
                return choice1.Equals(other.Choosed);

            if (Choosed is Choice<T2> choice2 && other.Choosed is Choice<T2>)
                return choice2.Equals(other.Choosed);

            if (Choosed is Choice<T3> choice3 && other.Choosed is Choice<T3>)
                return choice3.Equals(other.Choosed);

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
            return Choosed switch
            {
                Choice<T1> choice => choice.GetHashCode(),
                Choice<T2> choice => choice.GetHashCode(),
                Choice<T3> choice => choice.GetHashCode(),
                _ => throw new ChoiceValueException()
            };
        }
    }
}
