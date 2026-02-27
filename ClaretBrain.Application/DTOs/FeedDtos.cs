namespace ClaretBrain.Application.DTOs;

public record FeedActionDto(
    Guid Id,
    DateTime Timestamp,
    string Type,
    string Message,
    string? Model,
    int? TokensIn,
    int? TokensOut,
    string? Duration,
    string? Command,
    string? Output
);

public record FeedThreadDto(
    Guid Id,
    string Agent,
    string TaskTitle,
    DateTime StartedAt,
    string Status,
    List<FeedActionDto> Actions
);
