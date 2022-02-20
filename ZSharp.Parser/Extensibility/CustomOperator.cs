namespace ZSharp.Parser.Extensibility
{
    public struct CustomOperator
    {
        public string Symbol { get; set; }

        public OperatorType Type { get; set; }
        
        public int Precedence { get; set; }

        public CustomOperator(string symbol, OperatorType type, int precedence)
        {
            Symbol = symbol;
            Type = type;
            Precedence = precedence;
        }
    }
}
