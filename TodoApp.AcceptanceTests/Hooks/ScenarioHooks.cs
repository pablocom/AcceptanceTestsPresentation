using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.AcceptanceTests.TestOutputLogging;
using TodoApp.WebApi.Persistence;
using Xunit.Abstractions;

namespace TodoApp.AcceptanceTests.Hooks;

[Binding]
public sealed class ScenarioHooks
{
    private readonly TodoWebApplicationFactory _webApplicationFactory;
    private readonly ITestOutputHelper _testOutputHelper;

    public ScenarioHooks(TodoWebApplicationFactory webApplicationFactory, ITestOutputHelper testOutputHelper)
    {
        _webApplicationFactory = webApplicationFactory;
        _testOutputHelper = testOutputHelper;
    }

    [BeforeScenario(Order = 0)]
    public void InterceptTestOutputHelper()
    {
        TestOutputHelperInterceptor.CurrentTestOutputHelper = _testOutputHelper;
    }
    
    [BeforeScenario(Order = 1), AfterScenario]
    public async Task CleanDatabase()
    {
        await using var scope = _webApplicationFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();

        await dbContext.Todos.ExecuteDeleteAsync();
    }
}