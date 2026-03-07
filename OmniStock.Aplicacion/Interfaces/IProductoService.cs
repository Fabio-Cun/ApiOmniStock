using OmniStock.Aplicacion.DTO.ProductosDtos;

namespace OmniStock.Aplicacion.Interfaces
{
    public interface IProductoService
    {
        Task<List<ProductoDto>> ObtenerProductosAsync();

        Task<ProductoDto?> ObtenerPorIdAsync(int id);

        Task<List<ProductoDto>> ObtenerPorCategoriaAsync(int idCategoria);

        Task<ProductoDto> CrearProductoAsync(ProductoDto request);

        Task<bool> ActualizarProductoAsync(ProductoDto request);

        Task<bool> EliminarProductoAsync(int id);
    }
}
