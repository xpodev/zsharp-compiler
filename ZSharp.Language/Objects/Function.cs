using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSharp.Language
{
    public class Function 
        : NodeWrapper<FunctionNode>
        , IBuilder
        , INamedItem
    {
        protected internal CG.FunctionBuilder _cg;

        private readonly List<Parameter> _parameters = new();
        //protected SR.FunctionBuilder _sr;

        public IReadOnlyList<IParameterBuilder> Parameters => _parameters;

        public string Name { get; set; }

        public Function(FunctionNode node) : base(node)
        {
            Name = node.Name.Object.Name;
        }

        public IParameterBuilder AddParameter(Parameter parameter)
        {
            _parameters.Add(parameter);
            return parameter;
        }

        public void Build()
        {
            _cg.Name = Name;
        }
    }
}
