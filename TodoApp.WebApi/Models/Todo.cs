namespace TodoApp.WebApi.Models;

public sealed class Todo
{
    public Guid Id { get; private init; }
    public string Title { get; private set; } = default!;
    public bool IsCompleted { get; private set; }

    private Todo() { }

    public Todo(Guid id, string title, bool isCompleted)
    {
        ArgumentNullException.ThrowIfNull(title);

        Id = id;
        Title = title;
        IsCompleted = isCompleted;
    }
}
