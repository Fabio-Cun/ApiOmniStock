using Microsoft.EntityFrameworkCore;
using OmniStock.Dominio;
using OmniStock.Infraestructura.Datos;
using OmniStock.Infraestructura.Interfaces;
using OmniStock.Infraestructura.Modelos;

namespace OmniStock.Infraestructura.Repositorios
{
    internal class InventarioRepositorio: IInventarioRepositorio    
    {
        private readonly OmniStockDbContext _context;

        public InventarioRepositorio(OmniStockDbContext context)
        {
            _context = context;
        }

        public async Task<List<InventarioDominio>> ObtenerTodosAsync()
        {
            var inventarios = await _context.Inventarios
                .Include(i => i.IdProductoNavigation)
                .ToListAsync();

            return inventarios.Select(MapToDomain).ToList();
        }

        public async Task<InventarioDominio?> ObtenerPorIdAsync(int id)
        {
            var inventario = await _context.Inventarios
                .Include(i => i.IdProductoNavigation)
                .FirstOrDefaultAsync(i => i.IdInventario == id);

            if (inventario == null)
                return null;

            return MapToDomain(inventario);
        }

        public async Task<InventarioDominio?> ObtenerPorProductoAsync(int idProducto)
        {
            var inventario = await _context.Inventarios
                .Include(i => i.IdProductoNavigation)
                .FirstOrDefaultAsync(i => i.IdProducto == idProducto);

            if (inventario == null)
                return null;

            return MapToDomain(inventario);
        }

        public async Task<InventarioDominio> CrearAsync(int idProducto, int cantidad)
        {
            var inventario = new Inventario
            {
                IdProducto = idProducto,
                Cantidad = cantidad,
                FechaIngreso = DateTime.Now
            };

            _context.Inventarios.Add(inventario);
            await _context.SaveChangesAsync();

            return MapToDomain(inventario);
        }

        public async Task ActualizarCantidadAsync(int idProducto, int nuevaCantidad)
        {
            var inventario = await _context.Inventarios
                .FirstOrDefaultAsync(i => i.IdProducto == idProducto);

            if (inventario == null)
                return;

            inventario.Cantidad = nuevaCantidad;
            await _context.SaveChangesAsync();
        }

        public async Task AjustarCantidadAsync(int idProducto, int delta)
        {
            var inventario = await _context.Inventarios
                .FirstOrDefaultAsync(i => i.IdProducto == idProducto);

            if (inventario == null)
                return;

            inventario.Cantidad += delta;

            if (inventario.Cantidad < 0)
                throw new InvalidOperationException($"Stock insuficiente para el producto {idProducto}. Stock actual: {inventario.Cantidad - delta}.");

            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var inventario = await _context.Inventarios.FindAsync(id);

            if (inventario != null)
            {
                _context.Inventarios.Remove(inventario);
                await _context.SaveChangesAsync();
            }
        }

        private static InventarioDominio MapToDomain(Inventario inventario)
        {
            return new InventarioDominio
            {
                IdInventario = inventario.IdInventario,
                IdProducto = inventario.IdProducto,
                Cantidad = inventario.Cantidad,
                FechaIngreso = inventario.FechaIngreso
            };
        }
    }
}
