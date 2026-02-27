using ClaretBrain.Application.DTOs;
using ClaretBrain.Application.Interfaces;
using ClaretBrain.Domain.Entities;
using ClaretBrain.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ClaretBrain.Api.Controllers;

[ApiController]
[Route("agents")]
public class AgentsController(IAgentRepository repo) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<AgentDto>> GetAll()
    {
        var agents = await repo.GetAllAsync();
        return agents.Select(a => new AgentDto(a.Id, a.Name, a.Status.ToString().ToLower(), a.CurrentTask, a.Model));
    }

    [HttpPost]
    public async Task<ActionResult<AgentDto>> Create(AgentDto dto)
    {
        var agent = new Agent { Name = dto.Name, Status = Enum.Parse<AgentStatus>(dto.Status, true), CurrentTask = dto.CurrentTask, Model = dto.Model };
        await repo.AddAsync(agent);
        return new AgentDto(agent.Id, agent.Name, agent.Status.ToString().ToLower(), agent.CurrentTask, agent.Model);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, AgentDto dto)
    {
        var agent = await repo.GetByIdAsync(id);
        if (agent == null) return NotFound();
        agent.Name = dto.Name;
        agent.CurrentTask = dto.CurrentTask;
        agent.Model = dto.Model;
        agent.Status = Enum.Parse<AgentStatus>(dto.Status, true);
        await repo.UpdateAsync(agent);
        return NoContent();
    }
}
