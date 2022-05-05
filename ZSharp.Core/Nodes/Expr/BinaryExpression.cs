namespace ZSharp.Core
{
    public record class BinaryExpression(
        NodeInfo<Expression> Left, 
        NodeInfo<Expression> Right, 
        string Operator
        ) : Expression
    {
        public override Object GetCompilerObject(IContext ctx)
        {
            throw new System.NotImplementedException();
        }
    }
}
