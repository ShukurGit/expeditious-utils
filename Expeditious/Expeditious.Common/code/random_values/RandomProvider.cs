

// for viewing only

namespace Expedite.Utils.Randomizing
{
    using System;
    using System.Threading;

    //  code by John Skeet (ThreadedRandom Provider) - provides every times new Random class
    public class RandomProvider
    {
        static private int seed = Environment.TickCount;
        static private ThreadLocal<Random> randomWrapper = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));



        static public Random GetThreadedRandom()
        {
            return randomWrapper.Value;
        }


        static public Random GetOrdinaryRandom()
        {
            return new Random(Environment.TickCount);
        }


        static public Random GetRandom(bool useThreadedRandom = false)
        {
            if (useThreadedRandom)
                return GetThreadedRandom();
            else
                return GetOrdinaryRandom();
        }

    }
}
