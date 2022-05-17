using System;
using System.Collections.Generic;
using System.Linq;
using ZSharp.Core;

namespace ZSharp.Engine.Processors
{
    internal class ContextPreparationProcess : Processor<IContextPreparationItem>
    {
        public ContextPreparationProcess(Engine engine) : base(engine)
        {
        }

        public override BuildResult<Error, Node> Process(Node node)
        {
            BuildResult<ErrorType, Node> result = new(node);

            ProjectScope ctx = Engine.Context;

            if (node is ModifiedObject modifiedObject)
            {
                foreach (var modifierName in ((IEnumerable<NodeInfo<Identifier>>)modifiedObject.Modifiers).Reverse())
                {
                    if (ctx.GetObject(modifierName.Object.Name) is not NodeModifier modifier)
                    {
                        result.Error($"Modifier {modifierName.Object.Name} not found");
                    }
                    else
                    {
                        node = modifier.Modify(node);
                    }
                }
            }

            result = result.CombineErrors(base.Process(node));

            return result;
        }
    }
}
