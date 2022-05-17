namespace ZSharp.Parser
{
    public static class ParserExtensions
    {
        internal static Symbols Symbols { get; set; }

        internal static DocumentParser Parser { get; set; }

        public static Parser<char, T> BeforeWhitespace<T>(this Parser<char, T> parser) =>
            Parser.WithAnyWhitespace(parser);

        public static Parser<char, NodeInfo> BuildWith(this Parser<char, NodeInfo> parser, ParserBuilderSettings settings)
        {
            Parser<char, NodeInfo> many = settings.IsModifiable ? parser.WithPrefixModifiers().ManyCollection().WithObjectInfo().UpCast() : parser;
            if (settings.AllowBlockDefinition)
                parser = many.Parenthesized(settings.BlockBracketType).Or(parser);
            if (settings.Keyword is not null) parser = parser.WithPrefixKeyword(settings.Keyword).BeforeWhitespace();
            return settings.IsModifiable ? parser.WithPrefixModifiers().UpCast() : parser;
        }

        public static Parser<char, NodeInfo> BuildWith<T>(this Parser<char, NodeInfo<T>> parser, ParserBuilderSettings settings)
            where T : Node =>
            parser.UpCast().BuildWith(settings);

        public static Parser<char, T> BuildWith<T>(this Parser<char, T> parser, ParserBuilderSettings settings)
            where T : Node =>
            parser.WithObjectInfo().BuildWith(settings).Select(o => o.Cast<T>());

        public static Parser<char, Collection> ManyCollection(this Parser<char, NodeInfo> parser) =>
            parser.Many().Select(items => new Collection(items));

        public static Parser<char, Collection<T>> ManyCollection<T>(this Parser<char, T> parser)
            where T : Node =>
            parser.WithObjectInfo().ManyCollection();

        public static Parser<char, Collection<T>> ManyCollection<T>(this Parser<char, NodeInfo<T>> parser)
            where T : Node =>
            parser.Many().Select(items => new Collection<T>(items));

        public static Parser<char, Collection<T>> ManyInside<T>(this Parser<char, T> parser, BracketType brackets = BracketType.Curly)
            where T : Node =>
            Parser.CreateParser(parser).Many().Select(items => new Collection<T>(items)).Parenthesized(brackets);

        public static Parser<char, NodeInfo<Collection>> ManyInside(this Parser<char, NodeInfo> parser, BracketType brackets = BracketType.Curly) =>
            parser.Many().Select(items => new Collection(items)).Parenthesized(brackets).WithObjectInfo();

        public static Parser<char, NodeInfo<Collection<T>>> ManyInside<T>(this Parser<char, NodeInfo<T>> parser, BracketType brackets = BracketType.Curly)
            where T : Node =>
            parser.Many().Select(Collection<T>.Create).Parenthesized(brackets);

        public static Parser<char, T> Parenthesized<T>(this Parser<char, T> parser, BracketType brackets = BracketType.Curvy) =>
            brackets switch
            {
                BracketType.Angle => parser.Between(Symbols.LAngleBracket, Symbols.RAngleBracket),
                BracketType.Curly => parser.Between(Symbols.LCurlyBracket, Symbols.RCurlyBracket),
                BracketType.Curvy => parser.Between(Symbols.LCurvyBracket, Symbols.RCurvyBracket),
                BracketType.Square => parser.Between(Symbols.LSquareBracket, Symbols.RSquareBracket),
                _ => throw new System.ArgumentOutOfRangeException(nameof(brackets), brackets, $"Must be a valid {nameof(BracketType)} member.")
            };

        //public static Parser<char, Node> SingleOrManyCollection<T>(this Parser<char, T> parser) 
        //    where T : Node =>
        //    parser.

        public static Parser<char, NodeInfo> UpCast<T>(this Parser<char, NodeInfo<T>> parser) where T : Node =>
            parser.Cast<NodeInfo>();

        public static Parser<char, Node> UpCast<T>(this Parser<char, T> parser) where T : Node =>
            parser.Cast<Node>();

        public static Parser<char, NodeInfo<T>> WithObjectInfo<T>(this Parser<char, T> parser) where T : Node =>
            Parser.CreateParser(parser);

        public static Parser<char, T> WithPrefixKeyword<T>(this Parser<char, T> parser, string keyword)
        {
            if (keyword is null)
                throw new System.ArgumentNullException(nameof(keyword));
            Result<char, Identifier> parseResult = Parser.Identifier.Identifier.Parse(keyword);
            if (!parseResult.Success) 
                throw new System.ArgumentException("Must be a valid Z# identifier", nameof(keyword));
            return Parser.Identifier.Identifier.Assert(id => id.Name == keyword).BeforeWhitespace().Then(parser);
        }

        public static Parser<char, NodeInfo<ModifiedObject>> WithPrefixModifiers(this Parser<char, NodeInfo> parser)
        {
            Parser<char, NodeInfo<ModifiedObject>> modified = Parser.CreateParser(
                from @object in Try(parser)
                select new ModifiedObject()
                {
                    Object = @object
                });
            modified = modified.Or(Try(
                from modifier in Parser.Identifier.Parser
                from @object in Rec(() => modified)
                select @object.Select(mod => mod.WithInsertModifier(modifier))
                    ));
            return modified;
        }

        public static Parser<char, NodeInfo<ModifiedObject<T>>> WithPrefixModifiers<T>(this Parser<char, NodeInfo<T>> parser)
            where T : Node
            {
            Parser<char, NodeInfo<ModifiedObject<T>>> modified = Parser.CreateParser(
                from @object in Try(parser)
                select new ModifiedObject<T>(@object)
                );
            modified = modified.Or(Try(
                from modifier in Parser.Identifier.Parser
                from @object in Rec(() => modified)
                    select @object.Select(mod => mod.WithInsertModifier(modifier))
                    ));
                return modified;
            }

        public static Parser<char, ModifiedObject<T>> WithPrefixModifiers<T>(this Parser<char, T> parser, string keyword = null)
            where T : Node
        {
            Parser<char, ModifiedObject<T>> modified;
            if (keyword is not null)
            {
                modified = (
                    from modifiers in Parser.Identifier.Parser.Until(String(keyword).BeforeWhitespace())
                    from @object in parser.WithObjectInfo()
                    select new ModifiedObject<T>(@object).WithInsertModifiers(modifiers)
                    );
            } else
            {
                modified =
                    from @object in Parser.CreateParser(Try(parser))
                    select new ModifiedObject<T>(@object);
                modified = Try(
                    from modifier in Parser.Identifier.Parser
                    from @object in Rec(() => modified)
                    select @object.WithInsertModifier(modifier)
                    ).Or(modified);
            }
            return modified;
        }
    }
}
