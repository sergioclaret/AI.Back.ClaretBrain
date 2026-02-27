using ClaretBrain.Domain.Common;
using ClaretBrain.Domain.Enums;

namespace ClaretBrain.Domain.Entities;

public class KanbanTask : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Agent { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public TaskStatus Status { get; set; } = TaskStatus.Todo;
    public Priority Priority { get; set; } = Priority.Medium;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<TaskFile> Files { get; set; } = new();
}
