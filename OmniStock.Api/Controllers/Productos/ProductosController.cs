using Microsoft.AspNetCore.Mvc;
using OmniStock.Aplicacion.DTO.ProductosDtos;
using OmniStock.Aplicacion.Interfaces;
using OmniStock.Aplicacion.comun;

namespace OmniStock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet("ListarProductos")]
        public async Task<IActionResult> ObtenerProductos()
        {
            var productos = await _productoService.ObtenerProductosAsync();

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Productos obtenidos correctamente",
                Data = productos
            });
        }

        [HttpGet("ObtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var producto = await _productoService.ObtenerPorIdAsync(id);

            if (producto == null)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Producto no encontrado"
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Producto obtenido correctamente",
                Data = producto
            });
        }

        [HttpGet("ObtenerPorCategoria/{idCategoria}")]
        public async Task<IActionResult> ObtenerPorCategoria(int idCategoria)
        {
            var productos = await _productoService.ObtenerPorCategoriaAsync(idCategoria);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Productos obtenidos correctamente",
                Data = productos
            });
        }

        [HttpPost("CrearProducto")]
        public async Task<IActionResult> Crear([FromBody] ProductoDto request)
        {
            var producto = await _productoService.CrearProductoAsync(request);

            return Ok(new ApiResponse<ProductoDto>
            {
                Success = true,
                Message = "Producto creado correctamente",
                Data = producto
            });
        }

        [HttpPut("ActualizarProducto")]
        public async Task<IActionResult> Actualizar([FromBody] ProductoDto request)
        {
            var actualizado = await _productoService.ActualizarProductoAsync(request);

            if (!actualizado)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Producto no encontrado"
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Producto actualizado correctamente"
            });
        }

        [HttpDelete("EliminarProducto/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _productoService.EliminarProductoAsync(id);

            if (!eliminado)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Producto no encontrado"
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Producto eliminado correctamente"
            });
        }
    }
}