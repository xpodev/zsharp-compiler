using System.Collections.Generic;

namespace ZSharp.Engine.Cil
{
    public static class ILBuilderExtensions
    {
        #region Load Item(s)

        public static ILBuilder LoadItem(this ILBuilder il, INamedItem item) =>
            item switch
            {
                IParameter parameter => il.LoadItem(parameter),
                _ => throw new System.ArgumentException("Invalid type", nameof(item))
            };

        public static ILBuilder LoadItemOptimized(this ILBuilder il, INamedItem item) =>
            item switch
            {
                IParameter parameter => il.LoadItemOptimized(parameter),
                _ => throw new System.ArgumentException("Invalid type", nameof(item))
            };

        public static ILBuilder LoadItems(this ILBuilder il, params INamedItem[] items) =>
            il.LoadItems((IEnumerable<INamedItem>)items);

        public static ILBuilder LoadItemsOptimized(this ILBuilder il, params INamedItem[] items) =>
            il.LoadItemsOptimized((IEnumerable<INamedItem>)items);

        public static ILBuilder LoadItems(this ILBuilder il, IEnumerable<INamedItem> items)
        {
            foreach (INamedItem item in items)
            {
                il.LoadItem(item);
            }
            return il;
        }

        public static ILBuilder LoadItemsOptimized(this ILBuilder il, IEnumerable<INamedItem> items)
        {
            foreach (INamedItem item in items)
            {
                il.LoadItemOptimized(item);
            }
            return il;
        }

        public static ILBuilder LoadItem(this ILBuilder il, IParameterBuilder parameter)
        {
            il.Append(ILOpCodes.LoadArgument(parameter));
            return il;
        }

        public static ILBuilder LoadItemOptimized(this ILBuilder il, IParameterBuilder parameter)
        {
            il.Append(ILOpCodes.LoadArgumentOptimized(parameter));
            return il;
        }

        #endregion

        #region Call Constructor

        public static ILBuilder NewObject(this ILBuilder il, IType type, params INamedItem[] args)
            => il.NewObject(type, (IEnumerable<INamedItem>)args);

        public static ILBuilder NewObject(this ILBuilder il, IType type, IEnumerable<INamedItem> args)
        {
            il.LoadItems(args);
            il.Emit(OpCodes.Newobj, type);
            return il;
        }

        public static ILBuilder NewObjectOptimized(this ILBuilder il, IType type, params INamedItem[] args)
            => il.NewObjectOptimized(type, (IEnumerable<INamedItem>)args);

        public static ILBuilder NewObjectOptimized(this ILBuilder il, IType type, IEnumerable<INamedItem> args)
        {
            il.LoadItemsOptimized(args);
            il.Emit(OpCodes.Newobj, type);
            return il;
        }

        #endregion

        #region Function Calls

        public static ILBuilder Call(this ILBuilder il, IFunction func, params INamedItem[] args)
            => il.Call(func, (IEnumerable<INamedItem>)args);

        public static ILBuilder Call(this ILBuilder il, IFunction func, IEnumerable<INamedItem> args)
        {
            il.LoadItems(args);
            il.Emit(func.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, func);
            return il;
        }

        public static ILBuilder CallOptimized(this ILBuilder il, IFunction func, params INamedItem[] args)
            => il.CallOptimized(func, (IEnumerable<INamedItem>)args);

        public static ILBuilder CallOptimized(this ILBuilder il, IFunction func, IEnumerable<INamedItem> args)
        {
            il.LoadItemsOptimized(args);
            il.Emit(func.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, func);
            return il;
        }

        #endregion
    }
}
