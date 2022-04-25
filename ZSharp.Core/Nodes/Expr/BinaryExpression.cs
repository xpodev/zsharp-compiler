namespace ZSharp.Core
{
    public record class BinaryExpression(
        NodeInfo<Expression> Left, 
        NodeInfo<Expression> Right, 
        string Operator
        ) : Expression
    {
        public override Object GetCompilerObject()
        {
            throw new System.NotImplementedException();
        }
    }
}
