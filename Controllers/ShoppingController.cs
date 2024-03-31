using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Compra.Models;

namespace Compra.AddControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        public readonly AppDbContext _context;

        public ShoppingController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
            List<Shopping> list = new List<Shopping>();
            
            try
            {
                list = _context.Shoppings.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = list });
            }
            catch (Exception ex) {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = list });

            }
        }
    }
}
