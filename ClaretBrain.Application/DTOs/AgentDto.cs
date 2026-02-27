namespace ClaretBrain.Application.DTOs;

public record AgentDto(
    Guid Id,
    string Name,
    string Status,
    string? CurrentTask,
    string Model
);
