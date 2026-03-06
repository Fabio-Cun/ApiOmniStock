using OmniStock.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Infraestructura.Interfaces
{
    public interface IInventarioRepositorio
    {
        Task<List<InventarioDominio>> ObtenerTodosAsync();
        Task<InventarioDominio?> ObtenerPorIdAsync(int id);
        Task<InventarioDominio?> ObtenerPorProductoAsync(int idProducto);
        Task<InventarioDominio> CrearAsync(int idProducto, int cantidad);
        Task ActualizarCantidadAsync(int idProducto, int nuevaCantidad);
        Task AjustarCantidadAsync(int idProducto, int delta);
        Task EliminarAsync(int id);
    }
}
