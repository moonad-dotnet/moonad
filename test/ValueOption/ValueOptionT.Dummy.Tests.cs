namespace Moonad.Tests.ValueOptions
{
    public class OptionTDummyTest
    {
        [Theory, MemberData(nameof(DummyCollection))]
        public void ConvertDummy(Dummy? dummy, bool expected)
        {
            ValueOption<Dummy> fromOption = dummy;
            var toOption = dummy.ToValueOption();
            
            Assert.Equal(expected, fromOption.IsSome);
            Assert.Equal(expected, toOption.IsSome);
        }

        public static IEnumerable<object[]> DummyCollection()
        {
            yield return new object[] { Dummy.Create(1), true };
            yield return new object[] { Dummy.CreateNull()!, false };
        }

        #region Dummy

        public readonly struct Dummy
        {
            public readonly byte Age;

            private Dummy(byte age) =>
                Age = age;

            public static Dummy Create(byte age) =>
                new(age);

            public static Dummy? CreateNull() =>
                null;
        }

        #endregion
    }
}
