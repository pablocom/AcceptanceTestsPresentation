namespace TodoApp.AcceptanceTests.TestOutputLogging;

public static class LogScopeContext
{
    private static readonly AsyncLocal<LogScope?> AsyncLocalScope = new();
    public static LogScope? Current
    {
        get => AsyncLocalScope.Value;
        private set => AsyncLocalScope.Value = value;
    }

    public static IDisposable Push(object state)
    {
        var previousScope = Current;

        Current = new LogScope(state, previousScope);

        return new ScopeDisposer();
    }

    private sealed class ScopeDisposer : IDisposable
    {
        public void Dispose()
        {
            Current = Current?.Parent;
        }
    }
}