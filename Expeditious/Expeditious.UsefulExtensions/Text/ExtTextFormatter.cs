
using Expeditious.Common;


namespace Expeditious.UsefulExtensions.Text
{
    public static class ExtTextFormatter
    {
        static public String xSurrounWithParentheses(this string str, bool shiftAsideBySpaces = false)
        {
            return TextFormatter.SurroundWithChars(str, '(', ')', shiftAsideBySpaces);
        }


        static public String xSurrounWithSquareBrackets(this string str, bool shiftAsideBySpaces = false)
        {
            return TextFormatter.SurroundWithChars(str, '[', ']', shiftAsideBySpaces);
        }


        static public String xSurrounWithCurlyBrackets(this string str, bool shiftAsideBySpaces = false)
        {
            return TextFormatter.SurroundWithChars(str, '{', '}', shiftAsideBySpaces);
        }


        static public String xSurrounWithQuotationMarks(this string str, bool shiftAsideBySpaces = false, bool useSafeSymbol = true)
        {
            char symbol = useSafeSymbol ? '"' : '″';
            return TextFormatter.SurroundWithChars(str, symbol, symbol, shiftAsideBySpaces);
        }







        static public String xRemoveSurroundedParentheses( this String str, Boolean trimBefore = true)
        {
            return TextFormatter.RemoveSurroundedChars(str, '(', ')', trimBefore);
        }


        static public String xRemoveSurroundedSquareBrackets(this String str, Boolean trimBefore = true)
        {
            return TextFormatter.RemoveSurroundedChars(str, '[', ']', trimBefore);
        }


        static public String xRemoveSurroundedCurlyBrackets(this String str, Boolean trimBefore = true)
        {
            return TextFormatter.RemoveSurroundedChars(str, '{', '}', trimBefore);
        }


        static public String xRemoveSurroundedQuotationMarks(this String str, Boolean trimBefore = true)
        {
            char safeSymbol = '″';
            char symbol = '"';
            string result = TextFormatter.RemoveSurroundedChars(str, symbol, symbol, trimBefore);
            return TextFormatter.RemoveSurroundedChars(result, safeSymbol, safeSymbol, trimBefore);
        }
    }
}


