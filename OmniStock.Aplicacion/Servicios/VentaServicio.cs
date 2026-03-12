using OmniStock.Aplicacion.DTO.VentasDtos;
using OmniStock.Aplicacion.Interfaces;
using OmniStock.Dominio;
using OmniStock.Infraestructura.Interfaces;
using OmniStock.Infraestructura.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.Servicios
{
    public record ItemVentaDto(int IdProducto, int Cantidad);
    public class VentaServicio : IVentasServicio
    {
        private readonly IVentaRepositorio _ventaRepositorio;
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly IInventarioRepositorio _inventarioRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;

        public VentaServicio(IVentaRepositorio ventaRepositorio,IProductoRepositorio productoRepositorio,
                             IInventarioRepositorio inventarioRepositorio,IClienteRepositorio clienteRepositorio)
        {
            _ventaRepositorio = ventaRepositorio;
            _productoRepositorio = productoRepositorio;
            _inventarioRepositorio = inventarioRepositorio;
            _clienteRepositorio = clienteRepositorio;
        }

        /// <summary>
        /// Obtiene todas las ventas con sus detalles.
        /// </summary>
        public async Task<List<VentaDominio>> ObtenerTodasAsync()
        {
            return await _ventaRepositorio.ObtenerTodasAsync();
        }

        /// <summary>
        /// Obtiene una venta por su ID. Lanza excepción si no existe.
        /// </summary>
        public async Task<VentaDominio> ObtenerPorIdAsync(int id)
        {
            var venta = await _ventaRepositorio.ObtenerPorIdAsync(id);

            if (venta == null)
                throw new KeyNotFoundException($"No se encontró la venta con ID {id}.");

            return venta;
        }

        /// <summary>
        /// Obtiene el historial de ventas de un cliente específico.
        /// </summary>
        public async Task<List<VentaDominio>> ObtenerPorClienteAsync(int idCliente)
        {
            var cliente = await _clienteRepositorio.ObtenerPorIdAsync(idCliente)
                ?? throw new KeyNotFoundException($"No se encontró el cliente con ID {idCliente}.");

            return await _ventaRepositorio.ObtenerPorClienteAsync(idCliente);
        }

        /// <summary>
        /// Obtiene las ventas realizadas por un usuario (cajero/vendedor).
        /// </summary>
        public async Task<List<VentaDominio>> ObtenerPorUsuarioAsync(int idUsuario)
        {
            return await _ventaRepositorio.ObtenerPorUsuarioAsync(idUsuario);
        }

        /// <summary>
        /// Obtiene las ventas dentro de un rango de fechas.
        /// </summary>
        public async Task<List<VentaDominio>> ObtenerPorRangoFechaAsync(DateTime desde, DateTime hasta)
        {
            if (desde > hasta)
                throw new ArgumentException("La fecha de inicio no puede ser mayor a la fecha de fin.");

            return await _ventaRepositorio.ObtenerPorRangoFechaAsync(desde, hasta);
        }

        /// <summary>
        /// Procesa una nueva venta: valida stock, calcula totales y descuenta inventario.
        /// </summary>
        public async Task<VentaDominio> ProcesarVentaAsync(int? idCliente, int idUsuario, List<ItemVentaDto> items)
        {
            if (items == null || items.Count == 0)
                throw new ArgumentException("La venta debe contener al menos un producto.");

            if (idCliente.HasValue)
            {
                var clienteExiste = await _clienteRepositorio.ObtenerPorIdAsync(idCliente.Value);
                if (clienteExiste == null)
                    throw new KeyNotFoundException($"No se encontró el cliente con ID {idCliente}.");
            }

            var detalles = new List<DetalleVentaDominio>();

            foreach (var item in items)
            {
                if (item.Cantidad <= 0)
                    throw new ArgumentException($"La cantidad del producto ID {item.IdProducto} debe ser mayor a cero.");

                var producto = await _productoRepositorio.ObtenerPorIdAsync(item.IdProducto)
                    ?? throw new KeyNotFoundException($"No se encontró el producto con ID {item.IdProducto}.");

                var inventario = await _inventarioRepositorio.ObtenerPorProductoAsync(item.IdProducto);

                if (inventario == null || inventario.Cantidad < item.Cantidad)
                    throw new InvalidOperationException(
                        $"Stock insuficiente para '{producto.NombreProducto}'. " +
                        $"Disponible: {inventario?.Cantidad ?? 0}, solicitado: {item.Cantidad}.");

                detalles.Add(new DetalleVentaDominio
                {
                    IdProducto = item.IdProducto,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = producto.Precio,
                    Subtotal = producto.Precio * item.Cantidad
                });
            }

            // Descontar inventario por cada ítem confirmado
            foreach (var item in items)
            {
                await _inventarioRepositorio.AjustarCantidadAsync(item.IdProducto, -item.Cantidad);
            }

            return await _ventaRepositorio.CrearAsync(idCliente, idUsuario, detalles);
        }

        /// <summary>
        /// Cancela una venta y restituye el stock de los productos involucrados.
        /// </summary>
        public async Task CancelarVentaAsync(int idVenta)
        {
            var venta = await _ventaRepositorio.ObtenerPorIdAsync(idVenta)
                ?? throw new KeyNotFoundException($"No se encontró la venta con ID {idVenta}.");

            // Restituir inventario
            foreach (var detalle in venta.Detalles)
            {
                await _inventarioRepositorio.AjustarCantidadAsync(detalle.IdProducto, detalle.Cantidad);
            }

            await _ventaRepositorio.EliminarAsync(idVenta);
        }

        /// <summary>
        /// Calcula el total de ventas en un rango de fechas (reporte de ingresos).
        /// </summary>

        public async Task<ResumenVentasDto> ObtenerResumenVentasAsync(DateTime desde, DateTime hasta)
        {
            var ventas = await ObtenerPorRangoFechaAsync(desde, hasta);

            var total = ventas.Sum(v => v.Total);
            var cantidad = ventas.Count;
            var promedio = cantidad > 0 ? total / cantidad : 0;

            return new ResumenVentasDto
            {
                Desde = desde,
                Hasta = hasta,
                CantidadVentas = cantidad,
                TotalIngresos = total,
                PromedioVenta = promedio
            };
        }
    }
}
