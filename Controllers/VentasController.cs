using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiOmniStock.Data;
using ApiOmniStock.Models;

namespace ApiOmniStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly ApiOmniStockContext _context;

        public VentasController(ApiOmniStockContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
        {
            return await _context.Ventas.Include(v => v.Cliente)
                                        .Include(v => v.Detalles)
                                        .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> GetVenta(int id)
        {
            var venta = await _context.Ventas.Include(v => v.Cliente)
                                             .Include(v => v.Detalles)
                                             .FirstOrDefaultAsync(v => v.IdVenta == id);
            if (venta == null) return NotFound();
            return venta;
        }

        [HttpPost]
        public async Task<ActionResult<Venta>> PostVenta(Venta venta)
        {
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVenta), new { id = venta.IdVenta }, venta);
        }
    }
}
