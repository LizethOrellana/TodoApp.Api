
public class GetTasksByProject
{
    private readonly ITaskRepository _repo;

    public GetTasksByProject(ITaskRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<TaskEntity>> Execute(int projectId)
    {
        return await _repo.GetByProjectId(projectId);
    }
}