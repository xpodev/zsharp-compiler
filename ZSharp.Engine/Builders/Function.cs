using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ZSharp.Core;

namespace ZSharp.Engine
{
    public class Function
        : SRFObject
        , IFunction
        , IBuildable
        , IDependencyFinder
        , ISRFResolvable
        , ISRFCompilable
        , IResolvable
        //, ICompilable
    {
        private readonly FunctionDescriptor _info;

        public MethodBuilder SRF { get; private set; }

        public MethodDefinition MC { get; private set; }

        public List<IParameter> Parameters { get; } = new();

        MethodInfo IFunction.SRF => SRF;

        Mono.Cecil.MethodReference IFunction.MC => MC;

        public List<Expression> Body { get; }

        public FunctionType Type { get; private set; }

        //public LambdaFunction InnerFunction { get; private set; }

        //IEnumerable<IParameter> IFunction.Parameters => Parameters;

        public bool IsStatic => _info.HasModifier("static");

        public bool IsInstance => !IsStatic;

        public bool IsVirtual => _info.HasModifier("virtual");

        public bool IsResolved => false; // _info.Type is IType;

        public Type DeclaringType { get; private set; }

        public Function(FunctionDescriptor info) : base(info.Name)
        {
            _info = info;
            Body = new(_info.Body);
        }

        public Function(string name, FunctionType type, FunctionDescriptor info)
            : base(name)
        {
            Type = type;
            Body = new();
            _info = info;
        }

        public IEnumerable<IType> GetParameterTypes()
        {
            return Parameters.Select(p => p.Type);
        }

        public bool IsCallableWith(params IType[] types)
        {
            if (types.Length != Parameters.Count) return false;

            foreach ((IType, IType) item in types.Zip(GetParameterTypes()))
            {
                if (item.Item2.SRF.IsAssignableFrom(item.Item1.SRF)) return false;
            }

            return true;
        }

        public IFunction MakeGeneric(params IType[] types)
        {
            if (types is null) return this;
            if (types.Length == 0) return this;

            System.Type[] srfTypes = new System.Type[types.Length];

            GenericInstanceMethod mc = new(MC);
            for (int i = 0; i < types.Length; i++)
            {
                mc.GenericArguments.Add(types[i].MC);
                srfTypes[i] = types[i].SRF;
            }

            return new FunctionReference(SRF.MakeGenericMethod(srfTypes), mc);
        }

        public string Compile(GenericProcessor<IBuildable> proc, Context ctx)
        {
            // SRF method definition
            {
                System.Reflection.MethodAttributes srfAttributes = 0;

                if (IsStatic) srfAttributes |= System.Reflection.MethodAttributes.Static;

                if (DeclaringType is null)
                    SRF = ctx.Module.SRF.DefineGlobalMethod(
                        Name,
                        srfAttributes,
                        null, null
                        );
                else
                    SRF = DeclaringType.SRF.DefineMethod(
                        Name,
                        srfAttributes,
                        null, null
                        );
            }

            // MC method definition
            {
                Mono.Cecil.MethodAttributes mcAttributes = 0;

                if (IsStatic) mcAttributes |= Mono.Cecil.MethodAttributes.Static;

                MC = new(Name, mcAttributes, ctx.TypeSystem.Void.MC);

                //if (Type.Input is not null && Type.Input.SRF != ctx.TypeSystem.Unit.SRF)
                //{
                //    MC.Parameters.Add(new ParameterDefinition(Type.Input.MC));
                //}

                (DeclaringType?.MC ?? ctx.Module.MCGlobalScope).Methods.Add(MC);
            }

            // should always be true (cuz member functions are methods)
            if (DeclaringType is null)
                ctx.Scope.AddItem<SRFFunctionOverload>(new(this));

            return null;
        }

        public string Compile(GenericProcessor<ISRFResolvable> proc, Context ctx)
        {
            IType type = Type is not null ? Type : (Expression)ctx.Evaluate(_info.Type) as IType;

            if (type is null)
                throw new Exception($"{Name}'s type expression evaluated to null");

            if (type is not FunctionType fType)
                fType = Type ??= new(type, ctx.TypeSystem.Void);

            //LambdaFunction innerFunction;
            //FunctionType _fType = fType;
            //List<string> parameterNames = new(_info.ParameterNames);
            //while ((_fType = _fType.Output as FunctionType) is not null)
            //{
            //    innerFunction = new($"{Name}::InnerFunction", DeclaringType, _fType.Output);
            //    if (_fType.Input is Collection tuple)
            //    {
            //        IEnumerable<IType> tupleType = tuple.Select(proc.Process).Cast<IType>();
            //        innerFunction.Parameters.AddRange(
            //            tupleType.Zip(parameterNames).Select(
            //                (param, i) => new Parameter(param.Second, param.First, i, innerFunction)
            //                )
            //            );
            //        parameterNames.RemoveRange(0, tuple.Count);
            //    }
            //    else
            //    {
            //        innerFunction.AddParameter(parameterNames[0], _fType.Input);
            //        parameterNames.RemoveAt(0);
            //    }
            //    innerFunction.Build(new IL()
            //    {
            //        Cil.ILOpCodes.Return()
            //    }, ctx);
            //}

            {
                List<IType> parameterTypes = new();
                if (fType.Input is Collection tuple)
                    parameterTypes.AddRange(tuple.Select(o => o.Expression).Cast<IType>());
                else if (fType.Input is GenericTypeOverload generic)
                    parameterTypes.Add(generic.NonGenericType);
                else if (fType.Input != ctx.TypeSystem.Unit)
                    parameterTypes.Add(fType.Input);

                SRF.SetParameters(parameterTypes.Select(t => t.SRF).ToArray());
                SRF.SetReturnType(fType.Output.SRF);

                for (int i = 0; i < parameterTypes.Count; i++)
                {
                    Parameters.Add(new Parameter(_info.ParameterNames[i], parameterTypes[i], i, this));
                    MC.Parameters.Add(new(
                        _info.ParameterNames[i],
                        Mono.Cecil.ParameterAttributes.None,
                        parameterTypes[i].MC
                        ));
                }

                MC.ReturnType = fType.Output.MC;
            }

            //if (types.Where(t => t != ctx.TypeSystem.Unit).Count() != _parameterNames.Count)
            //    throw new Exception($"{Name} parameter names & types count mismatch (expected {types.Count}, got {_parameterNames.Count})");

            //foreach (IParameter parameter in Parameters)
            //{
            //    ParameterBuilder srf = SRF.DefineParameter(
            //        parameter.Position + 1,
            //        System.Reflection.ParameterAttributes.None,
            //        parameter.Name
            //        );
            //    ParameterDefinition mc = new(
            //        parameter.Name,
            //        Mono.Cecil.ParameterAttributes.None,
            //        parameter.Type.MC
            //        );
            //    MC.Parameters.Add(mc);
            //}

            return null;
        }

        public string Compile(GenericProcessor<ISRFCompilable> proc, Context ctx)
        {
            if (_info?.HasModifier("__entrypoint") ?? false) ctx.Module.MC.EntryPoint = MC;

            SearchScope scope = ctx.Scope.EnterScope();

            foreach (IParameter parameter in Parameters)
            {
                scope.AddItem(parameter);
            }

            ILGenerator srf = SRF.GetILGenerator();
            Mono.Cecil.Cil.ILProcessor mc = MC.Body.GetILProcessor();
            var il = _info.Body.Select(ctx.Evaluate).Select(res => (Expression)res)
                .ToArray();
            foreach (Cil.IILGenerator ilGen in il)
            {
                foreach (Cil.ILInstruction instruction in ilGen.GetIL())
                {
                    instruction.SRF.EmitTo(srf);
                    instruction.MC.EmitTo(mc);
                }
            }

            ctx.Scope.ExitScope();

            return null;
        }

        public string Compile(GenericProcessor<IResolvable> proc, Context ctx)
        {
            {
                //SRF.SetReturnType()
            }

            return null;
        }

        [SurroundingOperatorOverload("(", ")")]
        public object Invoke(Expression expr)
        {
            object obj = expr is Literal literal ? literal.Value : expr;
            return GetInvocableMethod().Invoke(null, new object[] { obj });
        }

        public Result<string, Expression> Invoke(params object[] args)
        {
            MethodInfo method = GetInvocableMethod();
            return method is null
                ? new($"Could not get invocable method for \'{Name}\'", null)
                : method.Invoke(null, args) is Expression expr 
                    ? new(expr) 
                    : new($"\'{Name}\' did not return a compile time value", null);
        }

        public MethodInfo GetInvocableMethod()
        {
            System.Type[] types = GetParameterTypes().Select(t => t.SRF).ToArray();
            BindingFlags bindingFlags = BindingFlags.Default;
            if (SRF.IsStatic) bindingFlags |= BindingFlags.Static;
            bindingFlags |= SRF.IsPublic ? BindingFlags.Public : BindingFlags.NonPublic;

            if (DeclaringType is null)
            {
                return Context.CurrentContext.Module.SRF
                    .GetMethod(Name, bindingFlags, null, SRF.CallingConvention, types, null);
            }
            else
            {
                return DeclaringType.SRF
                    .GetMethod(Name, bindingFlags, null, SRF.CallingConvention, types, null);
            }
        }

        public string Compile(GenericProcessor<IDependencyFinder> proc, Context ctx)
        {
            DependencyFinder finder = proc as DependencyFinder;
            finder.FindDependencies(this, _info.Type);

            return null;
        }
    }
}
