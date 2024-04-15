using System.Net;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.WebApi.Dtos;
using TodoApp.WebApi.Persistence;

namespace TodoApp.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class CreateTodoStepDefinitions
{
    private readonly TodoWebApplicationFactory _webApplicationFactory;
    
    private TodoDto? _createTodoDto;
    private HttpResponseMessage? _responseMessage;

    public CreateTodoStepDefinitions(TodoWebApplicationFactory webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }
    
    [Given("the following data to create a Todo")]
    public void GivenTheFollowingDataToCreateATodo(DataTable table)
    {
        _createTodoDto = table.CreateInstance<TodoDto>();
    }

    [When("the Todo is created")]
    public async Task WhenTheTodoIsCreated()
    {
        using var client = _webApplicationFactory.CreateClient();

        _responseMessage = await client.PostAsync("/todos", JsonContent.Create(_createTodoDto));
    }

    [Then("the created Todo should have the following data")]
    public async Task ThenTheCreatedTodoShouldHaveTheFollowingData(DataTable table)
    {
        _responseMessage!.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var scope = _webApplicationFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();

        var expectedTodo = table.CreateInstance<TodoData>();
        var createdTodo = await dbContext.Todos.SingleAsync();

        createdTodo.Id.Should().Be(expectedTodo.Id);
        createdTodo.Title.Should().Be(expectedTodo.Title);
        createdTodo.IsCompleted.Should().Be(expectedTodo.IsCompleted);
    }
}