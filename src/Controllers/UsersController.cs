using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = new[] {
            new { Id = 1, Name = "John" },
            new { Id = 2, Name = "Jane" }
        };
        return Ok(users);
    }
}
