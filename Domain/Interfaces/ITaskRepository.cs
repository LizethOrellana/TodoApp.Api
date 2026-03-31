

public interface ITaskRepository
{
    Task<List<TaskEntity>> GetByProjectId(int projectId);
    Task<List<TaskEntity>> GetDeleted();
    Task Add(TaskEntity task);
    Task Update(TaskEntity task);
    Task Delete(int id);
}