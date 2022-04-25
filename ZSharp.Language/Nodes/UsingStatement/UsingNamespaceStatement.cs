using ZSharp.Engine;
using System;

namespace ZSharp.Language
{
    internal record class UsingNamespaceStatement(FullyQualifiedName FQN) : UsingStatement
    {
        public override BuildResult<Error, Node> Process(DelegateProcessor<IContextPreparationItem> proc)
        {
            BuildResult<Error, Node> result = new(this);

            ProjectScope pctx = proc.Engine.Context;
            DocumentScope ctx = pctx.Document;
            IContext @namespace = pctx;

            foreach (NodeInfo<Identifier> part in FQN.Parts)
            {
                @namespace = @namespace.GetObject(part.Object.Name) as Namespace;
                if (@namespace is null)
                {
                    result.Error("Namespace not found: " + part.Object.Name);
                    break;
                }
            }

            if (@namespace is not null) result.Errors.AddRange(ctx.ImportScope(@namespace as Namespace));

            return result;
        }
    }
}
