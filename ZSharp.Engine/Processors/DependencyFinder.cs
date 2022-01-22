using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSharp.Core;

namespace ZSharp.Engine
{
    internal class DependencyFinder : GenericProcessor<IDependencyFinder>
    {
        private readonly GenericProcessor<ISRFResolvable> _resolver;
        private readonly GenericProcessor<ISRFCompilable> _compiler;

        public DependencyGraph<SRFObject> DependencyGraph { get; } = new();

        public DependencyFinder(Context ctx) : base(ctx)
        {
            _resolver = new(ctx);
            _compiler = new(ctx);
        }

        public override Expression Process(Expression expr)
        {
            if (expr is IDependencyFinder dependant) return dependant.Compile(this, Context);
            return base.Process(expr);
        }

        public override void PostProcess()
        {
            base.PostProcess();

#if DEBUG

            using StreamWriter graphFile = File.CreateText("./deps.graph");
            foreach (KeyValuePair<SRFObject, IReadOnlySet<SRFObject>> pair in DependencyGraph)
            {
                graphFile.WriteLine(pair.Key.Name);
                foreach (SRFObject dependency in pair.Value)
                {
                    graphFile.WriteLine("  " + dependency.Name);
                }
            }

#endif

            IEnumerable<IEnumerable<SRFObject>> buildOrder = DependencyGraph.GetDependencyOrder();

            int level = 0;
            foreach (IEnumerable<SRFObject> items in buildOrder)
            {
                _ = items.Select(_resolver.Process).Select(_compiler.Process);
                level++;
            }
        }

        public void FindDependencies(SRFObject dependant, Expression expr)
        {
            if (expr is null) return;
            else if (expr is SRFObject dependency)
            {
                DependencyGraph.AddDependency(dependant, dependency);
            }
            else if (expr is Identifier id)
            {
                if (Context.Scope.GetItem<SRFObject>(id.Name) is SRFObject srfID)
                    DependencyGraph.AddDependency(dependant, srfID);
                else
                    FindDependencies(dependant, Context.Scope.GetItem(id.Name));
            } 
            else if (expr is FunctionCall call)
            {
                FindDependencies(dependant, call.Callable);
                FindDependencies(dependant, call.Argument);
            } 
            else if (expr is UnaryExpression unary)
            {
                if (Context.Scope.GetItem<SRFObject>(unary.Operator.Name) is SRFObject op)
                    DependencyGraph.AddDependency(dependant, op);
                FindDependencies(dependant, unary.Operand);
            } 
            else if (expr is Collection collection)
            {
                collection.Items.ForEach(expr => FindDependencies(dependant, expr));
            } 
            else if (expr is BinaryExpression binary)
            {
                if (Context.Scope.GetItem<SRFObject>(binary.Operator.Name) is SRFObject op)
                    DependencyGraph.AddDependency(dependant, op);
                FindDependencies(dependant, binary.Left);
                FindDependencies(dependant, binary.Right);
            } 
            else if (expr is IDependencyProvider<SRFObject> provider)
            {
                DependencyGraph.AddDependencies(dependant, provider.GetDependencies(dependant));
            }
            else
            {
                DependencyGraph.AddDependencies(dependant);
                Console.WriteLine($"Unmatched type: {expr.GetType().FullName}");
            }
            Expression tmp = base.Process(expr);
            if (tmp != expr) FindDependencies(dependant, tmp);
        }
    }
}
