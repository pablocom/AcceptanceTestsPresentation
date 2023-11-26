using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.WebApi.Dtos;
using TodoApp.WebApi.Models;
using TodoApp.WebApi.Persistence;

namespace TodoApp.WebApi;

public static class Endpoints
{
    public static void MapTodoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("todos", CreateTodo);
        app.MapDelete("todos/{id}", DeleteTodo);
    }

    private static async Task<IResult> CreateTodo([FromServices] TodoDbContext dbContext, [FromBody] TodoDto dto)
    {
        var todo = new Todo(dto.Id, dto.Title, dto.IsCompleted);
        dbContext.Add(todo);
        
        await dbContext.SaveChangesAsync();
        
        return Results.Ok();
    }

    private static async Task<IResult> DeleteTodo([FromServices] TodoDbContext dbContext, [FromRoute] Guid id)
    {
        var todoToDelete = await dbContext.Todos.FirstOrDefaultAsync(x => x.Id == id);
        dbContext.Remove(todoToDelete);
        
        await dbContext.SaveChangesAsync();
        
        return Results.Ok();
    }
}