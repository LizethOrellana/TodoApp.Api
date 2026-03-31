using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Data;
using TodoApp.Api.Models;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    public UsersController(AppDbContext context) => _context = context;

    [HttpGet]
    public IActionResult GetUsers() => Ok(_context.Users.ToList());

    [HttpPost]
    public IActionResult AddUser(UserEntity user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok(user);
    }
}