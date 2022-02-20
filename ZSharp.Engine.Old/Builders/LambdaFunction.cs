//using Mono.Cecil;
//using MCIL = Mono.Cecil.Cil;
//using MSIL = System.Reflection.Emit;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Reflection.Emit;
//using Mono.Cecil.Cil;
//using ZSharp.Engine.Cil;

//namespace ZSharp.Engine
//{
//    public class LambdaFunction 
//        : NamedItem
//        , IFunction
//    {
//        public readonly List<INamedItem> _capturedItems = new();

//        public Type CaptureClass { get; }

//        public TypeBuilder SRFCaptureClass { get; }

//        public TypeDefinition MCCaptureClass { get; }

//        public MethodBuilder SRF { get; }

//        public MethodDefinition MC { get; }

//        MethodInfo IFunction.SRF => SRF;

//        MethodReference IFunction.MC => MC;

//        public List<IParameter> Parameters { get; } = new();

//        public bool IsStatic => false;

//        public bool IsInstance => true;

//        public Type DeclaringType { get; }

//        IType IFunction.DeclaringType => DeclaringType;

//        public LambdaFunction(string name, Type parent, IType returnType) : base(name)
//        {
//            DeclaringType = parent;

//            CaptureClass = new();

//            if (parent is not null)
//                SRFCaptureClass = parent.SRF.DefineNestedType(
//                    name + ".CaptureClass",
//                    System.Reflection.TypeAttributes.NotPublic
//                    );
//            else
//                SRFCaptureClass = Context.CurrentContext.Module.SRF.DefineType(
//                    name + ".CaptureClass",
//                    System.Reflection.TypeAttributes.NotPublic
//                    );
//            MCCaptureClass = new(null, name + ".CaptureClass", Mono.Cecil.TypeAttributes.NotPublic);

//            SRF = SRFCaptureClass.DefineMethod(
//                "LambdaImplementation", 
//                System.Reflection.MethodAttributes.Public,
//                returnType.SRF, null
//                );
//            MC = new("LambdaImplementation", Mono.Cecil.MethodAttributes.Public, returnType.MC);
//        }

//        public void AddParameter(string name, IType type) =>
//            Parameters.Add(new Parameter(name, type, Parameters.Count, this));

//        public void Build(IL body, Context ctx)
//        {
//            System.Type[] srfParameters = Parameters.Select(p => p.Type.SRF).ToArray();
//            SRF.SetParameters(srfParameters);

//            ConstructorBuilder srfConstructor = SRFCaptureClass.DefineConstructor(
//                System.Reflection.MethodAttributes.Public,
//                CallingConventions.HasThis,
//                srfParameters
//                );
//            MethodDefinition mcConstructor = new(
//                ".ctor",
//                Mono.Cecil.MethodAttributes.Public, 
//                ctx.TypeSystem.Void.MC
//                );

//            ILGenerator srfIL = srfConstructor.GetILGenerator();
//            ILProcessor mcIL = mcConstructor.Body.GetILProcessor();

//            FieldBuilder[] srfFields = new FieldBuilder[Parameters.Count];
//            FieldDefinition[] mcFields = new FieldDefinition[Parameters.Count];

//            foreach (IParameter parameter in Parameters)
//            {
//                SRF.DefineParameter(
//                    parameter.Position,
//                    System.Reflection.ParameterAttributes.None,
//                    parameter.Name
//                    );

//                MC.Parameters.Add(
//                    new(
//                        parameter.Name,
//                        Mono.Cecil.ParameterAttributes.None,
//                        parameter.Type.MC
//                        )
//                    );

//                FieldBuilder srfField = srfFields[parameter.Position] = SRFCaptureClass.DefineField(
//                    parameter.Name,
//                    parameter.Type.SRF,
//                    System.Reflection.FieldAttributes.Private | 
//                    System.Reflection.FieldAttributes.InitOnly
//                    );
//                FieldDefinition mcField = mcFields[parameter.Position] = new(
//                    parameter.Name,
//                    Mono.Cecil.FieldAttributes.Private | 
//                    Mono.Cecil.FieldAttributes.InitOnly,
//                    parameter.Type.MC
//                    );

//                ParameterDefinition mcConsParam = new(
//                    parameter.Name,
//                    Mono.Cecil.ParameterAttributes.None,
//                    parameter.Type.MC
//                    );

//                mcConstructor.Parameters.Add(mcConsParam);

//                srfIL.Emit(MSIL.OpCodes.Ldarg, parameter.Position);
//                srfIL.Emit(MSIL.OpCodes.Stfld, srfField);

//                mcIL.Emit(MCIL.OpCodes.Ldarg, mcConsParam);
//                mcIL.Emit(MCIL.OpCodes.Stfld, mcField);
//            }

//            srfIL = SRF.GetILGenerator();
//            mcIL = MC.Body.GetILProcessor();

//            foreach (Cil.ILInstruction instruction in body)
//            {
//                if (instruction.Operand is IParameter pRef)
//                    Cil.ILOpCodes.LoadArgument(pRef).InsertInto(srfIL, mcIL);
//                else
//                    instruction.InsertInto(srfIL, mcIL);
//            }

//            SRFCaptureClass.CreateType();
//            (DeclaringType?.MC?.NestedTypes ?? ctx.Module.MC.Types).Add(MCCaptureClass);

//            MCCaptureClass.Methods.Add(MC);
//            MCCaptureClass.Methods.Add(mcConstructor);
//        }

//        public void GenerateCreateDelegate()
//        {
//            ILBuilder il = null; // todo: get outer function builder
//            il.NewObject(CaptureClass, _capturedItems);
//        }

//        public IEnumerable<IType> GetParameterTypes()
//        {
//            return Parameters.Select(p => p.Type);
//        }

//        public bool IsCallableWith(params IType[] types)
//        {
//            if (types.Length != Parameters.Count) return false;

//            foreach ((IType, IType) item in types.Zip(GetParameterTypes()))
//            {
//                if (item.Item2.SRF.IsAssignableFrom(item.Item1.SRF)) return false;
//            }

//            return true;
//        }

//        public IFunction MakeGeneric(params IType[] types)
//        {
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
//    }
//}
