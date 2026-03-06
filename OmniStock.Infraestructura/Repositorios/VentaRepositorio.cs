using Microsoft.EntityFrameworkCore;
using OmniStock.Dominio;
using OmniStock.Infraestructura.Datos;
using OmniStock.Infraestructura.Interfaces;
using OmniStock.Infraestructura.Modelos;

namespace OmniStock.Infraestructura.Repositorios
{
    public class VentaRepositorio:IVentaRepositorio
    {
        private readonly OmniStockDbContext _context;

        public VentaRepositorio(OmniStockDbContext context)
        {
            _context = context;
        }

        public async Task<List<VentaDominio>> ObtenerTodasAsync()
        {
            var ventas = await _context.Ventas
                .Include(v => v.DetalleVenta)
                    .ThenInclude(d => d.IdProductoNavigation)
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .ToListAsync();

            return ventas.Select(MapToDomain).ToList();
        }

        public async Task<VentaDominio?> ObtenerPorIdAsync(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.DetalleVenta)
                    .ThenInclude(d => d.IdProductoNavigation)
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(v => v.IdVenta == id);

            if (venta == null)
                return null;

            return MapToDomain(venta);
        }

        public async Task<List<VentaDominio>> ObtenerPorClienteAsync(int idCliente)
        {
            var ventas = await _context.Ventas
                .Include(v => v.DetalleVenta)
                    .ThenInclude(d => d.IdProductoNavigation)
                .Where(v => v.IdCliente == idCliente)
                .ToListAsync();

            return ventas.Select(MapToDomain).ToList();
        }

        public async Task<List<VentaDominio>> ObtenerPorUsuarioAsync(int idUsuario)
        {
            var ventas = await _context.Ventas
                .Include(v => v.DetalleVenta)
                    .ThenInclude(d => d.IdProductoNavigation)
                .Where(v => v.IdUsuario == idUsuario)
                .ToListAsync();

            return ventas.Select(MapToDomain).ToList();
        }

        public async Task<List<VentaDominio>> ObtenerPorRangoFechaAsync(DateTime desde, DateTime hasta)
        {
            var ventas = await _context.Ventas
                .Include(v => v.DetalleVenta)
                    .ThenInclude(d => d.IdProductoNavigation)
                .Where(v => v.FechaVenta >= desde && v.FechaVenta <= hasta)
                .ToListAsync();

            return ventas.Select(MapToDomain).ToList();
        }

        public async Task<VentaDominio> CrearAsync(int? idCliente, int idUsuario, List<DetalleVentaDominio> detalles)
        {
            var total = detalles.Sum(d => d.Subtotal);

            var venta = new Venta
            {
                IdCliente = idCliente,
                IdUsuario = idUsuario,
                FechaVenta = DateTime.Now,
                Total = total,
                DetalleVenta = detalles.Select(d => new Modelos.DetalleVenta
                {
                    IdProducto = d.IdProducto,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            return MapToDomain(venta);
        }

        public async Task EliminarAsync(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.DetalleVenta)
                .FirstOrDefaultAsync(v => v.IdVenta == id);

            if (venta != null)
            {
                _context.Ventas.Remove(venta);
                await _context.SaveChangesAsync();
            }
        }

        private static VentaDominio MapToDomain(Venta venta)
        {
            return new VentaDominio
            {
                IdVenta = venta.IdVenta,
                IdCliente = venta.IdCliente,
                IdUsuario = venta.IdUsuario,
                FechaVenta = venta.FechaVenta,
                Total = venta.Total,
                Detalles = venta.DetalleVenta.Select(d => new  DetalleVenta
                {
                    IdDetalle = d.IdDetalle,
                    IdVenta = d.IdVenta,
                    IdProducto = d.IdProducto,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                }).ToList()
            };
        }

    }
}

