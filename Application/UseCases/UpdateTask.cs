using Microsoft.AspNetCore.SignalR;
using TodoApp.Api.Hubs;
using TodoApp.Api.Models;

namespace TodoApp.Api.Application.UseCases;

public class UpdateTask
{
    private readonly ITaskRepository _repo;
    private readonly IHubContext<TaskHub> _hub;

    public UpdateTask(ITaskRepository repo, IHubContext<TaskHub> hub)
    {
        _repo = repo;
        _hub = hub;
    }

    public async Task Execute(TaskEntity task)
    {
        task.UpdatedAt = DateTime.UtcNow;

        await _repo.Update(task);
        await _hub.Clients.All.SendAsync("taskUpdated", task);
    }
}