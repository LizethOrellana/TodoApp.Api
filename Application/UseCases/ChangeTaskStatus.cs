using TodoApp.Api.Models;

namespace TodoApp.Api.Application.UseCases;

public class ChangeTaskStatus
{
    private readonly ITaskRepository _repo;

    public ChangeTaskStatus(ITaskRepository repo)
    {
        _repo = repo;
    }

    public async Task Execute(TaskEntity task, string newStatus)
    {
        task.Status = newStatus;
        task.UpdatedAt = DateTime.UtcNow;

        await _repo.Update(task);
    }
}