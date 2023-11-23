using Microsoft.EntityFrameworkCore;
using TodoApp.WebApi.Models;
using TodoApp.WebApi.Persistence.Mappings;

namespace TodoApp.WebApi.Persistence;

public class TodoDbContext : DbContext
{
    public DbSet<Todo> Todos { get; set; } = default!;

    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TodoEntityConfiguration());
    }
}
