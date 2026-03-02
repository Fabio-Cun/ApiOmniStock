using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiOmniStock.Data;
using ApiOmniStock.Models;

namespace ApiOmniStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleVentasController : ControllerBase
    {
        private readonly ApiOmniStockContext _context;

        public DetalleVentasController(ApiOmniStockContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleVenta>>> GetVentaDetalles()
        {
            return await _context.VentaDetalles.Include(d => d.Producto)
                                               .Include(d => d.Venta)
                                               .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<DetalleVenta>> PostVentaDetalle(DetalleVenta detalle)
        {
            _context.VentaDetalles.Add(detalle);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVentaDetalles), new { id = detalle.IdDetalle }, detalle);
        }
    }
}
