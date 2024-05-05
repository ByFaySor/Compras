using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Compras.Models;

[Index(nameof(Name))]
public class Shopping : BaseEntity<long>
{
    [Required]
    [MinLength(2), MaxLength(20)]
    public required string Name { get; set; }

    [Required]
    [Column(TypeName = "decimal(19,4)")]
    public required decimal Price { get; set; }
}
