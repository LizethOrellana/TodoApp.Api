namespace TodoApp.Api.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}