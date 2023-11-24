using Microsoft.AspNetCore.Mvc;
using TodoApp.WebApi.Dtos;
using TodoApp.WebApi.Models;
using TodoApp.WebApi.Persistence;

namespace TodoApp.WebApi;

public static class Endpoints
{
    public static void MapTodoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("todos", CreateTodo);
    }

    private static async Task<IResult> CreateTodo([FromServices] TodoDbContext dbContext, [FromBody] TodoDto dto)
    {
        var todo = new Todo(dto.Id, dto.Title, dto.IsCompleted);

        dbContext.Add(todo);
        await dbContext.SaveChangesAsync();
        
        return Results.Ok();
    }
}