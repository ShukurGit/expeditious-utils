


namespace Expedite.Utils
{

    public static class MiniTestFramework
    {
        private static int passed = 0;
        private static int failed = 0;

        public static void Run(Action test)
        {
            try
            {
                test();
                passed++;
                WriteColored($"✔ {test.Method.Name}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                failed++;
                WriteColored($"✘ {test.Method.Name}", ConsoleColor.Red);
                Console.WriteLine($"   {ex.Message}");
            }
        }


        public static void Summary()
        {
            Console.WriteLine();
            Console.WriteLine($"Total: {passed + failed}, Passed: {passed}, Failed: {failed}");

            if (failed > 0)
                Environment.Exit(1);
        }


        public static void AssertEqual(string expected, string actual)
        {
            if (expected != actual)
                throw new Exception($"Expected '{expected}', got '{actual}'");
        }


        private static void WriteColored(string text, ConsoleColor color)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = old;
        }
    }
}