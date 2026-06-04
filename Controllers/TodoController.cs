using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.DTOs;
using TodoApp.Models;

namespace TodoApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var todos = await db.Todos.ToListAsync();
        return Ok(ApiResponse<List<Todo>>.Ok(todos, "data berhasil"));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var todo = await db.Todos.FindAsync(id);
        if (todo == null) return NotFound(ApiResponse<Todo>.Fail("data tidak ditemukan"));
        return Ok(ApiResponse<Todo>.Ok(todo, "data berhasil"));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TodoCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<string>.Fail("Validasi gagal"));
            // ModelState
            // .Where(x => x.Value?.Errors.Count > 0)
            // .ToDictionary(
                // x => x.Key,
                // x => x.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
            // )));
        }

        var todo = new Todo
        {
            Title = dto.Title,
            IsCompleted = dto.IsCompleted
        };

        await db.Todos.AddAsync(todo);
        await db.SaveChangesAsync();
        return Ok(ApiResponse<Todo>.Ok(todo, "data berhasil"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TodoUpdateDto dto)
    {
        if (!ModelState.IsValid){
            return BadRequest(ApiResponse<string>.Fail("Validasi gagal"));
        }

        var existingTodo = await db.Todos.FindAsync(id);
        if (existingTodo == null) return NotFound(ApiResponse<Todo>.Fail("data tidak ditemukan"));
        existingTodo.Title = dto.Title;
        existingTodo.IsCompleted = dto.IsCompleted;
        await db.SaveChangesAsync();
        return Ok(ApiResponse<Todo>.Ok(existingTodo, "data berhasil"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var todo = await db.Todos.FindAsync(id);
        if (todo == null) return NotFound(ApiResponse<Todo>.Fail("data tidak ditemukan"));
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Ok(ApiResponse<Todo>.Ok(todo, "data berhasil"));
    }
}