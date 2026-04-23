

namespace Expeditious.Common
{
    public static class SharedConstTextSymbols
    {
        public enum SymbolType { Undefined, Digit, LetterBig, LetterSmall, PunctuationSign, MathSign };


        // quotes
        static public readonly Char CHAR_MONEY_AZN = '₼';
        static public readonly Char QUOTES_DOUBLE = '"';        // 34
        static public readonly Char QUOTES_SINGLE = '\'';       // 39
        static public readonly Char QUOTES_DOUBLE_SAFE = 'ʺ';   // 698
        static public readonly Char QUOTES_SINGLE_SAFE = '΄';   // 900
        static public readonly Char QUOTES_DOUBLE_SAFE_2 = 'ˮ';   // 750
        static public readonly Char QUOTES_SINGLE_SAFE_2 = '`';   //  96
        static public readonly Char QUOTES_SINGLE_SAFE_3 = 'ˈ';   // 712
        static public readonly Char QUOTES_SINGLE_SAFE_RUS_START = '«';   // 
        static public readonly Char QUOTES_SINGLE_SAFE_RUS_END = '»';   // 
        static private readonly String QUOTES_RUSSIAN = "«»";

        // apostrophe
        static public readonly Char CHAR_APOSTROPHE_INI_39 = '\'';
        static public readonly Char CHAR_APOSTROPHE_INI_ESC_96 = '`';
        static public readonly Char CHAR_APOSTROPHE_SUBTITUTION_712 = 'ˈ';
        static public readonly Char CHAR_APOSTROPHE_SUBTITUTION_900 = '΄';
        static public readonly Char CHAR_APOSTROPHE_SUBTITUTION_BEST_PAIR_OPEN_8216 = '‘';
        static public readonly Char CHAR_APOSTROPHE_SUBTITUTION_BEST_PAIR_CLOSE_8217 = '’';
        static public readonly Char CHAR_APOSTROPHE_SUBTITUTION_8242 = '′';
        static public readonly String STR_APOSTROPHE_WEB = "&apos;";

        // comma zapyataya
        static public readonly Char CHAR_COMMA_INI_44 = ',';
        static public readonly Char CHAR_COMMA_SUBTITUTION_8218 = '‚';

        // math
        static public readonly Char CHAR_MATH_PLUS_43 = '+';                    //  43
        static public readonly Char CHAR_MATH_MINUS_45 = '-';                   //  45
        static public readonly Char CHAR_MATH_MULTIPLY_183 = '·';               // 183
        static public readonly Char CHAR_MATH_DIVIDE_247 = '÷';                 // 247
        static public readonly Char CHAR_MATH_SUM = '∑';  // 8721
        static public readonly Char CHAR_MATH_INFINITY = '∞';  //
        static public readonly Char CHAR_MATH_NOT_EQUAL = '≠';  // 
        static public readonly Char CHAR_MATH_LESS_EQUAL = '≤';  //
        static public readonly Char CHAR_MATH_MORE_EQUAL = '≥';  //
        static public readonly Char CHAR_MATH_APPROXIMATELY_EQUAL = '≈';  //

        // points
        static public readonly Char CHAR_POINT_PASSWORD = '●';  // 9679
        static public readonly Char CHAR_POINT_MULTTIPLY = '•';  // 8226

        // arrows
        static public readonly Char CHAR_ARROW_RIGHT = '→';  // 
        static public readonly Char CHAR_ARROW_LEFT = '←';  // 
        static public readonly Char CHAR_ARROW_UP = '↑';  // 
        static public readonly Char CHAR_ARROW_DOWN = '↓';  // 
        static public readonly Char CHAR_ARROW_UP_LEFT = '↖';  // 
        static public readonly Char CHAR_ARROW_UP_RIGHT = '↗';  // 
        static public readonly Char CHAR_ARROW_DOWN_LEFT = '↙';  // 
        static public readonly Char CHAR_ARROW_DOWN_RIGHT = '↘';  // 

        static public readonly char LOOPED_SQUARE = '⌘';
        static public readonly char DEQREE_SIGN = '°';

        static public readonly Char CHAR_NON_BREAKING_SPACE = (char)160;  //  160 &nbsp;  non-breaking space
        static public readonly Char CHAR_CARRIAGE_RETURN_13 = '\r';  //  CHAR(13) (Carriage Return - \r)
        static public readonly Char CHAR_LINE_FEED_10 = '\n';  //  CHAR(10) (Line Feed - \n)

        static private readonly string TOPLUM = "≤≥≠≈∞√∆●←↑→↓↔↕↖↗↘↙™℠℗№";


        static public class PunctuationMarks
        {
            static public readonly char PERIOD = '.';
            static public readonly char QUESTION_MARK = '?';
            static public readonly char EXCLAMATION_POINT = '!';
            static public readonly char COMMA = ',';
            static public readonly char COMMA_SAFE = '‚';
            static public readonly char COLON = ':';
            static public readonly char SEMICOLON = ';';

            static public readonly char HYPHEN = '-';
            static public readonly char EN_DASH = '–';
            static public readonly char EM_DASH = '—';

            static public readonly char PARENTHESES_OPEN = '(';
            static public readonly char PARENTHESES_CLOSE = ')';

            static public readonly char SQUARE_BRACKETS_OPEN = '[';
            static public readonly char SQUARE_BRACKETS_CLOSE = ']';

            static public readonly char CURLY_BRACKETS_OPEN = '{';
            static public readonly char CURLY_BRACKETS_CLOSE = '}';

            static public readonly char ANGLE_BRACKETS_OPEN = '<';
            static public readonly char ANGLE_BRACKETS_CLOSE = '>';

            static public readonly char MATH_ANGLE_BRACKETS_OPEN = '⟨';
            static public readonly char MATH_ANGLE_BRACKETS_CLOSE = '⟩';

            static public readonly char QUOTATION_MARK = '"';
            static public readonly char QUOTATION_MARK_SAFE = '″';
            static public readonly char QUOTATION_MARK_RUS_OPEN = '«';
            static public readonly char QUOTATION_MARK_RUS_CLOSE = '»';
            static public readonly char QUOTATION_MARK_OTHER_OPEN = '“';
            static public readonly char QUOTATION_MARK_OTHER_CLOSE = '„';
            static public readonly char QUOTATION_MARK_OTHER_BOLD_OPEN = '❝';
            static public readonly char QUOTATION_MARK_OTHER_BOLD_CLOSE = '❞';
            static public readonly char QUOTATION_MARK_TRIPLE_OPEN = '‷';
            static public readonly char QUOTATION_MARK_TRIPLE_CLOSE = '‴';
            static public readonly char QUOTATION_MARK_QUADRUPLE = '⁗';
            static public readonly char QUOTATION_MARK_SINGLE_BOLD_OPEN = '❛';
            static public readonly char QUOTATION_MARK_SINGLE_BOLD_CLOSE = '❜';

            static public readonly char APOSTROPHE = '\'';
            static public readonly char APOSTROPHE_SAFE = '’';

            static public readonly char SLASH = '/';
            static public readonly char ELLIPSES = '…';
            static public readonly char ASTERISK = '*';
            static public readonly char ASTERISM = '⁂';

            static public readonly char AMPERSAND = '&';
            static public readonly char BULLET_POINT = '•';
            static public readonly char MIDDLE_DOT = '·';
            static public readonly char BOLD_DOT = '●';  // 9679
            static public readonly char POUND_SYMBOL = '#';
            static public readonly char TILDE = '~';
            static public readonly char BACKSLASH = '\\';
            static public readonly char AT_SYMBOL = '@';
            static public readonly char CARET_SYMBOL = '^';
            static public readonly char PIPE_SYMBOL = '|';


        }
    }
}
