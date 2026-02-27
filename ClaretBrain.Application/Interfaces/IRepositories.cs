using ClaretBrain.Domain.Entities;

namespace ClaretBrain.Application.Interfaces;

public interface IAgentRepository
{
    Task<List<Agent>> GetAllAsync();
    Task<Agent?> GetByIdAsync(Guid id);
    Task AddAsync(Agent agent);
    Task UpdateAsync(Agent agent);
}

public interface ITaskRepository
{
    Task<List<KanbanTask>> GetAllAsync();
    Task<KanbanTask?> GetByIdAsync(Guid id);
    Task AddAsync(KanbanTask task);
    Task UpdateAsync(KanbanTask task);
}

public interface IFeedRepository
{
    Task<List<FeedThread>> GetThreadsAsync();
    Task<FeedThread?> GetThreadByIdAsync(Guid id);
    Task AddThreadAsync(FeedThread thread);
    Task AddActionAsync(Guid threadId, FeedAction action);
}
