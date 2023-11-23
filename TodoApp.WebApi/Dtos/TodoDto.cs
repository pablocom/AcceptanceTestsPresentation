namespace TodoApp.WebApi.Dtos;

public record TodoDto(Guid Id, string Title, bool IsComplete);