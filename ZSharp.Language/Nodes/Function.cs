using System.Collections.Generic;
using ZSharp.Engine;
using ZSharp.CG;

namespace ZSharp.Language
{
    internal record class Function : Node, ICompilable
    {
        internal FunctionBuilder _cgBuilder;
        
        public NodeInfo<Identifier> Name { get; set; }

        public List<NodeInfo<Identifier>> GenericParameters { get; set; }

        public List<NodeInfo<Identifier>> Parameters { get; set; }
        
        public NodeInfo<Type> Type { get; set; }

        public NodeInfo<FunctionBody> Body { get; set; }

        internal Function Create()
        {
            return Body.Object.DeclaringFunction = this;
        }

        public override string ToString()
        {
            string generic = GenericParameters.Count > 0 ? $"<{string.Join(", ", GenericParameters)}>" : string.Empty;
            string parameters = $"({string.Join(" ", Parameters)})";
            return $"func {Name}{generic}{parameters}: {Type}";
        }

        public BuildResult<Error, Node> Process(DelegateProcessor<ICompilable> proc)
        {
            BuildResult<Error, Node> result = new(this);

            //Engine.TypeReference type = proc.Engine.Evaluator.Evaluate(Type.Object).Value as Engine.TypeReference;

            //_cgBuilder = proc.Engine.Context.Module.DefineFunction(Name.Object.Name, type.CG);

            //foreach (var parameter in Parameters)
            //{
            //    _cgBuilder.DefineParameter(parameter.Object.Name, type.CG);
            //}

            //_cgBuilder.Build();

            return result;
        }

        public override Object GetCompilerObject()
        {
            throw new System.NotImplementedException();
        }
    }
}
