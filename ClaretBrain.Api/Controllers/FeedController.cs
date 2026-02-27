using ClaretBrain.Application.DTOs;
using ClaretBrain.Application.Interfaces;
using ClaretBrain.Domain.Entities;
using ClaretBrain.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ClaretBrain.Api.Hubs;

namespace ClaretBrain.Api.Controllers;

[ApiController]
[Route("feed")]
public class FeedController(IFeedRepository repo, IHubContext<FeedHub> hub) : ControllerBase
{
    [HttpGet("threads")]
    public async Task<IEnumerable<FeedThreadDto>> GetThreads()
    {
        var threads = await repo.GetThreadsAsync();
        return threads.Select(t => new FeedThreadDto(
            t.Id, t.Agent, t.TaskTitle, t.StartedAt, t.Status.ToString().ToLower(),
            t.Actions.Select(a => new FeedActionDto(a.Id, a.Timestamp, a.Type.ToString().ToLower(), a.Message, a.Model, a.TokensIn, a.TokensOut, a.Duration, a.Command, a.Output)).ToList()
        ));
    }

    [HttpPost("threads")]
    public async Task<ActionResult<FeedThreadDto>> CreateThread(FeedThreadDto dto)
    {
        var thread = new FeedThread
        {
            Agent = dto.Agent,
            TaskTitle = dto.TaskTitle,
            StartedAt = dto.StartedAt,
            Status = Enum.Parse<FeedThreadStatus>(dto.Status, true)
        };
        thread.Actions = dto.Actions.Select(a => new FeedAction
        {
            Timestamp = a.Timestamp,
            Type = Enum.Parse<FeedActionType>(a.Type, true),
            Message = a.Message,
            Model = a.Model,
            TokensIn = a.TokensIn,
            TokensOut = a.TokensOut,
            Duration = a.Duration,
            Command = a.Command,
            Output = a.Output
        }).ToList();
        await repo.AddThreadAsync(thread);
        var outDto = new FeedThreadDto(thread.Id, thread.Agent, thread.TaskTitle, thread.StartedAt, thread.Status.ToString().ToLower(), thread.Actions.Select(a => new FeedActionDto(a.Id, a.Timestamp, a.Type.ToString().ToLower(), a.Message, a.Model, a.TokensIn, a.TokensOut, a.Duration, a.Command, a.Output)).ToList());
        await hub.Clients.All.SendAsync("feed:thread", outDto);
        return outDto;
    }

    [HttpPost("actions")]
    public async Task<IActionResult> AddAction(Guid threadId, FeedActionDto dto)
    {
        var action = new FeedAction
        {
            Timestamp = dto.Timestamp,
            Type = Enum.Parse<FeedActionType>(dto.Type, true),
            Message = dto.Message,
            Model = dto.Model,
            TokensIn = dto.TokensIn,
            TokensOut = dto.TokensOut,
            Duration = dto.Duration,
            Command = dto.Command,
            Output = dto.Output
        };
        await repo.AddActionAsync(threadId, action);
        await hub.Clients.All.SendAsync("feed:action", new FeedActionDto(action.Id, action.Timestamp, action.Type.ToString().ToLower(), action.Message, action.Model, action.TokensIn, action.TokensOut, action.Duration, action.Command, action.Output));
        return NoContent();
    }
}
