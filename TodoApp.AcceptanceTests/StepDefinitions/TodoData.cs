namespace TodoApp.AcceptanceTests.StepDefinitions;

public sealed record TodoData(
    Guid Id, 
    string Title, 
    bool IsCompleted);