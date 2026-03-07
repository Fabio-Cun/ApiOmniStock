using OmniStock.Aplicacion.DTO.ProductosDtos;
using OmniStock.Aplicacion.Interfaces;
using OmniStock.Infraestructura.Interfaces;
using OmniStock.Dominio;

namespace OmniStock.Aplicacion.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepositorio _productoRepositorio;

        public ProductoService(IProductoRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        public async Task<List<ProductoDto>> ObtenerProductosAsync()
        {
            var productos = await _productoRepositorio.ObtenerTodosAsync();

            return productos.Select(MapToDto).ToList();
        }

        public async Task<ProductoDto?> ObtenerPorIdAsync(int id)
        {
            var producto = await _productoRepositorio.ObtenerPorIdAsync(id);

            if (producto == null)
                return null;

            return MapToDto(producto);
        }

        public async Task<List<ProductoDto>> ObtenerPorCategoriaAsync(int idCategoria)
        {
            var productos = await _productoRepositorio.ObtenerPorCategoriaAsync(idCategoria);

            return productos.Select(MapToDto).ToList();
        }

        public async Task<ProductoDto> CrearProductoAsync(ProductoDto request)
        {
            var producto = await _productoRepositorio.CrearAsync(
                request.NombreProducto,
                request.Descripcion,
                request.Precio,
                request.IdCategoria
            );

            return MapToDto(producto);
        }

        public async Task<bool> ActualizarProductoAsync(ProductoDto request)
        {
            var existente = await _productoRepositorio.ObtenerPorIdAsync(request.IdProducto);

            if (existente == null)
                return false;

            existente.NombreProducto = request.NombreProducto;
            existente.Descripcion = request.Descripcion;
            existente.Precio = request.Precio;
            existente.IdCategoria = request.IdCategoria;

            await _productoRepositorio.ActualizarAsync(existente);

            return true;
        }

        public async Task<bool> EliminarProductoAsync(int id)
        {
            var existente = await _productoRepositorio.ObtenerPorIdAsync(id);

            if (existente == null)
                return false;

            await _productoRepositorio.EliminarAsync(id);

            return true;
        }

        private static ProductoDto MapToDto(ProductoDominio producto)
        {
            return new ProductoDto
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.NombreProducto,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                IdCategoria = producto.IdCategoria,
                NombreCategoria = producto.IdCategoriaNavigation?.NombreCategoria,
                CantidadStock = producto.Inventario?.Cantidad,
                CreadoEn = producto.CreadoEn
            };
        }
    }
}