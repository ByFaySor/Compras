namespace Compras.Models.DTOs;

public class ShoppingGetResponse
{
    public long Id { get; set; }
    public required string Name { get; set; }

    public decimal Price { get; set; }
}
