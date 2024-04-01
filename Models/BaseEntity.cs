using System.ComponentModel.DataAnnotations;

namespace Compra.Models;

public abstract class BaseEntity
{
    [Key]
    public long Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}