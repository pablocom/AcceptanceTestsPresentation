using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow.Assist;
using TodoApp.WebApi.Models;
using TodoApp.WebApi.Persistence;

namespace TodoApp.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class DeleteTodoStepDefinitions
{
    private readonly TodoWebApplicationFactory _webApplicationFactory;
    private HttpResponseMessage? _responseMessage;

    public DeleteTodoStepDefinitions(TodoWebApplicationFactory webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }
    
    [Given("the existing todos")]
    public async Task GivenTheExistingTodos(Table table)
    {
        await using var scope = _webApplicationFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();

        var rows = table.CreateSet<TodoData>();
        foreach (var row in rows) 
            dbContext.Add(new Todo(row.Id, row.Title, row.IsCompleted));

        await dbContext.SaveChangesAsync();
    }

    [When(@"deleting the Todo with ID ""(.*)""")]
    public async Task WhenDeletingTheTodoWithId(Guid id)
    {
        using var client = _webApplicationFactory.CreateClient();
        
        _responseMessage = await client.DeleteAsync($"/todos/{id}");
    }

    [Then(@"the todo with ID ""(.*)"" should no longer exist")]
    public async Task ThenTheTodoWithIdShouldNoLongerExist(Guid id)
    {
        _responseMessage!.StatusCode.Should().Be(HttpStatusCode.OK);
        
        await using var scope = _webApplicationFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();

        var isTodoPresent = await dbContext.Todos.AnyAsync(x => x.Id == id);
        isTodoPresent.Should().BeFalse();
    }
}
