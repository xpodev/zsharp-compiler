﻿using System;
using System.Collections.Generic;
using System.Linq;
using ZSharp.Core;

namespace ZSharp.Engine
{
    public class FunctionDescriptor
        : NamedItem
        , IObjectDescriptor
        , IModifierProvider<string>
    {
        public Expression Type { get; private set; }

        private readonly List<string> _modifiers = new();

        public List<string> ParameterNames { get; private set; }

        public List<Expression> Body { get; private set; }

        public FunctionDescriptor(string name) : base(name) { }

        public FunctionDescriptor(Pair info) : this(info.Left as Identifier)
        {
            Type = info.Right;
        }

        public void AddModifier(string modifier)
        {
            if (HasModifier(modifier))
                throw new InvalidOperationException($"Duplicate modifier: {modifier}");
            _modifiers.Add(modifier);
        }

        public bool HasModifier(string modifier) => _modifiers.Contains(modifier);

        [KeywordOverload("func")]
        public static FunctionDescriptor CreateFunction(Identifier id) => new(id.Name);

        [KeywordOverload("func")]
        public static FunctionDescriptor CreateFunction(string id) => new(id);

        [OperatorOverload(":")]
        public static FunctionDescriptor SetType(FunctionDescriptor func, Expression type)
        {
            if (func.Type is not null)
                throw new InvalidOperationException();

            func.Type = type;
            return func;
        }

        [SurroundingOperatorOverload("{", "}")]
        public static FunctionDescriptor Initialize(FunctionDescriptor func, Collection body)
        {
            if (func.Body is not null)
                throw new InvalidOperationException();

            func.Body = new(body);

            return func;
        }

        [SurroundingOperatorOverload("{", "}")]
        public static FunctionDescriptor Initialize(FunctionDescriptor func)
            => Initialize(func, Collection.Empty);

        [SurroundingOperatorOverload("(", ")")]
        public FunctionDescriptor SetParameters(Collection items)
        {
            if (ParameterNames is not null)
                throw new InvalidOperationException();
            ParameterNames = new(items.Cast<Identifier>().Select(id => id.Name));
            return this;
        }

        [SurroundingOperatorOverload("(", ")")]
        public FunctionDescriptor SetParameters(Identifier item)
        {
            return SetParameters(new Collection(item));
        }

        [SurroundingOperatorOverload("(", ")")]
        public FunctionDescriptor SetParameters()
        {
            return SetParameters(Collection.Empty);
        }

        [KeywordOverload("__entrypoint")]
        public static FunctionDescriptor MakeEntryPoint(FunctionDescriptor func)
        {
            func.AddModifier("__entrypoint");

            return func;
        }

        [KeywordOverload("static")]
        public static FunctionDescriptor MakeStatic(FunctionDescriptor func)
        {
            func.AddModifier("static");

            return func;
        }

        public Expression Compile(GenericProcessor<IObjectDescriptor> proc, Context ctx)
        {
            Function func = new(this);

            ParameterNames ??= new();
            Type ??= ctx.TypeSystem.Unit as Expression;

            return func;
        }
    }
}