
using Expeditious.Common;


namespace Expeditious.UsefulExtensions.Text
{
    public static class ExtTextNormalizer
    {
        static public String xReplaceProblematicPunctuationChars(this string text)
        {
            return TextNormalizer.ProblematicPunctuationChars.Replace(text);
        }


        static public String xRestoreProblematicPunctuationChars(this string text)
        {
            return TextNormalizer.ProblematicPunctuationChars.Restore(text);
        } 
    }
}
