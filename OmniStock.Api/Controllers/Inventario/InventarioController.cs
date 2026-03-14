using Microsoft.AspNetCore.Mvc;
using OmniStock.Aplicacion.comun;
using OmniStock.Aplicacion.DTO.InventarioDtos;
using OmniStock.Aplicacion.Interfaces;

namespace OmniStock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioServicio _inventarioServicio;

        public InventarioController(IInventarioServicio inventarioServicio)
        {
            _inventarioServicio = inventarioServicio;
        }

        // Obtener todo el inventario
        [HttpGet("ListarInventario")]
        public async Task<IActionResult> ObtenerInventario()
        {
            var inventario = await _inventarioServicio.ObtenerTodosAsync();

            return Ok(new ApiResponse<List<InventarioDto>>
            {
                Success = true,
                Message = "Inventario obtenido correctamente",
                Data = inventario
            }); 
        }

        // Obtener inventario por producto
        [HttpGet("ObtenerPorProducto/{idProducto}")]
        public async Task<IActionResult> ObtenerPorProducto(int idProducto)
        {
            try
            {
                var inventario = await _inventarioServicio.ObtenerPorProductoAsync(idProducto);

                return Ok(new ApiResponse<MovimientoInventarioDto>
                {
                    Success = true,
                    Message = "Inventario obtenido correctamente",
                    Data = inventario
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<MovimientoInventarioDto>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        // Registrar stock inicial
        [HttpPost("RegistrarStockInicial")]
        public async Task<IActionResult> RegistrarStockInicial([FromBody] MovimientoInventarioDto request)
        {
            try
            {
                var resultado = await _inventarioServicio
                    .RegistrarStockInicialAsync(request.IdProducto, request.Cantidad);

                return Ok(new ApiResponse<MovimientoInventarioDto>
                {
                    Success = true,
                    Message = "Stock inicial registrado correctamente",
                    Data = resultado
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<MovimientoInventarioDto>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        // Entrada de stock
        [HttpPost("EntradaStock")]
        public async Task<IActionResult> EntradaStock([FromBody] MovimientoInventarioDto request)
        {
            try
            {
                await _inventarioServicio
                    .EntradaStockAsync(request.IdProducto, request.Cantidad);

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Entrada de stock registrada correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        // Salida de stock
        [HttpPost("SalidaStock")]
        public async Task<IActionResult> SalidaStock([FromBody] MovimientoInventarioDto request)
        {
            try
            {
                await _inventarioServicio
                    .SalidaStockAsync(request.IdProducto, request.Cantidad);

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Salida de stock registrada correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        // Ajustar stock manualmente
        [HttpPut("AjustarStock")]
        public async Task<IActionResult> AjustarStock([FromBody] MovimientoInventarioDto request)
        {
            try
            {
                await _inventarioServicio
                    .AjustarStockAsync(request.IdProducto, request.Cantidad);

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Stock ajustado correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        // Productos con bajo stock
        [HttpGet("ProductosBajoStock")]
        public async Task<IActionResult> ProductosBajoStock(int umbral = 5)
        {
            var productos = await _inventarioServicio
                .ObtenerProductosBajoStockAsync(umbral);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Productos con bajo stock obtenidos correctamente",
                Data = productos
            });
        }
    }
}