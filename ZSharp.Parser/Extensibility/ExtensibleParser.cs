using System;
using System.Collections.Generic;

namespace ZSharp.Parser.Extensibility
{
    public abstract class ExtensibleParser<T, U>
        : CustomParser<T>
        , IExtensibleParser<U>
        where T : Node
        where U : Node
    {
        private protected readonly Dictionary<string, ExtensionParser> _extensions = new();

        public ExtensibleParser(string name, string @namespace) : base(name, @namespace) { }

        public void AddExtension<V>(ICustomParser<V> extensionParser, string keyword)
            where V : U
        {
            if (keyword is null)
                throw new ArgumentNullException(nameof(keyword));
            ExtensionParser extension = new(extensionParser);
            if (!_extensions.TryAdd(extension.Name, extension))
                throw new InvalidOperationException($"Parser \'{Name}\' already contains an extension parser with name \'{extension.Name}\'");
            extension.Parser = extension.Parser.WithPrefixKeyword(keyword);
            //if (extension.Keyword is not null)
            //    extension.Parser = 
            //        parser.Document.Identifier.Parser.Assert(s => s.Object.Name == extension.Keyword)
            //        .Then(OneOf(
            //            extension.Parser.WithPrefixModifiers().Many().Select(items => Collection.Create(items)).Parenthesized(BracketType.Curly),
            //            extension.Parser
            //            ));
            //extension.Parser = extension.Parser.WithPrefixModifiers().Cast<NodeInfo>();
        }

        public ICustomParser<V> GetExtension<V>(string fqn)
            where V : U
        {
            string[] parts = fqn.Split('.');

            if (parts.Length == 1) return (ICustomParser<V>)_extensions[parts[0]];
            IExtensibleParser<U> parser = this;

            foreach (string part in parts[..-1])
            {
                if (parser.GetExtension<V>(part) is not IExtensibleParser<U> extension)
                    return null;
                if (extension is not IExtensibleParser<U> extensible)
                    return null;
                parser = extensible;
            }
            return parser.GetExtension<V>(parts[^1]);
        }
    }
}
