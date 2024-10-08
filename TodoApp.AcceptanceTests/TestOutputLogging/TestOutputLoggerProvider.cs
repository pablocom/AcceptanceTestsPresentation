using Microsoft.Extensions.Logging;

namespace TodoApp.AcceptanceTests.TestOutputLogging;

public sealed class TestOutputLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new TestOutputLogger(categoryName, TestOutputHelperInterceptor.CurrentTestOutputHelper, TimeProvider.System);
    }

    public void Dispose() { }
}