using Microsoft.AspNetCore.Mvc;
using OmniStock.Aplicacion.comun;
using OmniStock.Aplicacion.DTO.RolesDtos;
using OmniStock.Aplicacion.Interfaces;

namespace OmniStock.Api.Controllers.Roles
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly IRolservice _rolService;

        public RolController(IRolservice rolService)
        {
            _rolService = rolService;
        }

        // GET: api/roles
        [HttpGet("ListarRoles")]
        public async Task<IActionResult> ObtenerRoles()
        {
            var roles = await _rolService.ObtenerRolesAsync();
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Roles obtenidos correctamente",
                Data = roles
            });
        }

        // GET: api/roles/{id}
        [HttpGet("ObtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var rol = await _rolService.ObtenerPorIdAsync(id);

            if (rol == null)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Rol no encontrado"
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Rol obtenido correctamente",
                Data = rol
            });
        }

        // POST: api/roles
        [HttpPost("crearRol")]
        public async Task<IActionResult> Crear([FromBody] RolDto request)
        {
            var rolCreado = await _rolService.CrearRolAsync(request);

            return Ok(new ApiResponse<RolDto>
            {
                Success = true,
                Message = "Rol creado correctamente",
                Data = rolCreado
            });
        }

        // PUT: api/roles
        [HttpPut("actualizarRol")]
        public async Task<IActionResult> Actualizar([FromBody] RolDto request)
        {
            var actualizado = await _rolService.ActualizarRolAsync(request);

            if (!actualizado)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Rol no encontrado"
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Rol actualizado correctamente"
            });
        }

        // DELETE: api/roles/{id}
        [HttpDelete("borrarRol/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _rolService.EliminarRolAsync(id);

            if (!eliminado)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Rol no encontrado"
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Rol eliminado correctamente"
            });
        }

    }
}
