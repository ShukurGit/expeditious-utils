
using Expeditious.Common;


namespace Expeditious.UsefulExtensions.Text
{
    public static class ExtTextFormatter
    {
        static public String xSurrounWithParentheses(this string str, bool shiftAsideBySpaces = false)
        {
            return TextFormatter.SurroundWithChars(str, '(', ')', shiftAsideBySpaces);
        }


        static public String xSurrounWithSquareBrackets(string str, bool shiftAsideBySpaces = false)
        {
            return TextFormatter.SurroundWithChars(str, '[', ']', shiftAsideBySpaces);
        }


        static public String xSurrounWithCurlyBrackets(string str, bool shiftAsideBySpaces = false)
        {
            return TextFormatter.SurroundWithChars(str, '{', '}', shiftAsideBySpaces);
        }


        static public String xSurrounWithQuotationMarks(string str, bool shiftAsideBySpaces = false, bool useSafeSymbol = true)
        {
            char symbol = useSafeSymbol ? '"' : '″';
            return TextFormatter.SurroundWithChars(str, symbol, symbol, shiftAsideBySpaces);
        }







        static public String xRemoveSurroundedParentheses(String str, Boolean trimBefore = true)
        {
            return TextFormatter.RemoveSurroundedChars(str, '(', ')', trimBefore);
        }


        static public String xRemoveSurroundedSquareBrackets(String str, Boolean trimBefore = true)
        {
            return TextFormatter.RemoveSurroundedChars(str, '[', ']', trimBefore);
        }


        static public String xRemoveSurroundedCurlyBrackets(String str, Boolean trimBefore = true)
        {
            return TextFormatter.RemoveSurroundedChars(str, '{', '}', trimBefore);
        }


        static public String xRemoveSurroundedQuotationMarks(String str, Boolean trimBefore = true)
        {
            char safeSymbol = '″';
            char symbol = '"';
            string result = TextFormatter.RemoveSurroundedChars(str, symbol, symbol, trimBefore);
            return TextFormatter.RemoveSurroundedChars(result, safeSymbol, safeSymbol, trimBefore);
        }
    }
}


