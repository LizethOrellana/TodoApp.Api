using TodoApp.Api.Models;

namespace TodoApp.Api.Application.UseCases;

public class RestoreTask
{
    private readonly ITaskRepository _repo;

    public RestoreTask(ITaskRepository repo)
    {
        _repo = repo;
    }

    public async Task Execute(TaskEntity task)
    {
        task.Status = TaskStates.Pending;
        task.UpdatedAt = DateTime.UtcNow;

        await _repo.Update(task);
    }
}