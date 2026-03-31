using Microsoft.AspNetCore.SignalR;
using TodoApp.Api.Hubs;
using TodoApp.Api.Models;

public class AddTask
{
    private readonly ITaskRepository _repo;
    private readonly IHubContext<TaskHub> _hub;

    public AddTask(ITaskRepository repo, IHubContext<TaskHub> hub)
    {
        _repo = repo;
        _hub = hub;
    }

    public async Task Execute(TaskEntity task)
    {
        task.Status = TaskStates.Pending;
        await _repo.Add(task);

        await _hub.Clients.All.SendAsync("taskCreated", task);
    }
}