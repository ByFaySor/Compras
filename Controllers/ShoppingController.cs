using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Compra.Models;
using Compra.Repositories;

namespace Compra.AddControllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingController : ControllerBase
{
    private readonly IShoppingRepository _repository;

    public ShoppingController(IShoppingRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<Shopping>> ListShopping()
    {
        return await _repository.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShoppingDTO>> GetShopping(long id)
    {
        var shopping = await _repository.GetById(id);

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

        shopping = await _repository.Insert(shopping);

        return ShoppingToDTO(shopping);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutShopping(long id, ShoppingDTO shoppingDTO)
    {
        var shoppingItem = await _repository.GetById(id);

        if (shoppingItem == null)
        {
            return NotFound();
        }

        shoppingItem.Name = shoppingDTO.Name;
        shoppingItem.Price = shoppingDTO.Price;

        try
        {
            await _repository.Update(shoppingItem);
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
        var shopping = await _repository.GetById(id);

        if (shopping == null)
        {
            return NotFound();
        }

        await _repository.Delete(shopping);

        return NoContent();
    }

    private static ShoppingDTO ShoppingToDTO(Shopping shopping)
    {
        return new ShoppingDTO
        {
            Id = shopping.Id,
            Name = shopping.Name,
            Price = shopping.Price
        };
    }

    private bool ShoppingExists(long id)
    {
        return _repository.Any(e => e.Id == id);
    }
}
