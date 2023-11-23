using BoDi;

namespace TodoApp.AcceptanceTests.Hooks;

[Binding]
public sealed class WebApplicationFactoryRegistrationHook
{
    private readonly IObjectContainer _objectContainer;

    public WebApplicationFactoryRegistrationHook(IObjectContainer objectContainer)
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