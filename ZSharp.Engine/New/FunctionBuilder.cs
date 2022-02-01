//using Mono.Cecil;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Reflection.Emit;
//using ZSharp.Core;

//namespace ZSharp.Engine
//{
//    public class SRFFunctionBuilder
//        : NamedItem
//        , IFunction
//        , IBuildable
//        //, IResolve
//        , ISRFCompilable
//        , IModifierProvider<string>
//    {
//        public MethodBuilder? SRF { get; private set; }

//        public MethodDefinition? MC { get; private set; }

//        public bool IsStatic => HasModifier("static");

//        public bool IsInstance => !IsStatic;

//        public Type? DeclaringType { get; set; }

//        public FunctionType Type { get; private set; }

//        private Collection? _body;

//        private readonly List<string> _modifiers = new();

//        MethodInfo? IFunction.SRF => SRF;

//        Mono.Cecil.MethodReference? IFunction.MC => MC;

//        public bool IsVirtual => HasModifier("virtual");

//        public SRFFunctionBuilder(string name, FunctionTypeDescriptor type)
//            : base(name)
//        {
//            Type = type;
//        }

//        public SRFFunctionBuilder(string name, TypeReference input, TypeReference output)
//            : this(name, new FunctionTypeDescriptor(input, output))
//        {
            
//        }

//        public BuildResult<ErrorType, Expression?> Compile(GenericProcessor<IBuildable> proc, Context ctx)
//        {
//            // SRF method definition
//            {
//                System.Reflection.MethodAttributes srfAttributes = 0;

//                if (IsStatic) srfAttributes |= System.Reflection.MethodAttributes.Static;

//                if (DeclaringType is null)
//                    SRF = ctx.Module.SRF.DefineGlobalMethod(
//                        Name,
//                        srfAttributes,
//                        Type.Output.SRF, new[] { Type.Input.SRF });
//                else
//                    SRF = DeclaringType.SRF.DefineMethod(
//                        Name,
//                        srfAttributes,
//                        Type.Output.SRF, new[] { Type.Input.SRF }
//                        );
//            }

//            // MC method definition
//            {
//                Mono.Cecil.MethodAttributes mcAttributes = 0;

//                if (IsStatic) mcAttributes |= Mono.Cecil.MethodAttributes.Static;

//                MC = new(Name, mcAttributes, Type.Output.MC);

//                if (Type.Input is not null && Type.Input.SRF != ctx.TypeSystem.Unit.SRF)
//                {
//                    MC.Parameters.Add(new ParameterDefinition(Type.Input.MC));
//                }

//                (DeclaringType?.MC ?? proc.Context.Module.MCGlobalScope).Methods.Add(MC);
//            }

//            if (DeclaringType is null)
//                ctx.Scope.AddItem<FunctionOverload>(new(this));

//            return new(this);
//        }

//        public BuildResult<ErrorType, Expression?> Compile(GenericProcessor<ISRFCompilable> proc, Context ctx)
//        {
//            if (SRF is null || MC is null || _body is null) return new(null);

//            if (HasModifier("__entrypoint")) ctx.Module.MC.EntryPoint = MC;

//            ILGenerator srf = SRF.GetILGenerator();
//            Mono.Cecil.Cil.ILProcessor mc = MC.Body.GetILProcessor();
//            var il = _body.Cast<Expression>().Select(proc.Process)
//                .Cast<Cil.IILGenerator>().ToArray();
//            foreach (Cil.IILGenerator ilGen in il)
//            {
//                foreach (Cil.ILInstruction instruction in ilGen.GetIL())
//                {
//                    instruction.MSIL.EmitTo(srf);
//                    instruction.MCIL.EmitTo(mc);
//                }
//            }

//            return new(this);
//        }

//        public BuildResult<ErrorType, Expression?> Compile(GenericProcessor<IResolvable> proc, Context ctx)
//        {
//            {
//                //SRF.SetReturnType()
//            }

//            return new(this);
//        }

//        #region Operators

//        [SurroundingOperatorOverload("{", "}")]
//        public static SRFFunctionBuilder Initialize(SRFFunctionBuilder func, Core.Collection exprs)
//        {
//            if (func._body is not null) throw new InvalidOperationException();

//            func._body = exprs;
//            return func;
//        }

//        [SurroundingOperatorOverload("{", "}")]
//        public static SRFFunctionBuilder Initialize(SRFFunctionBuilder func)
//        {
//            if (func._body is not null) throw new InvalidOperationException();

//            func._body = new Core.Collection(/*Cil.ILOpCodes.Return()*/);
//            return func;
//        }

//        #endregion

//        public IFunction? MakeGeneric(params IType[] types)
//        {
//            if (SRF is null) return null;

//            if (types is null) return this;
//            if (types.Length == 0) return this;

//            System.Type[] srfTypes = new System.Type[types.Length];

//            GenericInstanceMethod mc = new(MC);
//            for (int i = 0; i < types.Length; i++)
//            {
//                mc.GenericArguments.Add(types[i].MC);
//                srfTypes[i] = types[i].SRF;
//            }

//            return new FunctionReference(SRF.MakeGenericMethod(srfTypes), mc);
//        }

//        public bool IsCallableWith(params IType[] types)
//        {
//            if (types.Length == 0) return Type.Input == Context.CurrentContext.TypeSystem.Unit;
//            if (types.Length != 1) return false;

//            return true;
//        }

//        public IEnumerable<IType> GetParameterTypes()
//        {
//            return new IType[] { Type.Input };
//        }

//        public void AddModifier(string modifier)
//        {
//            if (HasModifier(modifier))
//                throw new InvalidOperationException($"Duplicate modifier: {modifier}");
//            _modifiers.Add(modifier);
//        }

//        public bool HasModifier(string modifier) => _modifiers.Contains(modifier);

//        [SurroundingOperatorOverload("(", ")")]
//        public Expression? Call()
//        {
//            if (Type.Input.SRF == Context.CurrentContext.TypeSystem.Unit.SRF)
//                return GetInvocableMethod()?.Invoke(null, new object[] { Unit.Value }) as Expression;
//            return GetInvocableMethod()?.Invoke(null, Array.Empty<object>()) as Expression;
//        }

//        public MethodInfo? GetInvocableMethod()
//        {
//            if (SRF is null) return null;

//            System.Type[] types = GetParameterTypes().Select(t => t.SRF).ToArray();
//            BindingFlags bindingFlags = BindingFlags.Default;
//            if (SRF.IsStatic) bindingFlags |= BindingFlags.Static;
//            bindingFlags |= SRF.IsPublic ? BindingFlags.Public : BindingFlags.NonPublic;

//            if (DeclaringType is null)
//            {
//                return Context.CurrentContext.Module.SRF
//                    .GetMethod(Name, bindingFlags, null, SRF.CallingConvention, types, null);
//            } else
//            {
//                return DeclaringType.SRF
//                    .GetMethod(Name, bindingFlags, null, SRF.CallingConvention, types, null);
//            }
//        }

//        public BuildResult<ErrorType, Expression?> Invoke(params Expression[] args)
//        {
//            BuildResult<ErrorType, Expression?> result = new(null);

//            MethodInfo? method = GetInvocableMethod();
//            return method is null
//                ? result.Error($"Could not get invocable method for \'{Name}\'")
//                : method.Invoke(null, args) is Expression expr
//                    ? result.Return<Expression?>(expr)
//                    : result.Error($"\'{Name}\' did not return a compile time value");
//        }
//    }
//}
