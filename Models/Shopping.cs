using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Compra.Models;

[Index(nameof(Name))]
public class Shopping : BaseEntity
{
    [Required]
    [MinLength(2), MaxLength(20)]
    public required string Name { get; set; }

    [Required]
    [Column(TypeName = "decimal(19,4)")]
    public required decimal Price { get; set; }
}

public class ShoppingDTO
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MinLength(2), MaxLength(20)]
    public required string Name { get; set; }

    [Required]
    [Column(TypeName = "decimal(19,4)")]
    public required decimal Price { get; set; }
}

/* public class ShoppingCreateRequestDTO
{
    public string? Name { get; set; }
}

public class ShoppingCreateResponseDTO
{
    public long Id { get; set; }
    public string? Name { get; set; }
} */
