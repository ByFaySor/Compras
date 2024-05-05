using System.ComponentModel.DataAnnotations;

namespace Compras.Models.DTOs;

public class ShoppingCreateRequest
{
    [Required]
    [MinLength(2), MaxLength(20)]
    public required string Name { get; set; }

    [Required]
    public decimal Price { get; set; }
}