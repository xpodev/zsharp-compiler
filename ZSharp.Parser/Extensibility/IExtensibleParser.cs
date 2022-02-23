﻿namespace ZSharp.Parser.Extensibility
{
    /// <summary>
    /// Interface for parsers that can be extended with more parsers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IExtensibleParser<T>
        where T : DocumentObject
    {
        /// <summary>
        /// Adds an extension parser to this parser.
        /// </summary>
        /// <param name="customParser">The new parser to add.</param>
        /// <param name="settings">Settings related to how to build the parser. Can be used to extend the custom parser.</param>
        void AddExtension<U>(ICustomParser<U> customParser, ParserBuilderSettings settings = default) where U : T;

        /// <summary>
        /// Get an extension registered in this parser by full name.
        /// </summary>
        /// <param name="fqn">The fully qualified name of the parser (sub-parser are separated by dot '.')</param>
        /// <returns>The extension with the given name or <c>null</c> if no extension exists.</returns>
        ICustomParser<U> GetExtension<U>(string fqn) where U : T;
    }
}