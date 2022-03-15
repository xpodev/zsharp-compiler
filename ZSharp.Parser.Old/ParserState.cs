﻿namespace ZSharp.Parser
{
    public class ParserState
    {
        private static ParserState _state;

        private int _line = 1, _column = 0;
        private readonly OldCore.DocumentInfo _document;

        private ParserState(OldCore.DocumentInfo document)
        {
            _document = document;
        }

        public static void Reset(OldCore.DocumentInfo document)
        {
            _state = new(document);
        }

        public static OldCore.FileInfo GetInfo(string text)
        {
            int startLine = _state._line;
            int startColumn = _state._column;

            UpdateInfo(text);

            return new(_state._document, startLine, startColumn, _state._line, _state._column);
        }

        public static Pidgin.Unit UpdateInfo(string text)
        {
            int endLine = _state._line;
            int endColumn = _state._column;

            foreach (char c in text)
            {
                switch (c)
                {
                    case '\n':
                        endLine++;
                        endColumn = 1;
                        break;
                    case '\r':
                        endColumn = 1;
                        break;
                    default:
                        endColumn++;
                        break;
                }
            }

            _state._line = endLine;
            _state._column = endColumn;
            return Pidgin.Unit.Value;
        }

        public static Pidgin.Parser<char, OldCore.NodeInfo> CreateFileInfo(Pidgin.Parser<char, OldCore.Expression> parser) =>
            Pidgin.Parser.Rec(() => Utils.CreateObjectInfo(_state._document, parser));
    }
}
