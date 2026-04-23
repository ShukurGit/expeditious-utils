

namespace AppConsoleTester
{
    public static class ActionsScope
    {
        public static async Task Run()
        {
            await Actions_A.StartAction();
        }
    }
}
