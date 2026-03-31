using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Data;
using TodoApp.Api.Models;
using Microsoft.AspNetCore.SignalR;
using TodoApp.Api.Hubs;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<TaskHub> _hubContext;

        public TasksController(AppDbContext context, IHubContext<TaskHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/tasks
        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.User)
                .Where(t => t.Status != TaskStates.Deleted)
                .ToList();

            return Ok(tasks);
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<IActionResult> AddTask(TaskEntity task)
        {
            task.Status = TaskStates.Pending; // por defecto nueva tarea está pendiente
            _context.Tasks.Add(task);
            _context.SaveChanges();

            await _hubContext.Clients.All.SendAsync("TasksUpdated", _context.Tasks.ToList());

            return Ok(task);
        }

        // PUT: api/tasks/{id}/complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();

            task.Status = TaskStates.Done;
            task.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();

            await _hubContext.Clients.All.SendAsync("TasksUpdated", _context.Tasks.ToList());

            return Ok(task);
        }

        // PUT: api/tasks/{id} → editar título, descripción, proyecto, usuario, etc.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskEntity updatedTask)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Status = updatedTask.Status;
            task.ProjectId = updatedTask.ProjectId; // 👈 actualizar proyecto
            task.UserId = updatedTask.UserId;       // 👈 actualizar usuario asignado
            task.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            await _hubContext.Clients.All.SendAsync("TasksUpdated", _context.Tasks.ToList());

            return Ok(task);
        }

        // PUT: api/tasks/{id}/assign/{userId} → asignar usuario
        [HttpPut("{id}/assign/{userId}")]
        public IActionResult AssignTask(int id, int userId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (task == null || user == null) return NotFound();

            task.UserId = userId;
            task.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();

            return Ok(task);
        }

        // DELETE: api/tasks/{id} → eliminación lógica
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();

            task.Status = TaskStates.Deleted;
            task.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();

            await _hubContext.Clients.All.SendAsync("TasksUpdated", _context.Tasks.ToList());

            return Ok(task);
        }

        // PUT: api/tasks/{id}/start → mover a InProcess
        [HttpPut("{id}/start")]
        public async Task<IActionResult> StartTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();

            task.Status = TaskStates.InProcess;
            task.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();

            await _hubContext.Clients.All.SendAsync("TasksUpdated", _context.Tasks.ToList());

            return Ok(task);
        }

        // GET: api/tasks/status → conteo de los 4 estados
        [HttpGet("status")]
        public IActionResult GetTaskStatus()
        {
            var done = _context.Tasks.Count(t => t.Status == TaskStates.Done);
            var pending = _context.Tasks.Count(t => t.Status == TaskStates.Pending);
            var inProcess = _context.Tasks.Count(t => t.Status == TaskStates.InProcess);
            var deleted = _context.Tasks.Count(t => t.Status == TaskStates.Deleted);

            return Ok(new { done, pending, inProcess, deleted });
        }

        // GET: api/tasks/deleted
        [HttpGet("deleted")]
        public IActionResult GetDeletedTasks()
        {
            var deletedTasks = _context.Tasks
                .Where(t => t.Status == TaskStates.Deleted)
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    ProjectId = t.ProjectId,
                    UserId = t.UserId ?? 0
                })
                .ToList();

            return Ok(deletedTasks);
        }

        // GET: api/tasks/search?query=texto
        [HttpGet("search")]
        public IActionResult SearchTasks([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Debe ingresar un texto para buscar.");

            var tasks = _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.User)
                .Where(t => t.Status != TaskStates.Deleted &&
                            (t.Title.ToLower().Contains(query.ToLower()) ||
                             (t.Description != null && t.Description.ToLower().Contains(query.ToLower()))))
                .ToList();

            return Ok(tasks);
        }

        [HttpGet("by-project/{projectId}")]
        public IActionResult GetTasksByProject(int projectId)
        {
            var tasks = _context.Tasks
                .Where(t => t.ProjectId == projectId && t.Status != TaskStates.Deleted)
                .ToList();

            return Ok(tasks);
        }


    }
}