namespace ZSharp.Core
{
    public class Literal : Expression
    {
        public object Value { get; set; }

        public Literal(object value)
        {
            Value = value;
        }
    }

    public class Literal<T> : Literal
    {
        public new T Value { get; set; }

        public Literal(T value) : base(value)
        {
            Value = value;
        }
    }
}
