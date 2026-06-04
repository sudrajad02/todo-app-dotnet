using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
namespace TodoApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloController: ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(ApiResponse<string>.Ok("Hello World!"));
    }

    [HttpGet("error")]
    public IActionResult Error()
    {
        return BadRequest(ApiResponse<string>.Fail("Error!"));
    }
}