

namespace AppConsoleTester.ActionsScope
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
        }
    }
}
