using Microsoft.AspNetCore.SignalR;
using TodoApp.Api.Hubs;
using TodoApp.Api.Models;

namespace TodoApp.Api.Application.UseCases;

public class DeleteTask
{
    private readonly ITaskRepository _repo;

    private readonly IHubContext<TaskHub> _hub;

    public DeleteTask(ITaskRepository repo, IHubContext<TaskHub> hub)
    {
        _repo = repo;
        _hub = hub;
    }

    public async Task Execute(int id)
    {
        await _repo.Delete(id);
        await _hub.Clients.All.SendAsync("taskDeleted", id);
    }
}