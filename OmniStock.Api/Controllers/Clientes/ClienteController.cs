using Microsoft.AspNetCore.Mvc;
using OmniStock.Aplicacion.DTO.ClientesDto;
using OmniStock.Aplicacion.Interfaces;
using OmniStock.Aplicacion.comun;

namespace OmniStock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IclienteServicio _clienteServicio;

        public ClienteController(IclienteServicio clienteServicio)
        {
            _clienteServicio = clienteServicio;
        }

        [HttpGet("ListarClientes")]
        public async Task<IActionResult> ObtenerClientes()
        {
            var clientes = await _clienteServicio.ObtenerTodosAsync();

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Clientes obtenidos correctamente",
                Data = clientes
            });
        }

        [HttpGet("ObtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var cliente = await _clienteServicio.ObtenerPorIdAsync(id);

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Cliente obtenido correctamente",
                    Data = cliente
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("CrearCliente")]
        public async Task<IActionResult> Crear([FromBody] ClienteDto request)
        {
            try
            {
                var cliente = await _clienteServicio.CrearAsync(
                    request.Cedula,
                    request.Nombre,
                    request.Apellido,
                    request.Email,
                    request.Telefono
                );

                return Ok(new ApiResponse<ClienteDto>
                {
                    Success = true,
                    Message = "Cliente creado correctamente",
                    Data = cliente
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPut("ActualizarCliente/{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ClienteDto request)
        {
            try
            {
                await _clienteServicio.ActualizarAsync(
                    id,
                    request.Cedula,
                    request.Nombre,
                    request.Apellido,
                    request.Email,
                    request.Telefono
                );

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Cliente actualizado correctamente"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("EliminarCliente/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _clienteServicio.EliminarAsync(id);

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Cliente eliminado correctamente"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
