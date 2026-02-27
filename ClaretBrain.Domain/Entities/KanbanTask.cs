using ClaretBrain.Domain.Common;
using ClaretBrain.Domain.Enums;

namespace ClaretBrain.Domain.Entities;

public class KanbanTask : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Agent { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public ClaretBrain.Domain.Enums.TaskStatus Status { get; set; } = ClaretBrain.Domain.Enums.TaskStatus.Todo;
    public Priority Priority { get; set; } = Priority.Medium;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<TaskFile> Files { get; set; } = new();
}
