using System.ComponentModel.DataAnnotations;

namespace Compra.Models;

public abstract class BaseEntity<TId>
{
    [Key]
    public TId? Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}