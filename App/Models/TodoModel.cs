using System.ComponentModel.DataAnnotations;
using App.Enums;

namespace App.Models;

public class TodoModel
{
    public int Id { get; set; }

    [Required, MinLength(3), MaxLength(40)]
    public required string Title { get; set; }

    [MaxLength(200)]
    public string? Description { get; set; }

    [EnumDataType(typeof(EStatus))]
    public EStatus Status { get; set; } = EStatus.Pending;
}