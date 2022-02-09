namespace ZSharp.Parser
{
    internal class CallParser
    {
        const char AngleL = '<', AngleR = '>';
        const char CurlyL = '{', CurlyR = '}';
        const char CurvyL = '(', CurvyR = ')';
        const char PipeL = '|', PipeR = '|';
        const char SquareL = '[', SquareR = ']';

        internal CallParser(DocumentParser p)
        {
            Parser<char, Unit>
                angleL, angleR,
                curlyL, curlyR,
                curvyL, curvyR,
                pipeL, pipeR,
                squareL, squareR;

            angleL = p.WithAnyWhitespace(Char(AngleL)).IgnoreResult();
            angleR = p.WithAnyWhitespace(Char(AngleR)).IgnoreResult();
            curlyL = p.WithAnyWhitespace(Char(CurlyL)).IgnoreResult();
            curlyR = p.WithAnyWhitespace(Char(CurlyR)).IgnoreResult();
            curvyL = p.WithAnyWhitespace(Char(CurvyL)).IgnoreResult();
            curvyR = p.WithAnyWhitespace(Char(CurvyR)).IgnoreResult();
            pipeL = p.WithAnyWhitespace(Char(PipeL)).IgnoreResult();
            pipeR = p.WithAnyWhitespace(Char(PipeR)).IgnoreResult();
            squareL = p.WithAnyWhitespace(Char(SquareL)).IgnoreResult();
            squareR = p.WithAnyWhitespace(Char(SquareR)).IgnoreResult();
        }
    }
}
