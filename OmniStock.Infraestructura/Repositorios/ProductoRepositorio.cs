using Microsoft.EntityFrameworkCore;
using OmniStock.Dominio;
using OmniStock.Infraestructura.Datos;
using OmniStock.Infraestructura.Interfaces;
using OmniStock.Infraestructura.Modelos;

namespace OmniStock.Infraestructura.Repositorios
{
    public class ProductoRepositorio:IProductoRepositorio
    {
        private readonly OmniStockDbContext _context;

        public ProductoRepositorio(OmniStockDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductoDominio>> ObtenerTodosAsync()
        {
            var productos = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.Inventario)
                .ToListAsync();

            return productos.Select(MapToDomain).ToList();
        }

        public async Task<ProductoDominio?> ObtenerPorIdAsync(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.Inventario)
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            if (producto == null)
                return null;

            return MapToDomain(producto);
        }

        public async Task<List<ProductoDominio>> ObtenerPorCategoriaAsync(int idCategoria)
        {
            var productos = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.Inventario)
                .Where(p => p.IdCategoria == idCategoria)
                .ToListAsync();

            return productos.Select(MapToDomain).ToList();
        }

        public async Task<ProductoDominio> CrearAsync(string nombreProducto, string? descripcion, decimal precio, int? idCategoria)
        {
            var producto = new Producto
            {
                NombreProducto = nombreProducto,
                Descripcion = descripcion,
                Precio = precio,
                IdCategoria = idCategoria,
                CreadoEn = DateTime.Now
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return MapToDomain(producto);
        }

        public async Task ActualizarAsync(ProductoDominio productoDominio)
        {
            var producto = await _context.Productos.FindAsync(productoDominio.IdProducto);

            if (producto == null)
                return;

            producto.NombreProducto = productoDominio.NombreProducto;
            producto.Descripcion = productoDominio.Descripcion;
            producto.Precio = productoDominio.Precio;
            producto.IdCategoria = productoDominio.IdCategoria;

            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }

        private static ProductoDominio MapToDomain(Producto producto)
        {
            return new ProductoDominio
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.NombreProducto,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                IdCategoria = producto.IdCategoria,
                CreadoEn = producto.CreadoEn,
                IdCategoriaNavigation = producto.IdCategoriaNavigation == null ? null : new CategoriaDominio
                {
                    IdCategoria = producto.IdCategoriaNavigation.IdCategoria,
                    NombreCategoria = producto.IdCategoriaNavigation.NombreCategoria
                },
                Inventario = producto.Inventario == null ? null : new InventarioDominio
                {
                    IdInventario = producto.Inventario.IdInventario,
                    IdProducto = producto.Inventario.IdProducto,
                    Cantidad = producto.Inventario.Cantidad,
                    FechaIngreso = producto.Inventario.FechaIngreso
                }
            };
        }
    }
}
