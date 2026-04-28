

using Expeditious.Candidates;
using Expeditious.Candidates.code.text_similarity;
using Expeditious.Common;
using Expeditious.Common.code.collections.Combinatorics;
using Expeditious.Common.Combinatorics;
using Expeditious.UsefulExtensions.Text;


namespace AppConsoleTester
{
    public static class Actions_A
    {
        public static async Task StartAction()
        {
            await Action_A3();
        }


        private static async Task Action_A3()
        {
            string text = "Salam, Bakı! Привет, мир. Hello world! Bakı-2026.";

            var words = TextSearchHelper.SplitToWords(text);

            bool hasBaku = TextSearchHelper.ContainsWord(text, "Bakı");
            bool hasBaku2 = TextSearchHelper.ContainsWord(text, "BakI");

            bool hasAny = TextSearchHelper.ContainsAnyWord(text, new[]
            {
                "Paris",
                "Bakı",
                "London"
            });

            bool hasAll = TextSearchHelper.ContainsAllWords(text, new[]
            {
                "Salam",
                "Bakı"
            });

            int count = TextSearchHelper.CountWord(text, "Bakı");

            string replaced = TextSearchHelper.ReplaceWholeWord(
                text,
                "Bakı",
                "Gəncə");


            int rere = 90;
        }


        private static async Task Action_A2()
        {

            var product = CartesianProductHelper.GetCartesianProduct(new[]
                {
                    new[] { "A", "B" },
                    new[] { "1", "2" },
                    new[] { "X", "Y" }
                });

            var strings = StringCartesianProductHelper.GetConcatenatedProduct(new[]
                {
                    new[] { "A", "B" },
                    new[] { "1", "2" }
                });

            var subsets = PowerSetHelper.GetNonEmptyPowerSet(new[]
                {
                    "A", "B", "C"
                });

            var permutations = PermutationHelper.GetPermutations(new[]
                {
                    "A", "B", "C"
                });


            string dddd = CollectionTextFormatter.ToMultilineText(subsets);



            var ee = StringSimilarityHelper.WordsSimilarityLevenshtein("rock", "block");
            var ewewe = TextSimilarity.WordsSimilarityLevenshtein("rock", "block");
            var ewewee = TextSimilarity.WordsSimilarityLevenshtein("adil", "Agil");
            var dewewee = TextSimilarity.WordsSimilarityLevenshtein("stomatoloq", "st0mat010q");
            var ede = StringSimilarityHelper.WordsSimilarityLevenshtein("stomatoloq", "st0mat010q");

            var ti1 = TraceContext.TraceId;
            Console.WriteLine(ti1);
            var ti2 = TraceContext.TraceId;
            Console.WriteLine(ti2);

            ElementaryLogger.Configure(new CommonLoggerOptions()
            {
                IncludeStackTraceByDefault = false,
                LogRootDirectory = @"c:\Logs\",
                MaxStackTraceLines = 15,
                MinLevel = CommonLogLevel.Info,
                SpecificProjectName = "MyProject",
                UseJson = true,
            });
            ElementaryLogger.Info("info");
            ElementaryLogger.Info("info2");
            ElementaryLogger.Warning("Warn");



            ComplexLogger.Configure(new CommonLoggerOptions()
            {
                IncludeStackTraceByDefault = false,
                LogRootDirectory = @"c:\Logs\",
                MaxStackTraceLines = 15,
                MinLevel = CommonLogLevel.Info,
                SpecificProjectName = "MyProject2",
                UseJson = true,
            });
            ComplexLogger.Info("info");
            ComplexLogger.Info("info2");
            ComplexLogger.Warning("Warn");
            ComplexLogger.Shutdown();    // !!! dont forget


            string stop = "";
        }

        private static async Task Action_A1()
        {

            var ti1 = TraceContext.TraceId;
            Console.WriteLine(ti1);
            var ti2 = TraceContext.TraceId;
            Console.WriteLine(ti2);


            //YeniVariantsCore_bax.RunAA();

            int[] sdd = [1, 2, 3, 4,];
            var chars = new[] { 'A', 'B', 'C',  };

            var de = PermutationOtherHelper.PermutateIntArray(sdd);

            var dewrdfsd = PermutationOtherHelper.PermutateStringsSymbols(chars);
            //var dsad = ArrayPermutation_.ArrayPermutation(sdd, 0, 3);

            //Logger.Test_Run();

            List<string> tr = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                tr.Add(TraceContext.TraceId);
                Console.WriteLine(TraceContext.TraceId);
            }


            EasyLogger.Info("info 2");

            //Thread.Sleep(200);
            //Thread.Sleep(200);
            //Thread.Sleep(2000);
            EasyLogger.Warning("warn 2");
            //EasyLogger.Shutdown();

            var dasdasdasasdasdas = sdd.GetPermutations<int>();

            return;

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

