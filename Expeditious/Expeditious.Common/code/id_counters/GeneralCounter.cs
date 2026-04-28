

namespace Expeditious.Common
{
    public static class GeneralCounter
    {
        private static int _counter;

        public static void SetStartNubber(int startFromThis) => Interlocked.Exchange(ref _counter, startFromThis - 1);

        public static int Next() => Interlocked.Increment(ref _counter);

        public static int Current => Volatile.Read(ref _counter);

        public static void Reset() => Interlocked.Exchange(ref _counter, 0);
    }
}


