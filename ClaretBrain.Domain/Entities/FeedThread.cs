using ClaretBrain.Domain.Common;
using ClaretBrain.Domain.Enums;

namespace ClaretBrain.Domain.Entities;

public class FeedThread : Entity
{
    public string Agent { get; set; } = string.Empty;
    public string TaskTitle { get; set; } = string.Empty;
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public FeedThreadStatus Status { get; set; } = FeedThreadStatus.Running;
    public List<FeedAction> Actions { get; set; } = new();
}
