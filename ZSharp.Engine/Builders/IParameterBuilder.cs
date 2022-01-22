using Mono.Cecil;
using System.Reflection.Emit;

namespace ZSharp.Engine
{
    public interface IParameterBuilder : IParameter
    {
        //ParameterBuilder SRF { get; }

        ParameterDefinition MC { get; }

        //FunctionBuilder DeclaringFunction { get; }
    }
}
