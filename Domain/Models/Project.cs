namespace TodoApp.Api.Models
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Relación uno a muchos con tareas
        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}