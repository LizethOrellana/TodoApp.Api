using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Data;
using TodoApp.Api.Models;
using Microsoft.AspNetCore.SignalR;
using TodoApp.Api.Hubs;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/projects
        [HttpGet]
        public IActionResult GetProjects()
        {
            var projects = _context.Projects.ToList();
            return Ok(projects);
        }

        // POST: api/projects
        [HttpPost]
        public IActionResult AddProject(ProjectEntity project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
            return Ok(project);
        }

        // GET: api/projects/{id}/tasks
        [HttpGet("{id}/tasks")]
        public IActionResult GetTasksByProject(int id)
        {
            var tasks = _context.Tasks.Where(t => t.ProjectId == id && t.Status != TaskStates.Deleted).ToList();
            return Ok(tasks);
        }
    }
}