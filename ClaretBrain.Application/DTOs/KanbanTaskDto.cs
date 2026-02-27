namespace ClaretBrain.Application.DTOs;

public record TaskFileDto(Guid Id, string Name, long Size, string Type, string Url);

public record KanbanTaskDto(
    Guid Id,
    string Title,
    string Description,
    string Agent,
    string Model,
    string Status,
    string Priority,
    DateTime CreatedAt,
    List<TaskFileDto> Files
);
