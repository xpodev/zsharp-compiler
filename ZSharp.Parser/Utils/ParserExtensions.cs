namespace ZSharp.Parser
{
    public static class ParserExtensions
    {
        internal static Symbols Symbols { get; set; }

        internal static DocumentParser Parser { get; set; }

        public static Parser<char, T> BeforeWhitespace<T>(this Parser<char, T> parser) =>
            Parser.WithAnyWhitespace(parser);

        public static Parser<char, ObjectInfo> BuildWith(this Parser<char, ObjectInfo> parser, ParserBuilderSettings settings)
        {
            Parser<char, Unit> keyword = Parser.Identifier.Identifier.Assert(s => s.Name == settings.Keyword).BeforeWhitespace().IgnoreResult();
            Parser<char, ObjectInfo> many = settings.IsModifiable ? parser.WithPrefixModifiers().ManyCollection().WithObjectInfo().UpCast() : parser;
            if (settings.Keyword is null) _ = parser;
            else if (settings.AllowDefault && settings.AllowBlockDefinition)
                parser = keyword.Then(OneOf(
                    Symbols.Colon.Then(many),
                    many.Parenthesized(settings.BlockBracketType),
                    parser
                    ));
            else if (settings.AllowDefault)
                parser = keyword.Then(Symbols.Colon.Then(many).Or(parser));
            else if (settings.AllowBlockDefinition)
                parser = keyword.Then(many.Parenthesized(settings.BlockBracketType).Or(parser));
            else 
                parser = keyword.Then(parser);
            return settings.IsModifiable ? parser.WithPrefixModifiers().UpCast() : parser;
        }

        public static Parser<char, ObjectInfo> BuildWith<T>(this Parser<char, ObjectInfo<T>> parser, ParserBuilderSettings settings)
            where T : DocumentObject =>
            parser.UpCast().BuildWith(settings);

        public static Parser<char, ObjectInfo> BuildWith<T>(this Parser<char, T> parser, ParserBuilderSettings settings)
            where T : DocumentObject =>
            parser.WithObjectInfo().UpCast().BuildWith(settings);

        public static Parser<char, Collection> ManyCollection(this Parser<char, ObjectInfo> parser) =>
            parser.Many().Select(items => new Collection(items));

        public static Parser<char, Collection<T>> ManyCollection<T>(this Parser<char, T> parser)
            where T : DocumentObject =>
            parser.WithObjectInfo().ManyCollection<T>();

        public static Parser<char, Collection<T>> ManyCollection<T>(this Parser<char, ObjectInfo<T>> parser)
            where T : DocumentObject =>
            parser.Many().Select(items => new Collection<T>(items));

        public static Parser<char, Collection<T>> ManyInside<T>(this Parser<char, T> parser, BracketType brackets = BracketType.Curly)
            where T : DocumentObject =>
            Parser.CreateParser(parser).Many().Select(items => new Collection<T>(items)).Parenthesized(brackets);

        public static Parser<char, ObjectInfo<Collection>> ManyInside(this Parser<char, ObjectInfo> parser, BracketType brackets = BracketType.Curly) =>
            parser.Many().Select(items => new Collection(items)).Parenthesized(brackets).WithObjectInfo();

        public static Parser<char, ObjectInfo<Collection<T>>> ManyInside<T>(this Parser<char, ObjectInfo<T>> parser, BracketType brackets = BracketType.Curly)
            where T : DocumentObject =>
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

        public static Parser<char, ObjectInfo> UpCast<T>(this Parser<char, ObjectInfo<T>> parser) where T : DocumentObject =>
            parser.Cast<ObjectInfo>();

        public static Parser<char, DocumentObject> UpCast<T>(this Parser<char, T> parser) where T : DocumentObject =>
            parser.Cast<DocumentObject>();

        public static Parser<char, ObjectInfo<T>> WithObjectInfo<T>(this Parser<char, T> parser) where T : DocumentObject =>
            Parser.CreateParser(parser);

        public static Parser<char, ObjectInfo<ModifiedObject>> WithPrefixModifiers(this Parser<char, ObjectInfo> parser)
        {
            Parser<char, ObjectInfo<ModifiedObject>> modified = Parser.CreateParser(
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

        public static Parser<char, ObjectInfo<ModifiedObject<T>>> WithPrefixModifiers<T>(this Parser<char, ObjectInfo<T>> parser)
            where T : DocumentObject
            {
            Parser<char, ObjectInfo<ModifiedObject<T>>> modified = Parser.CreateParser(
                from @object in Try(parser)
                select new ModifiedObject<T>()
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

        public static Parser<char, ModifiedObject<T>> WithPrefixModifiers<T>(this Parser<char, T> parser)
            where T : DocumentObject
        {
            Parser<char, ModifiedObject<T>> modified = 
                from @object in Parser.CreateParser(Try(parser))
                select new ModifiedObject<T>()
                {
                    Object = @object
                };
            modified = modified.Or(Try(
                from modifier in Parser.Identifier.Parser
                from @object in Rec(() => modified)
                select @object.WithInsertModifier(modifier)
                ));
            return modified;
        }
    }
}
