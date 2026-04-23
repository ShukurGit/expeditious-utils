using System;
using System.Collections.Generic;
using System.Text;

namespace Expedite.Utils.code.tester
{

    public static class ReplaceMultipleCharsTests
    {
        public static void RunAll()
        {
            MiniTestFramework.Run(Test_Replaces_Existing_Characters);
            MiniTestFramework.Run(Test_Leaves_Unmapped_Characters);
            MiniTestFramework.Run(Test_Replaces_All);
            MiniTestFramework.Run(Test_Empty_String);
            MiniTestFramework.Run(Test_Empty_Dictionary);
            MiniTestFramework.Run(Test_Repeated_Characters);
        }

        static void Test_Replaces_Existing_Characters()
        {
            var result = ReplaceMultipleChars("hello", new Dictionary<char, char>
        {
            { 'h', 'H' },
            { 'e', 'E' }
        });

            MiniTestFramework.AssertEqual("HEllo", result);
        }

        static void Test_Leaves_Unmapped_Characters()
        {
            var result = ReplaceMultipleChars("world", new Dictionary<char, char>
        {
            { 'x', 'X' }
        });

            MiniTestFramework.AssertEqual("world", result);
        }

        static void Test_Replaces_All()
        {
            var result = ReplaceMultipleChars("abc", new Dictionary<char, char>
        {
            { 'a', '1' },
            { 'b', '2' },
            { 'c', '3' }
        });

            MiniTestFramework.AssertEqual("123", result);
        }

        static void Test_Empty_String()
        {
            var result = ReplaceMultipleChars("", new Dictionary<char, char>
        {
            { 'a', 'b' }
        });

            MiniTestFramework.AssertEqual("", result);
        }

        static void Test_Empty_Dictionary()
        {
            var result = ReplaceMultipleChars("test", new Dictionary<char, char>());

            MiniTestFramework.AssertEqual("test", result);
        }

        static void Test_Repeated_Characters()
        {
            var result = ReplaceMultipleChars("aaa", new Dictionary<char, char>
        {
            { 'a', 'b' }
        });

            MiniTestFramework.AssertEqual("bbb", result);
        }

        // тестируемый метод
        static string ReplaceMultipleChars(string str, Dictionary<char, char> replacementChars)
        {
            var result = new char[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                result[i] = replacementChars.TryGetValue(str[i], out var r) ? r : str[i];
            }

            return new string(result);
        }
    }
}