using ZSharp.Core;

namespace ZSharp.Engine
{
    public class FunctionTypeDescriptor 
        : Expression
        , ISRFResolvable
    {
        public Expression Input { get; }

        public Expression Output { get; }

        public FunctionTypeDescriptor(Expression input, Expression output)
        {
            Input = input;
            Output = output;
        }

        public BuildResult<ErrorType, Expression?> Compile(GenericProcessor<ISRFResolvable> proc, Context ctx)
        {
            BuildResult<ErrorType, Expression?> result = new(this);

            IType? inputType, outputType;

            if (Input is null) 
                inputType = ctx.TypeSystem!.Unit;
            else {
                BuildResult<ErrorType, Expression?> input = ctx.Evaluate(Input);
                result.CombineErrors(input);
                inputType = input.Value as IType;
            }

            if (Output is null) 
                outputType = ctx.TypeSystem!.Unit;
            else
            {
                BuildResult<ErrorType, Expression?> output = ctx.Evaluate(Output);
                result.CombineErrors(output);
                outputType = output.Value as IType;
            }

            if (inputType is null)
                result.Error("Input type evaluated to null");
            if (outputType is null)
                result.Error("Output type evaluated to null");

            if (result.HasErrors)
                return result;
            return new(new FunctionType(inputType!, outputType!));
        }

        [OperatorOverload("->")]
        public static FunctionTypeDescriptor CreateFunctionType(Expression input, Expression output)
        {
            return new(input, output);
        }
    }
}
