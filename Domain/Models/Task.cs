using TodoApp.Api.Models;

public class TaskEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = TaskStates.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public int ProjectId { get; set; }

    public ProjectEntity? Project { get; set; }

    public int? UserId { get; set; }
    public UserEntity? User { get; set; }
}