namespace TodoApp.WebApi.Dtos;

public sealed record TodoDto(Guid Id, string Title, bool IsCompleted);