using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using Compras.Services;
using Compras.Models.DTOs;

namespace Compras.AddControllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingController : ControllerBase
{
    private readonly ShoppingService _service;
    private readonly IMapper _mapper;

    public ShoppingController(ShoppingService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ShoppingGetResponse>> GetAll()
    {
        var shoppings = await _service.GetAll();

        return _mapper.Map<IEnumerable<ShoppingGetResponse>>(shoppings);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ShoppingGetResponse>> GetById(long id)
    {
        var shopping = await _service.GetById(id);

        if (shopping is null)
        {
            return NotFound();
        }

        return _mapper.Map<ShoppingGetResponse>(shopping);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ShoppingCreateResponse>> create(ShoppingCreateRequest payload)
    {
        var shopping = await _service.Create(payload);

        return CreatedAtAction(
            nameof(GetById),
            new { id = shopping.Id },
            new {
                Id = shopping.Id
            }
        );
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> PutShopping(long id, ShoppingUpdateRequest payload)
    {
        try
        {
            var shopping = await _service.Update(id, payload);

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

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteShopping(long id)
    {
        var shopping = await _service.Delete(id);

        if (shopping is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    private bool ShoppingExists(long id) => _service.Any(id);
}
