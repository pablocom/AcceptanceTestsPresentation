using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.WebApi.Persistence;

namespace TodoApp.AcceptanceTests.Hooks;

[Binding]
public sealed class ScenarioHooks
{
    private readonly TodoWebApplicationFactory _webApplicationFactory;

    public ScenarioHooks(TodoWebApplicationFactory webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [BeforeScenario, AfterScenario]
    public async Task CleanDatabase()
    {
        await using var scope = _webApplicationFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();

        await dbContext.Todos.ExecuteDeleteAsync();
    }
}