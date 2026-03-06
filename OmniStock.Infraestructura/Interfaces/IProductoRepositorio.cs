using OmniStock.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Infraestructura.Interfaces
{
    public interface IProductoRepositorio
    {
        Task<List<ProductoDominio>> ObtenerTodosAsync();
        Task<ProductoDominio?> ObtenerPorIdAsync(int id);
        Task<List<ProductoDominio>> ObtenerPorCategoriaAsync(int idCategoria);
        Task<ProductoDominio> CrearAsync(string nombreProducto, string? descripcion, decimal precio, int? idCategoria);
        Task ActualizarAsync(ProductoDominio producto);
        Task EliminarAsync(int id);
    }
}
