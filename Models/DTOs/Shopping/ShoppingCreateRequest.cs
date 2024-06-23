using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Compras.Models.DTOs;

public class ShoppingCreateRequest
{
    [Required]
    [MinLength(2), MaxLength(20)]
    public required string Name { get; set; }

    [Required]
    [Precision(19, 4)]
    public decimal Price { get; set; }
}