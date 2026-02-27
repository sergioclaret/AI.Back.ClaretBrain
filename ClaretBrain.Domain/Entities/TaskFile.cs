using ClaretBrain.Domain.Common;

namespace ClaretBrain.Domain.Entities;

public class TaskFile : Entity
{
    public string Name { get; set; } = string.Empty;
    public long Size { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
