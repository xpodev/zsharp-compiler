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
}
