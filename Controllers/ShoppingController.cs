using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Compras.Models;
using Compras.Services;

namespace Compras.AddControllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingController : ControllerBase
{
    private readonly ShoppingService _service;

    public ShoppingController(ShoppingService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<Shopping>> GetAll()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShoppingDTO>> GetById(long id)
    {
        var shopping = await _service.GetById(id);

        if (shopping is null)
        {
            return NotFound();
        }

        return ShoppingToDTO(shopping);
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingDTO>> create(ShoppingDTO shoppingDTO)
    {
        var shopping = await _service.Create(shoppingDTO);

        return ShoppingToDTO(shopping);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutShopping(long id, ShoppingDTO shoppingDTO)
    {
        if (id != shoppingDTO.Id)
        {
            return BadRequest();
        }

        try
        {
            var shopping = await _service.Update(id, shoppingDTO);

            if (shopping is null)
            {
                return NotFound();
            }
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
        var shopping = await _service.Delete(id);

        if (shopping is null)
        {
            return NotFound();
        }

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

    private bool ShoppingExists(long id) => _service.Any(id);
}
