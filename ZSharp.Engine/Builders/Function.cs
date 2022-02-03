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

        public FunctionTypeDescriptor Type { get; private set; }

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

        public Function(string name, FunctionTypeDescriptor type, FunctionDescriptor info)
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

        public BuildResult<ErrorType, Expression?> Compile(GenericProcessor<IBuildable> proc, Context ctx)
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

                MC = new(Name, mcAttributes, ctx.TypeSystem!.Void.MC);

                //if (Type.Input is not null && Type.Input.SRF != ctx.TypeSystem.Unit.SRF)
                //{
                //    MC.Parameters.Add(new ParameterDefinition(Type.Input.MC));
                //}

                (DeclaringType?.MC ?? ctx.Module.MCGlobalScope).Methods.Add(MC);
            }

            // should always be true (cuz member functions are methods)
            if (DeclaringType is null)
                ctx.Scope.AddItem<SRFFunctionOverload>(new(this));

            return new(this);
        }

        public BuildResult<ErrorType, Expression?> Compile(GenericProcessor<ISRFResolvable> proc, Context ctx)
        {
            BuildResult<ErrorType, Expression?> typeResult = proc.Process(Type ?? _info.Type);
            if (typeResult.HasErrors) return typeResult.Return<Expression?>(this);

            if (typeResult.Value is not FunctionType fType)
                fType = new((typeResult.Value as IType)!, ctx.TypeSystem!.Unit);

            {
                List<IType> parameterTypes = new();
                if (fType.Input is Collection tuple)
                    parameterTypes.AddRange(tuple.Select(o => o.Expression).Cast<IType>());
                else if (fType.Input is GenericTypeOverload generic)
                    parameterTypes.Add(generic.NonGenericType);
                else if (fType.Input != ctx.TypeSystem!.Unit)
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

            return typeResult.Return<Expression?>(this);
        }

        public BuildResult<ErrorType, Expression?> Compile(GenericProcessor<ISRFCompilable> proc, Context ctx)
        {
            BuildResult<ErrorType, Expression?> result = new(this);

            if (_info?.HasModifier("__entrypoint") ?? false) ctx.Module.MC.EntryPoint = MC;

            SearchScope scope = ctx.Scope.EnterScope();

            foreach (IParameter parameter in Parameters)
            {
                scope.AddItem(parameter);
            }

            ILGenerator srf = SRF.GetILGenerator();
            Mono.Cecil.Cil.ILProcessor mc = MC.Body.GetILProcessor();
            var il = BuildResultUtils.CombineResults(_info.Body.Select(ctx.Evaluate));
            if (!il.HasErrors)
            {
                foreach (Cil.IILGenerator ilGen in il.Value)
                {
                    foreach (Cil.ILInstruction instruction in ilGen.GetIL())
                    {
                        instruction.MSIL.EmitTo(srf);
                        instruction.MCIL.EmitTo(mc);
                    }
                }
            }

            ctx.Scope.ExitScope();

            return BuildResultUtils.CombineErrors(result, il);
        }

        public BuildResult<ErrorType, Expression?> Compile(GenericProcessor<IResolvable> proc, Context ctx)
        {
            {
                //SRF.SetReturnType()
            }

            return new(this);
        }

        [SurroundingOperatorOverload("(", ")")]
        public object Invoke(Expression expr)
        {
            object obj = expr is Literal literal ? literal.Value : expr;
            return GetInvocableMethod().Invoke(null, new object[] { obj });
        }

        public BuildResult<ErrorType, Expression?> Invoke(params Expression[] args)
        {
            BuildResult<ErrorType, Expression?> result = new(null);

            MethodInfo method = GetInvocableMethod();
            return method is null
                ? result.Error($"Could not get invocable method for \'{Name}\'")
                : method.Invoke(null, args) is Expression expr 
                ? result.Return<Expression?>(expr) 
                : result.Error($"\'{Name}\' did not return a compile time value");
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

        public BuildResult<ErrorType, Expression?> Compile(DependencyFinder finder, Context ctx)
        {
            finder.FindDependencies(this, _info.Type);

            return new(this);
        }
    }
}
