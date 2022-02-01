using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZSharp.Core;

namespace ZSharp.Engine
{
    public class DependencyFinder 
        : BaseProcessor
        , IExpressionProcessor
    {
        private readonly GenericProcessor<ISRFResolvable> _resolver;
        private readonly GenericProcessor<ISRFCompilable> _compiler;

        private readonly Dictionary<SRFObject, ObjectInfo> _objects = new();

        internal DependencyGraph<SRFObject> DependencyGraph { get; } = new();

        public DependencyFinder(Context ctx) : base(ctx)
        {
            _resolver = new(ctx);
            _compiler = new(ctx);
        }

        public override List<BuildResult<ErrorType, ObjectInfo>> Process(List<BuildResult<ErrorType, ObjectInfo>> input)
        {
            input = base.Process(input);

            IEnumerable<IEnumerable<SRFObject>> buildOrder = DependencyGraph.GetDependencyOrder();

            Dictionary<SRFObject, BuildResult<ErrorType, ObjectInfo>> map = new();
            foreach (BuildResult<ErrorType, ObjectInfo> item in 
                buildOrder.SelectMany(
                    items =>
                    items
                    .Select(item => _objects[item])
                    .Select(_resolver.Process)
                    .Select(_compiler.Process)
                    )
                )
            {
                if (item.Value.Expression is SRFObject srf) map.Add(srf, item);
            }

            return input                
                .Select(
                    result => 
                    result.Value.Expression is SRFObject srf 
                    ? BuildResultUtils.CombineErrors(result, map[srf]) 
                    : result)
                .ToList();
        }

        public override BuildResult<ErrorType, ObjectInfo> Process(ObjectInfo @object)
        {
            if (@object.Expression is SRFObject srf) _objects.Add(srf, @object);
            return base.Process(@object);
        }

        public override void PostProcess()
        {
#if DEBUG

            using StreamWriter graphFile = File.CreateText("./deps.graph");
            foreach (KeyValuePair<SRFObject, IReadOnlySet<SRFObject>> pair in DependencyGraph)
            {
                graphFile.WriteLine((pair.Key).Name);
                foreach (SRFObject dependency in pair.Value)
                {
                    graphFile.WriteLine("  " + dependency.Name);
                }
            }

#endif
        }

        public void FindDependencies(SRFObject dependant, Expression expr)
        {
            switch (expr)
            {
                case null:
                    return;
                case SRFObject dependency:
                    DependencyGraph.AddDependency(dependant, dependency);
                    break;
                case Identifier id:
                    {
                        if (Context.Scope.GetItem<SRFObject>(id.Name) is SRFObject srfID)
                            DependencyGraph.AddDependency(dependant, srfID);
                        else
                            FindDependencies(dependant, Context.Scope.GetItem(id.Name));
                        break;
                    }

                case FunctionCall call:
                    FindDependencies(dependant, call.Callable);
                    FindDependencies(dependant, call.Argument);
                    break;
                case UnaryExpression unary:
                    {
                        if (Context.Scope.GetItem<SRFObject>(unary.Operator) is SRFObject op)
                            DependencyGraph.AddDependency(dependant, op);
                        FindDependencies(dependant, unary.Operand);
                        break;
                    }

                case Collection collection:
                    {
                        collection.Items.ForEach(expr => FindDependencies(dependant, expr));
                        break;
                    }

                case BinaryExpression binary:
                    {
                        if (Context.Scope.GetItem<SRFObject>(binary.Operator) is SRFObject op)
                            DependencyGraph.AddDependency(dependant, op);
                        FindDependencies(dependant, binary.Left);
                        FindDependencies(dependant, binary.Right);
                        break;
                    }

                case IDependencyProvider<SRFObject> provider:
                    DependencyGraph.AddDependencies(
                        dependant, 
                        provider.GetDependencies(dependant).Where(dependency => dependency is not null)
                        );
                    break;
                default:
                    DependencyGraph.AddDependencies(dependant);
                    Console.WriteLine($"Unmatched type: {expr.GetType().FullName}");
                    break;
            }
            //Expression tmp = base.Process(@object);
            //if (tmp != @object) FindDependencies(dependant, tmp);
        }

        public override BuildResult<Error, Expression?> Process(Expression expr)
        {
            if (expr is IDependencyFinder finder) return finder.Compile(this, Context);

            return new(expr);
        }
    }
}
