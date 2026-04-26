


namespace Expeditious.Candidates
{
    public static class TraceContext
    {
        private static readonly AsyncLocal<string> _traceId = new();

        public static string TraceId
        {
            get => _traceId.Value;
            set => _traceId.Value = value;
        }
    }
}
