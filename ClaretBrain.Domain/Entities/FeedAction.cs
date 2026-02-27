using ClaretBrain.Domain.Common;
using ClaretBrain.Domain.Enums;

namespace ClaretBrain.Domain.Entities;

public class FeedAction : Entity
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public FeedActionType Type { get; set; } = FeedActionType.Info;
    public string Message { get; set; } = string.Empty;
    public string? Model { get; set; }
    public int? TokensIn { get; set; }
    public int? TokensOut { get; set; }
    public string? Duration { get; set; }
    public string? Command { get; set; }
    public string? Output { get; set; }
}
