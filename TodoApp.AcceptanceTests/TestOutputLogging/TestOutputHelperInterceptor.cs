using Xunit.Abstractions;

namespace TodoApp.AcceptanceTests.TestOutputLogging;

public static class TestOutputHelperInterceptor
{
    public static ITestOutputHelper CurrentTestOutputHelper { get; set; } = null!;
}