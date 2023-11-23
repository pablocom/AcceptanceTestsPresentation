namespace TodoApp.AcceptanceTests.Hooks;

[Binding]
public sealed class TodoWebApplicationFactoryHolder
{
    public static TodoWebApplicationFactory Instance { get; private set; } = default!;

    [BeforeTestRun]
    public static void InitializeWebApplicationFactory() => Instance = new TodoWebApplicationFactory();
}