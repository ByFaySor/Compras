using Microsoft.AspNetCore.Mvc;
using Compra.Models;
using Microsoft.EntityFrameworkCore;

namespace Compra.AddControllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingController : ControllerBase
{
    public readonly AppDbContext _context;

    public ShoppingController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShoppingDTO>>> ListShopping()
    {
        return await _context.Shoppings
            .Select(shopping => ShoppingToDTO(shopping))
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShoppingDTO>> GetShopping(long id)
    {
        var shopping = await _context.Shoppings.FindAsync(id);

        if (shopping == null)
        {
            return NotFound();
        }

        return ShoppingToDTO(shopping);
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingDTO>> PostShopping(ShoppingDTO shoppingDTO)
    {
        var shopping = new Shopping
        {
            Name = shoppingDTO.Name,
            Price = shoppingDTO.Price,
        };

        _context.Shoppings.Add(shopping);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetShopping),
            new { id = shopping.Id },
            ShoppingToDTO(shopping)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutShopping(long id, ShoppingDTO shoppingDTO)
    {
        var shoppingItem = await _context.Shoppings.FindAsync(id);

        if (shoppingItem == null)
        {
            return NotFound();
        }

        shoppingItem.Name = shoppingDTO.Name;
        shoppingItem.Price = shoppingDTO.Price;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!ShoppingExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShopping(long id)
    {
        var shopping = await _context.Shoppings.FindAsync(id);

        if (shopping == null)
        {
            return NotFound();
        }

        _context.Shoppings.Remove(shopping);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static ShoppingDTO ShoppingToDTO(Shopping shopping) =>
       new ShoppingDTO
       {
           Id = shopping.Id,
           Name = shopping.Name,
           Price = shopping.Price
       };

    private bool ShoppingExists(long id)
    {
        return _context.Shoppings.Any(e => e.Id == id);
    }
}
