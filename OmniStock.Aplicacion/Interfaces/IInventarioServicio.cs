using OmniStock.Aplicacion.DTO.InventarioDtos;
using OmniStock.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.Interfaces
{
    public interface IInventarioServicio
    {
        Task<List<MovimientoInventarioDto>> ObtenerTodosAsync();

        Task<MovimientoInventarioDto> ObtenerPorProductoAsync(int idProducto);

        Task<MovimientoInventarioDto> RegistrarStockInicialAsync(int idProducto, int cantidad);

        Task EntradaStockAsync(int idProducto, int cantidad);

        Task SalidaStockAsync(int idProducto, int cantidad);

        Task AjustarStockAsync(int idProducto, int nuevaCantidad);

        Task<List<InventarioDominio>> ObtenerProductosBajoStockAsync(int umbral = 5);

    }
}
