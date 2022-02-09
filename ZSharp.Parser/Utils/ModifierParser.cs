using System.Collections.Generic;

namespace ZSharp.Parser
{
    internal class ModifierParser
    {
        Parser<char, Identifier> Single { get; }

        Parser<char, IEnumerable<Identifier>> Many { get; }

        internal ModifierParser(TermParser t)
        {
            Single = t.Identifier;
            Many = Single.Many();

            // todo: add paameterized modifiers
        }
    }
}
