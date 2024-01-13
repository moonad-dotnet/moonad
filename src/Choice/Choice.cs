namespace Moonad
{
    public interface IChoice { }

    public readonly struct Choice<T>(T value) : IChoice where T : notnull
    { 
        private readonly T Choosed = value;

        public static implicit operator T(Choice<T> choice) =>
            choice.Choosed;

        public override string ToString() =>
            Choosed.ToString();
    }
}
