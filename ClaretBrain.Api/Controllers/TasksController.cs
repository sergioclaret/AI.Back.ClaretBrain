using ClaretBrain.Application.DTOs;
using ClaretBrain.Application.Interfaces;
using ClaretBrain.Domain.Entities;
using ClaretBrain.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ClaretBrain.Api.Controllers;

[ApiController]
[Route("tasks")]
public class TasksController(ITaskRepository repo) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<KanbanTaskDto>> GetAll()
    {
        var tasks = await repo.GetAllAsync();
        return tasks.Select(t => new KanbanTaskDto(
            t.Id, t.Title, t.Description, t.Agent, t.Model,
            t.Status.ToString().ToLower(), t.Priority.ToString().ToLower(), t.CreatedAt,
            t.Files.Select(f => new TaskFileDto(f.Id, f.Name, f.Size, f.Type, f.Url)).ToList()
        ));
    }

    [HttpPost]
    public async Task<ActionResult<KanbanTaskDto>> Create(KanbanTaskDto dto)
    {
        var task = new KanbanTask
        {
            Title = dto.Title,
            Description = dto.Description,
            Agent = dto.Agent,
            Model = dto.Model,
            Status = Enum.Parse<ClaretBrain.Domain.Enums.TaskStatus>(dto.Status, true),
            Priority = Enum.Parse<Priority>(dto.Priority, true),
            CreatedAt = dto.CreatedAt
        };
        task.Files = dto.Files.Select(f => new TaskFile { Name = f.Name, Size = f.Size, Type = f.Type, Url = f.Url }).ToList();
        await repo.AddAsync(task);
        return new KanbanTaskDto(task.Id, task.Title, task.Description, task.Agent, task.Model, task.Status.ToString().ToLower(), task.Priority.ToString().ToLower(), task.CreatedAt,
            task.Files.Select(f => new TaskFileDto(f.Id, f.Name, f.Size, f.Type, f.Url)).ToList());
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, KanbanTaskDto dto)
    {
        var task = await repo.GetByIdAsync(id);
        if (task == null) return NotFound();
        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Agent = dto.Agent;
        task.Model = dto.Model;
        task.Status = Enum.Parse<ClaretBrain.Domain.Enums.TaskStatus>(dto.Status, true);
        task.Priority = Enum.Parse<Priority>(dto.Priority, true);
        await repo.UpdateAsync(task);
        return NoContent();
    }
}
