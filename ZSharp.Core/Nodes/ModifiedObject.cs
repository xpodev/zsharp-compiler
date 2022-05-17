﻿namespace ZSharp.Core
{
    using System.Collections.Generic;
    using ModifierType = Identifier;

    /// <summary>
    /// Describes a document object with modifiers.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public record class ModifiedObject : Node
    {
        /// <summary>
        /// The object that is modified.
        /// </summary>
        public NodeInfo Object { get; set; }

        /// <summary>
        /// List of modifiers in the same order as they appear in the file which
        /// is the reversed order in which they are applied.
        /// </summary>
        public List<NodeInfo<ModifierType>> Modifiers { get; } = new();

        /// <summary>
        /// Inserts a modifier in the given position.
        /// </summary>
        /// <param name="index">The new index of the modifier.</param>
        /// <param name="modifier">The modifier to insert.</param>
        /// <returns>A modified object with the new modifiers.</returns>
        public ModifiedObject WithInsertModifierAt(int index, NodeInfo<ModifierType> modifier)
        {
            Modifiers.Insert(index, modifier);
            return this;
        }

        /// <summary>
        /// Inserts a modifier in the first position.
        /// </summary>
        /// <param name="modifier">The modifier to insert.</param>
        /// <returns>A modified object with the new modifiers.</returns>
        public ModifiedObject WithInsertModifier(NodeInfo<ModifierType> modifier) =>
            WithInsertModifierAt(0, modifier);

        /// <summary>
        /// Inserts a modifier in the last position.
        /// </summary>
        /// <param name="modifier">The modifier to insert.</param>
        /// <returns>A modified object with the new modifiers.</returns>
        public ModifiedObject WithAppendModifier(NodeInfo<ModifierType> modifier)
        {
            Modifiers.Add(modifier);
            return this;
        }

        /// <summary>
        /// Inserts many modifiers at the given position.
        /// </summary>
        /// <param name="index">The position at which to insert the modifiers.</param>
        /// <param name="modifiers">The modifiers to insert.</param>
        /// <returns>A modified object with the new modifiers.</returns>
        public ModifiedObject WithInsertModifiersAt(int index, IEnumerable<NodeInfo<ModifierType>> modifiers)
        {
            Modifiers.InsertRange(index, modifiers);
            return this;
        }

        /// <summary>
        /// Inserts many modifiers at the beginning.
        /// </summary>
        /// <param name="modifiers">The modifiers to insert.</param>
        /// <returns>A modified object with the new modifiers.</returns>
        public ModifiedObject WithInsertModifiers(IEnumerable<NodeInfo<ModifierType>> modifiers)
            => WithInsertModifiersAt(0, modifiers);

        /// <summary>
        /// Inserts many modifiers at the end.
        /// </summary>
        /// <param name="modifiers">The modifiers to append.</param>
        /// <returns>A modified object with the new modifiers.</returns>
        public ModifiedObject WithAppendModifiers(IEnumerable<NodeInfo<ModifierType>> modifiers)
        {
            Modifiers.AddRange(modifiers);
            return this;
        }

        public override string ToString()
        {
            return Modifiers.Count == 0 ? Object.ToString() : $"{string.Join(' ', Modifiers)} {Object}";
        }

        public override Object GetCompilerObject(IContext ctx)
        {
            if (Modifiers.Count == 0) return Object.Object.GetCompilerObject(ctx);

            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// Describes a document object with modifiers.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public record class ModifiedObject<T> : ModifiedObject
        where T : Node
    {
        /// <summary>
        /// The object that is modified.
        /// </summary>
        public new NodeInfo<T> Object { get; set; }

        /// <summary>
        /// Creates a new modified object.
        /// </summary>
        /// <param name="object">The object to be modified.</param>
        public ModifiedObject(NodeInfo<T> @object)
        {
            base.Object = Object = @object;
        }

        /// <summary>
        /// Inserts a modifier in the given position.
        /// </summary>
        /// <param name="index">The new index of the modifier.</param>
        /// <param name="modifier">The modifier to insert.</param>
        /// <returns>A modified object with the new modifiers.</returns>
        public new ModifiedObject<T> WithInsertModifierAt(int index, NodeInfo<ModifierType> modifier)
        {
            Modifiers.Insert(index, modifier);
            return this;
        }

        /// <summary>
        /// Inserts a modifier in the first position.
        /// </summary>
        /// <param name="modifier">The modifier to insert.</param>
        /// <returns>A modified object with the new modifiers.</returns>
        public new ModifiedObject<T> WithInsertModifier(NodeInfo<ModifierType> modifier) =>
            WithInsertModifierAt(0, modifier);

        /// <summary>
        /// Inserts a modifier in the last position.
        /// </summary>
        /// <param name="modifier">The modifier to insert.</param>
        /// <returns>A modified object with the new modifiers.</returns>
        public new ModifiedObject<T> WithAppendModifier(NodeInfo<ModifierType> modifier)
        {
            Modifiers.Add(modifier);
            return this;
        }

        /// <summary>
        /// Inserts many modifiers at the given position.
        /// </summary>
        /// <param name="index">The position at which to insert the modifiers.</param>
        /// <param name="modifiers">The modifiers to insert.</param>
        /// <returns>A modified object with the new modifiers.</returns>
        public new ModifiedObject<T> WithInsertModifiersAt(int index, IEnumerable<NodeInfo<ModifierType>> modifiers)
        {
            Modifiers.InsertRange(index, modifiers);
            return this;
        }

        /// <summary>
        /// Inserts many modifiers at the beginning.
        /// </summary>
        /// <param name="modifiers">The modifiers to insert.</param>
        /// <returns>A modified object with the new modifiers.</returns>
        public new ModifiedObject<T> WithInsertModifiers(IEnumerable<NodeInfo<ModifierType>> modifiers)
            => WithInsertModifiersAt(0, modifiers);

        /// <summary>
        /// Inserts many modifiers at the end.
        /// </summary>
        /// <param name="modifiers">The modifiers to append.</param>
        /// <returns>A modified object with the new modifiers.</returns>
        public new ModifiedObject<T> WithAppendModifiers(IEnumerable<NodeInfo<ModifierType>> modifiers)
        {
            Modifiers.AddRange(modifiers);
            return this;
        }

        public override string ToString()
        {
            return Modifiers.Count == 0 ? Object.ToString() : $"{string.Join(' ', Modifiers)} {Object}";
        }
    }
}
