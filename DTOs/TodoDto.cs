using System.ComponentModel.DataAnnotations;

namespace TodoApp.DTOs;

public class TodoCreateDto
{
    [Required(ErrorMessage = "Title wajib diisi")]
    [MinLength(3, ErrorMessage = "Title minimal 3 karakter")]
    [MaxLength(100, ErrorMessage = "Title maksimal 100 karakter")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Description maksimal 500 karakter")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "IsCompleted wajib diisi")]
    public bool IsCompleted { get; set; }
}

public class TodoUpdateDto
{
    [Required(ErrorMessage = "Title wajib diisi")]
    [MinLength(3, ErrorMessage = "Title minimal 3 karakter")]
    [MaxLength(100, ErrorMessage = "Title maksimal 100 karakter")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Description maksimal 500 karakter")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "IsCompleted wajib diisi")]
    public bool IsCompleted { get; set; }
}