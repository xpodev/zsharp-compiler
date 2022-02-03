using ZSharp.Core;
using ZSharp.Engine;

[assembly: CustomOperator(".", 10, OperatorFixity.Infix, Associativity.Left)]
[assembly: CustomOperator("->", 40, OperatorFixity.Infix, Associativity.Right)]
[assembly: CustomOperator(",", 45, OperatorFixity.Infix, Associativity.Left)]
[assembly: CustomOperator(":", 50, OperatorFixity.Infix)]

[assembly: CustomSurroundingOperator("(", ")", 15, false)]
[assembly: CustomSurroundingOperator("{", "}", 100, false, true)]

[assembly: CustomKeywordLiteral("()")]

[assembly: CustomKeywordLiteral("infoof")]
[assembly: CustomKeywordLiteral("IL")]

[assembly: CustomKeywordLiteral("void")]
[assembly: CustomKeywordLiteral("string")]
[assembly: CustomKeywordLiteral("i32")]
[assembly: CustomKeywordLiteral("i64")]

[assembly: CustomKeyword("namespace", 10, OperatorFixity.Prefix)]
[assembly: CustomKeyword("class", 10, OperatorFixity.Prefix)]
[assembly: CustomKeyword("func", 10, OperatorFixity.Prefix)]
[assembly: CustomKeyword("static", 57, OperatorFixity.Prefix)]
[assembly: CustomKeyword("using", 30, OperatorFixity.Prefix)]

[assembly: CustomKeyword("__entrypoint", 56, OperatorFixity.Prefix)]

