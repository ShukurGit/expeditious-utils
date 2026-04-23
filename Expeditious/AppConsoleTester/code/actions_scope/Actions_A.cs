

using Expeditious.UsefulExtensions.Text;


namespace AppConsoleTester
{
    public static class Actions_A
    {
        public static async Task StartAction()
        {
            await Action_A1();
        }

        private static async Task Action_A1()
        {
            Console.WriteLine("-");

            string dd = ";sdfkj;lsdk;lfskdl;'\",'\",'\",kf";

            var dsds = dd.xReplaceProblematicPunctuationChars();
            int re = 90;
        }
    }
}
