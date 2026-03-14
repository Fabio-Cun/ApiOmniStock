using Microsoft.AspNetCore.Mvc;
using OmniStock.Aplicacion.comun;
using OmniStock.Aplicacion.DTO.ReporteDtos;
using OmniStock.Aplicacion.DTO.VentasDtos;
using OmniStock.Aplicacion.Interfaces;
using OmniStock.Aplicacion.Servicios;
using OmniStock.Dominio;

namespace OmniStock.Api.Controllers.Ventas
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IVentasServicio _ventaServicio;

        public VentasController(IVentasServicio ventaServicio)
        {
            _ventaServicio = ventaServicio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var ventas = await _ventaServicio.ObtenerTodasAsync();

            return Ok(new ApiResponse<List<VentaDominio>>
            {
                Success = true,
                Message = "Listado de ventas obtenido correctamente",
                Data = ventas
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var venta = await _ventaServicio.ObtenerPorIdAsync(id);

                return Ok(new ApiResponse<VentaDominio>
                {
                    Success = true,
                    Message = "Venta encontrada",
                    Data = venta
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<VentaDominio>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("cliente/{idCliente}")]
        public async Task<IActionResult> ObtenerPorCliente(int idCliente)
        {
            var ventas = await _ventaServicio.ObtenerPorClienteAsync(idCliente);

            return Ok(new ApiResponse<List<VentaDominio>>
            {
                Success = true,
                Message = "Ventas del cliente obtenidas",
                Data = ventas
            });
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> ObtenerPorUsuario(int idUsuario)
        {
            var ventas = await _ventaServicio.ObtenerPorUsuarioAsync(idUsuario);

            return Ok(new ApiResponse<List<VentaDominio>>
            {
                Success = true,
                Message = "Ventas del usuario obtenidas",
                Data = ventas
            });
        }

        [HttpPost("procesar")]
        public async Task<IActionResult> ProcesarVenta(
            [FromQuery] int? idCliente,
            [FromQuery] int idUsuario,
            [FromBody] List<DetalleVentaDto> items)
        {
            try
            {
                var venta = await _ventaServicio.ProcesarVentaAsync(idCliente, idUsuario, items);

                return Ok(new ApiResponse<VentaDominio>
                {
                    Success = true,
                    Message = "Venta procesada correctamente",
                    Data = venta
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<VentaDominio>
                {
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelarVenta(int id)
        {
            try
            {
                await _ventaServicio.CancelarVentaAsync(id);

                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    Message = "Venta cancelada correctamente"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("reportes")]
        public async Task<IActionResult> CalcularTotalIngresos([FromQuery] ReporteVentasDto request)
        {
            var resumen = await _ventaServicio.ObtenerResumenVentasAsync(request.Desde, request.Hasta);

            return Ok(new ApiResponse<ResumenVentasDto>
            {
                Success = true,
                Message = $"Resumen de ventas desde {request.Desde:yyyy-MM-dd HH:mm:ss} hasta {request.Hasta:yyyy-MM-dd HH:mm:ss}",
                Data = resumen
            });
        }
    }
}
