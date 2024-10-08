using System.Globalization;
using System.Text;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace TodoApp.AcceptanceTests.TestOutputLogging;

public sealed class TestOutputLogger : ILogger
{
    private const string LogLevelPadding = ": ";
    private static readonly string MessagePadding = new(' ', 7);
    private static readonly string NewLineWithMessagePadding = Environment.NewLine + MessagePadding;
    private const string TimestampFormat = "u";

    [ThreadStatic]
    private static StringBuilder? _logBuilder;

    private readonly string _categoryName;
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly TimeProvider _timeProvider;

    public TestOutputLogger(string categoryName, ITestOutputHelper testOutputHelper, TimeProvider timeProvider)
    {
        _categoryName = categoryName;
        _testOutputHelper = testOutputHelper;
        _timeProvider = timeProvider;
    }

    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        ArgumentNullException.ThrowIfNull(state);

        return LogScopeContext.Push(state);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel is not LogLevel.None;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        var message = formatter(state, exception);

        if (!string.IsNullOrEmpty(message) || exception is not null)
            WriteMessage(logLevel, eventId.Id, message, exception);
    }

    private void WriteMessage(LogLevel logLevel, int eventId, string? message, Exception? exception)
    {
        var logBuilder = _logBuilder;
        _logBuilder = null;
        logBuilder ??= new StringBuilder();

        AppendTimestamp(logBuilder);
        AppendLogLevel(logLevel, logBuilder);
        AppendCategoryNameWithEventId(eventId, logBuilder);
        logBuilder.AppendLine();
        AppendScopeInformation(logBuilder);

        var hasMessage = !string.IsNullOrEmpty(message);

        if (hasMessage)
        {
            logBuilder.Append(MessagePadding);

            var length = logBuilder.Length;
            logBuilder.Append(message);
            logBuilder.Replace(Environment.NewLine, NewLineWithMessagePadding, length, message!.Length);
        }

        if (exception is not null)
        {
            if (hasMessage)
                logBuilder.AppendLine();

            logBuilder.Append(exception);
        }

        try
        {
            _testOutputHelper.WriteLine(logBuilder.ToString());
        }
        catch (InvalidOperationException)
        {
            // Ignore exception if the application tries to log after the test ends
            // but before the ITestOutputHelper is detached, e.g. "There is no currently active test."
        }

        logBuilder.Clear();

        if (logBuilder.Capacity > 1024)
            logBuilder.Capacity = 1024;

        _logBuilder = logBuilder;
    }

    private void AppendTimestamp(StringBuilder logBuilder)
    {
        logBuilder.Append('[');
        logBuilder.Append(_timeProvider.GetLocalNow().ToString(TimestampFormat, CultureInfo.CurrentCulture));
        logBuilder.Append("] ");
    }

    private static void AppendLogLevel(LogLevel logLevel, StringBuilder logBuilder)
    {
        var logLevelText = logLevel switch
        {
            LogLevel.Critical => "crit",
            LogLevel.Debug => "dbug",
            LogLevel.Error => "fail",
            LogLevel.Information => "info",
            LogLevel.Trace => "trce",
            LogLevel.Warning => "warn",
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel)),
        };

        logBuilder.Append(logLevelText);
        logBuilder.Append(LogLevelPadding);
    }

    private void AppendCategoryNameWithEventId(int eventId, StringBuilder logBuilder)
    {
        logBuilder.Append(_categoryName);
        logBuilder.Append('[');
        logBuilder.Append(eventId);
        logBuilder.Append(']');
    }

    private static void AppendScopeInformation(StringBuilder builder)
    {
        var currentScope = LogScopeContext.Current;
        var scopesStack = new Stack<LogScope>();

        while (currentScope is not null)
        {
            scopesStack.Push(currentScope);
            currentScope = currentScope.Parent;
        }

        var depth = 0;
        while (scopesStack.Count > 0)
        {
            var scope = scopesStack.Pop();

            foreach (var scopeProperty in scope.GetProperties())
            {
                builder.Append(MessagePadding)
                    .Append(' ', depth * 2)
                    .Append("=> ")
                    .Append(scopeProperty)
                    .AppendLine();
            }

            depth++;
        }
    }
}