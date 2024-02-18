namespace Moonad
{
    public interface IChoice { }

    public readonly struct Choice<T>(T value) : IChoice where T : notnull
    { 
        private readonly T Chosen = value;

        public static implicit operator T(Choice<T> choice) =>
            choice.Chosen;

        public override string ToString() =>
            Chosen.ToString();
    }
}
