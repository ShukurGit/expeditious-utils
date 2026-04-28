
using System.Diagnostics;


namespace Expeditious.Common
{
    public class BenchmarkHelper
    {
        public static TimeSpan ExecutionTimeMeasure(Action action)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));

            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }
    }
}


