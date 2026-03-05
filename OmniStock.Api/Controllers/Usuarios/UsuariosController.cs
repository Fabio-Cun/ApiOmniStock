using Microsoft.AspNetCore.Mvc;
using OmniStock.Aplicacion.DTO.UsuariosDtos;
using OmniStock.Aplicacion.Interfaces;

namespace OmniStock.Api.Controllers.Usuarios
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("ListarUsuarios")]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var usuarios = await _usuarioService.ObtenerUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpGet("ObtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorIdAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpDelete("BorrarUsuario/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _usuarioService.EliminarUsuarioAsync(id);

            if (!eliminado)
                return NotFound();

            return Ok("Usuario eliminado");
        }


        
        [HttpPut("ActualizarUsuario")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarUsuarioDto actualizar)
        {
            await _usuarioService.ActualizarUsuarioAsync(actualizar);
            return Ok("Usuario actualizado");   

        }
    }
}
