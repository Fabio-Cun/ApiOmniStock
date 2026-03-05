using Microsoft.AspNetCore.Mvc;
using OmniStock.Aplicacion.Interfaces;

namespace OmniStock.Api.Controllers.Login
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

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var usuarios = await _usuarioService.ObtenerUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorIdAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(string nombreUsuario, string contrasena, int idRol)
        {
            var usuario = await _usuarioService.RegistrarAsync(nombreUsuario, contrasena, idRol);

            if (usuario == null)
                return BadRequest("El usuario ya existe");

            return Ok(usuario);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string nombreUsuario, string password)
        {
            var usuario = await _usuarioService.LoginAsync(nombreUsuario, password);

            if (usuario == null)
                return Unauthorized();

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _usuarioService.EliminarUsuarioAsync(id);

            if (!eliminado)
                return NotFound();

            return Ok("Usuario eliminado");
        }
    }
}
