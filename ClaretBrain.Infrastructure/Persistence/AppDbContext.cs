using ClaretBrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClaretBrain.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Agent> Agents => Set<Agent>();
    public DbSet<KanbanTask> Tasks => Set<KanbanTask>();
    public DbSet<TaskFile> Files => Set<TaskFile>();
    public DbSet<FeedThread> FeedThreads => Set<FeedThread>();
    public DbSet<FeedAction> FeedActions => Set<FeedAction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KanbanTask>()
            .HasMany(t => t.Files)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FeedThread>()
            .HasMany(t => t.Actions)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
