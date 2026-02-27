using ClaretBrain.Api.Filters;
using ClaretBrain.Api.Hubs;
using ClaretBrain.Application.Interfaces;
using ClaretBrain.Infrastructure.Persistence;
using ClaretBrain.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IAgentRepository, AgentRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IFeedRepository, FeedRepository>();

builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors();
app.UseMiddleware<TokenAuthFilter>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapHub<FeedHub>("/ws");
app.MapGet("/health", () => Results.Ok(new { ok = true }));

app.SeedDatabase();

app.Run();
