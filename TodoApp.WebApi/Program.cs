using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TodoApp.WebApi;
using TodoApp.WebApi.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<TodoDbContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapTodoEndpoints();

await app.RunAsync();
