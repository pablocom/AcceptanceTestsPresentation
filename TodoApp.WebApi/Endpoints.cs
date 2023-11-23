using TodoApp.WebApi.Dtos;
using TodoApp.WebApi.Models;
using TodoApp.WebApi.Persistence;

namespace TodoApp.WebApi;

public static class Endpoints
{
    public static IEndpointRouteBuilder MapTodoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("todos");

        group.MapPost("", async (TodoDbContext dbContext, TodoDto dto) =>
        {
            var todo = new Todo(dto.Id, dto.Title, dto.IsCompleted);

            dbContext.Add(todo);
            await dbContext.SaveChangesAsync();
        });
        
        return app;
    }
}