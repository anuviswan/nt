using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Shared.Utils.Helpers
{
    public class AsyncRef<T>
    {
        public T Value { get; set; }

        public AsyncRef() : this(default) { }
        public AsyncRef(T value) => Value = value;
        public static implicit operator T(AsyncRef<T> refValue) => refValue.Value;

        public static implicit operator AsyncRef<T>(T value) => new AsyncRef<T>(value);
        public override string ToString()
        {
            return Value == null ? string.Empty : Value.ToString();
        }
    }
}
