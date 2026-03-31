using TodoApp.Api.Models;

namespace TodoApp.Api.Application.UseCases;

public class GetDeletedTasks
{
    private readonly ITaskRepository _repo;

    public GetDeletedTasks(ITaskRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<TaskEntity>> Execute()
    {
        return await _repo.GetDeleted();
    }
}