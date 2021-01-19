namespace Nt.Utils.Helper
{
    public class NtRef<T>
    {
        public NtRef()
        {

        }

        public NtRef(T value) => Value = value;

        public T Value { get; set; }

        public static implicit operator T(NtRef<T> refValue) => refValue.Value;

        public static implicit operator NtRef<T>(T value) =>  new NtRef<T>(value);
        public override string ToString()
        {
            return Value == null ? string.Empty : Value.ToString();
        }

    }
}
