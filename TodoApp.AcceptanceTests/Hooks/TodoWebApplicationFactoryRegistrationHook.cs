using Reqnroll.BoDi;

namespace TodoApp.AcceptanceTests.Hooks;

[Binding]
public sealed class TodoWebApplicationFactoryRegistrationHook
{
    [BeforeTestRun]
    public static void InitializeWebApplicationFactory(IObjectContainer objectContainer)
    {
        objectContainer.RegisterInstanceAs(new TodoWebApplicationFactory());
    }
}