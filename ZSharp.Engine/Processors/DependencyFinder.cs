﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public override void PostProcess()
        {
            base.PostProcess();

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

            IEnumerable<IEnumerable<SRFObject>> buildOrder = DependencyGraph.GetDependencyOrder();

            int level = 0;
            foreach (IEnumerable<SRFObject> items in buildOrder)
            {
                _ = items.Select(_resolver.Process).Select(Bind(_compiler.Process));
                level++;
            }
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
                        if (Context.Scope.GetItem<SRFObject>(unary.Operator.Name) is SRFObject op)
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
                        if (Context.Scope.GetItem<SRFObject>(binary.Operator.Name) is SRFObject op)
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
    }
}
