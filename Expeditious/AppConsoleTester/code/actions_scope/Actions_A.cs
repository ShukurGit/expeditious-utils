

using Expeditious.Candidates;
using Expeditious.Candidates.code.logging_new;
using Expeditious.UsefulExtensions.Text;
using System.Xml.Linq;
using Yeni.YeniCore;


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

            //YeniVariantsCore_bax.RunAA();

            int[] sdd = [1, 2, 3, 4,];
            var chars = new[] { 'A', 'B', 'C',  };

            var de = PermutationHelper.PermutateIntArray(sdd);

            var dewrdfsd = PermutationHelper.PermutateStringsSymbols(chars);
            //var dsad = ArrayPermutation_.ArrayPermutation(sdd, 0, 3);

            //Logger.Test_Run();

            List<string> tr = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                tr.Add(TraceContext.TraceId);
                Console.WriteLine(TraceContext.TraceId);
            }


            var dasdasdasasdasdas = sdd.GetPermutations<int>();


            var items = new[] { "A", "B", "C" };

            foreach (var p in items.GetPermutations())
            {
                Console.WriteLine(string.Join(", ", p));
            }

            Console.WriteLine("*************************************");


            Console.WriteLine("-");

            string dd = ";sdfkj;lsdk;lfskdl;'\",'\",'\",kf";

            var dsds = dd.xReplaceProblematicPunctuationChars();
            int re = 90;
        }
    }
}
