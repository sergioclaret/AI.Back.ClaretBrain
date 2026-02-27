using ClaretBrain.Domain.Common;
using ClaretBrain.Domain.Enums;

namespace ClaretBrain.Domain.Entities;

public class Agent : Entity
{
    public string Name { get; set; } = string.Empty;
    public AgentStatus Status { get; set; } = AgentStatus.Offline;
    public string? CurrentTask { get; set; }
    public string Model { get; set; } = string.Empty;
}
