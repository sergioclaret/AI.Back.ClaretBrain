using ClaretBrain.Application.Interfaces;
using ClaretBrain.Domain.Entities;
using ClaretBrain.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClaretBrain.Infrastructure.Repositories;

public class AgentRepository(AppDbContext db) : IAgentRepository
{
    public async Task<List<Agent>> GetAllAsync() => await db.Agents.ToListAsync();
    public async Task<Agent?> GetByIdAsync(Guid id) => await db.Agents.FindAsync(id);
    public async Task AddAsync(Agent agent) { db.Agents.Add(agent); await db.SaveChangesAsync(); }
    public async Task UpdateAsync(Agent agent) { db.Agents.Update(agent); await db.SaveChangesAsync(); }
}

public class TaskRepository(AppDbContext db) : ITaskRepository
{
    public async Task<List<KanbanTask>> GetAllAsync() => await db.Tasks.Include(t => t.Files).ToListAsync();
    public async Task<KanbanTask?> GetByIdAsync(Guid id) => await db.Tasks.Include(t => t.Files).FirstOrDefaultAsync(t => t.Id == id);
    public async Task AddAsync(KanbanTask task) { db.Tasks.Add(task); await db.SaveChangesAsync(); }
    public async Task UpdateAsync(KanbanTask task) { db.Tasks.Update(task); await db.SaveChangesAsync(); }
}

public class FeedRepository(AppDbContext db) : IFeedRepository
{
    public async Task<List<FeedThread>> GetThreadsAsync() => await db.FeedThreads.Include(t => t.Actions).ToListAsync();
    public async Task<FeedThread?> GetThreadByIdAsync(Guid id) => await db.FeedThreads.Include(t => t.Actions).FirstOrDefaultAsync(t => t.Id == id);
    public async Task AddThreadAsync(FeedThread thread) { db.FeedThreads.Add(thread); await db.SaveChangesAsync(); }
    public async Task AddActionAsync(Guid threadId, FeedAction action)
    {
        var thread = await db.FeedThreads.Include(t => t.Actions).FirstOrDefaultAsync(t => t.Id == threadId);
        if (thread == null) return;
        thread.Actions.Add(action);
        await db.SaveChangesAsync();
    }
}
