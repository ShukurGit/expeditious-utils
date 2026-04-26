


namespace Expeditious.Candidates.SimpleLogger_a2
{
    public static class SimpleTraceContext
    {
        private static readonly AsyncLocal<string> _traceId = new();

        public static string TraceId
        {
            get => _traceId.Value;
            set => _traceId.Value = value;
        }
    }
}