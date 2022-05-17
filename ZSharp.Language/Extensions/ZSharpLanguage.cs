using System;

namespace ZSharp.Language.Extensions
{
    public class ZSharpLanguage : ILanguageExtension
    {
        public void Initialize(Parser.Parser parser, ExtensionContext ctx)
        {
            ctx
                .AddSingleton(new TypeParser().Build(parser))
                .AddSingleton(new TypedItemParser().Build(parser, ctx))
                .AddSingleton(new FunctionBodyParser().Build(parser))
                .AddSingleton(new FunctionParser().Build(parser, ctx))
                .AddSingleton(new UsingStatementParser().Build(parser));
        }
    }
}
