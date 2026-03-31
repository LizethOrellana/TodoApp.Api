using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Models;
using TodoApp.Api.Data;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskEntity>> GetByProjectId(int projectId)
    {
        return await _context.Tasks
            .Where(t => t.ProjectId == projectId && t.Status != TaskStates.Deleted)
            .ToListAsync();
    }

    public async Task<List<TaskEntity>> GetDeleted()
    {
        return await _context.Tasks
            .Where(t => t.Status == TaskStates.Deleted)
            .ToListAsync();
    }

    public async Task Add(TaskEntity task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task Update(TaskEntity task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            task.Status = TaskStates.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}