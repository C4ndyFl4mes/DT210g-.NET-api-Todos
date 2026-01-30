using System.ComponentModel.DataAnnotations;
using App.Enums;

namespace App.Models;

public class TodoModel
{
    public int Id { get; set; }

    [Required, MinLength(3), MaxLength(40)]
    public required string Title { get; set; }

    [Required, MinLength(3), MaxLength(150)]
    public required string Description { get; set; }

    [Required]
    public required EStatus Status { get; set; }
}