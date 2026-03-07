using OmniStock.Aplicacion.DTO.InventarioDtos;
using OmniStock.Dominio;
using OmniStock.Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.Servicios
{
    public class InventarioServicio
    {
        private readonly IInventarioRepositorio _inventarioRepositorio;
        private readonly IProductoRepositorio _productoRepositorio;

        public InventarioServicio(IInventarioRepositorio inventarioRepositorio, IProductoRepositorio productoRepositorio)
        {
            _inventarioRepositorio = inventarioRepositorio;
            _productoRepositorio = productoRepositorio;
        }

        /// <summary>
        /// Obtiene todos los registros de inventario.
        /// </summary>
        public async Task<List<MovimientoInventarioDto>> ObtenerTodosAsync()
        {
            var resultado = new List<MovimientoInventarioDto>();
            var datos =  await _inventarioRepositorio.ObtenerTodosAsync();
            if (datos.Count > 0)
            {
                foreach (var item in datos)
                {
                    var producto = await _productoRepositorio.ObtenerPorIdAsync(item.IdProducto);
                    resultado.Add(new MovimientoInventarioDto
                    {
                        IdInventario = item.IdInventario,
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        FechaIngreso = item.FechaIngreso,

                    });
                }

            }
            else
            {
               return (List<MovimientoInventarioDto>)resultado.DefaultIfEmpty();
            }
                

            return resultado;   
        }

        /// <summary>
        /// Obtiene el inventario de un producto específico.
        /// </summary>
        public async Task<MovimientoInventarioDto> ObtenerPorProductoAsync(int idProducto)
        {
            var inventario = await _inventarioRepositorio.ObtenerPorProductoAsync(idProducto);

            if (inventario == null)
                throw new KeyNotFoundException($"No se encontró inventario para el producto con ID {idProducto}.");


           var resultado= new MovimientoInventarioDto
            {
                IdInventario = inventario.IdInventario,
                IdProducto = inventario.IdProducto,
                Cantidad = inventario.Cantidad,
                FechaIngreso = inventario.FechaIngreso,
                
            };  

            return resultado;
        }

        /// <summary>
        /// Registra stock inicial para un producto. No permite duplicados.
        /// </summary>
        public async Task<MovimientoInventarioDto> RegistrarStockInicialAsync(int idProducto, int cantidad)
        {
            if (cantidad < 0)
                throw new ArgumentException("La cantidad de stock no puede ser negativa.");

            var producto = await _productoRepositorio.ObtenerPorIdAsync(idProducto)
                ?? throw new KeyNotFoundException($"No se encontró el producto con ID {idProducto}.");

            var existente = await _inventarioRepositorio.ObtenerPorProductoAsync(idProducto);
            if (existente != null)
                throw new InvalidOperationException($"El producto '{producto.NombreProducto}' ya tiene un registro de inventario.");

            var datos = await _inventarioRepositorio.CrearAsync(idProducto, cantidad);

            var resultado = new MovimientoInventarioDto
            {
                IdInventario = datos.IdInventario,
                IdProducto = datos.IdProducto,
                Cantidad = datos.Cantidad,
                FechaIngreso = datos.FechaIngreso
            };
            return resultado;
        }

        /// <summary>
        /// Añade unidades al stock de un producto (entrada de mercancía).
        /// </summary>
        public async Task EntradaStockAsync(int idProducto, int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad de entrada debe ser mayor a cero.");

            var producto = await _productoRepositorio.ObtenerPorIdAsync(idProducto)
                ?? throw new KeyNotFoundException($"No se encontró el producto con ID {idProducto}.");

            var inventario = await _inventarioRepositorio.ObtenerPorProductoAsync(idProducto);

            if (inventario == null)
                await _inventarioRepositorio.CrearAsync(idProducto, cantidad);
            else
                await _inventarioRepositorio.AjustarCantidadAsync(idProducto, cantidad);
        }

        /// <summary>
        /// Descuenta unidades del stock (salida de mercancía). Valida stock suficiente.
        /// </summary>
        public async Task SalidaStockAsync(int idProducto, int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad de salida debe ser mayor a cero.");

            var inventario = await _inventarioRepositorio.ObtenerPorProductoAsync(idProducto)
                ?? throw new KeyNotFoundException($"No se encontró inventario para el producto con ID {idProducto}.");

            if (inventario.Cantidad < cantidad)
                throw new InvalidOperationException($"Stock insuficiente. Disponible: {inventario.Cantidad}, solicitado: {cantidad}.");

            await _inventarioRepositorio.AjustarCantidadAsync(idProducto, -cantidad);
        }

        /// <summary>
        /// Ajusta el stock de un producto a un valor absoluto (corrección de inventario).
        /// </summary>
        public async Task AjustarStockAsync(int idProducto, int nuevaCantidad)
        {
            if (nuevaCantidad < 0)
                throw new ArgumentException("La cantidad ajustada no puede ser negativa.");

            var producto = await _productoRepositorio.ObtenerPorIdAsync(idProducto)
                ?? throw new KeyNotFoundException($"No se encontró el producto con ID {idProducto}.");

            await _inventarioRepositorio.ActualizarCantidadAsync(idProducto, nuevaCantidad);
        }

        /// <summary>
        /// Obtiene los productos con stock por debajo del umbral indicado (stock mínimo).
        /// </summary>
        public async Task<List<InventarioDominio>> ObtenerProductosBajoStockAsync(int umbral = 5)
        {
            var todos = await _inventarioRepositorio.ObtenerTodosAsync();
            return todos.Where(i => i.Cantidad <= umbral).ToList();
        }
    }

}
