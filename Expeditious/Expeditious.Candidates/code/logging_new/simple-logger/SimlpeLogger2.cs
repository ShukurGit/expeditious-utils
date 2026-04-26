
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Expeditious.Candidates.SimpleLogger_a2
{
    public class SimlpeLogger2
    {
        private static readonly BlockingCollection<string> _queue = new();
        private static readonly CancellationTokenSource _cts = new();
        private static Task _worker;

        private static EasyLoggerOptions _options = new EasyLoggerOptions();
    }
}
