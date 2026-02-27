using ClaretBrain.Api.Seed;
using ClaretBrain.Infrastructure.Persistence;

namespace ClaretBrain.Api;

public static class ProgramSeed
{
    public static void SeedDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();
        DbSeeder.Seed(db);
    }
}
