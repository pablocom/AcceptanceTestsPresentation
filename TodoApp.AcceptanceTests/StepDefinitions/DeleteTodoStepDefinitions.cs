using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    
    [Given(@"the existing todos")]
    public async Task GivenTheExistingTodos(DataTable table)
    {
        await using var scope = _webApplicationFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();

        var rows = table.CreateSet<TodoData>();
        foreach (var row in rows)
        {
            dbContext.Todos.Add(new Todo(row.Id, row.Title, row.IsCompleted));
        }

        await dbContext.SaveChangesAsync();
    }

    [When(@"deleting the Todo with ID ""(.*)""")]
    public async Task WhenDeletingTheTodoWithId(Guid todoId)
    {
        using var client = _webApplicationFactory.CreateClient();

        _responseMessage = await client.DeleteAsync($"todos/{todoId}");
    }


    [Then(@"the remaining todos should be")]
    public async Task ThenTheRemainingTodosShouldBe(DataTable table)
    {
        _responseMessage!.StatusCode.Should().Be(HttpStatusCode.OK);
        
        await using var scope = _webApplicationFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();

        var expectedTodos = table.CreateSet<TodoData>();
        var actualTodos = await dbContext.Todos.ToArrayAsync();

        actualTodos.Should().BeEquivalentTo(expectedTodos);
    }
}