using Reqnroll.BoDi;

namespace TodoApp.AcceptanceTests.Hooks;

[Binding]
public sealed class TodoWebApplicationFactoryRegistrationHook
{
    private readonly IObjectContainer _objectContainer;

    public TodoWebApplicationFactoryRegistrationHook(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    [BeforeScenario(Order = 0)]
    public void RegisterWebApplicationFactoryInContainer()
    {
        if (_objectContainer.IsRegistered<TodoWebApplicationFactory>())
            return;
        
        _objectContainer.RegisterInstanceAs(TodoWebApplicationFactoryHolder.Instance);
    }
}

[Binding]
public sealed class TodoWebApplicationFactoryHolder
{
    public static TodoWebApplicationFactory Instance { get; private set; } = default!;

    [BeforeTestRun]
    public static void InitializeWebApplicationFactory() => Instance = new();
}