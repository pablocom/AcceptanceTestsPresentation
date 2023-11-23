namespace TodoApp.WebApi.Models;

public class Todo
{
    public Guid Id { get; init; }
    public string Title { get; private set; } = default!;
    public bool IsComplete { get; private set; }

    private Todo() { }

    public Todo(Guid id, string title, bool isComplete)
    {
        ArgumentNullException.ThrowIfNull(title);

        Id = id;
        Title = title;
        IsComplete = isComplete;
    }
}
