/* using System.ComponentModel.DataAnnotations; */

namespace Compra.Models;

public class Shopping
{
    /* [Key] */
    public long Id { get; set; }

    /* [Required]
    [StringLength(maximumLength:120)] */
    public string? Name { get; set; }
}

public class ShoppingDTO
{
    /* [Key] */
    public long Id { get; set; }

    /* [Required]
    [StringLength(maximumLength: 120)] */
    public string? Name { get; set; }
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
