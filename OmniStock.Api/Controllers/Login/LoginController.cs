using Microsoft.AspNetCore.Mvc;
using OmniStock.Aplicacion.comun;
using OmniStock.Aplicacion.DTO;
using OmniStock.Aplicacion.DTO.UsuariosDtos;
using OmniStock.Aplicacion.Interfaces;

namespace OmniStock.Api.Controllers.Login
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioDto request)
        {
            var usuario = await _usuarioService.RegistrarAsync(request);

            if (usuario == null)
            {
                return BadRequest(new ApiResponse<UsuarioDto>
                {
                    Success = false,
                    Message = "El usuario ya existe"
                });
            }

            return Ok(new ApiResponse<UsuarioDto>
            {
                Success = true,
                Message = "Usuario registrado correctamente",
                Data = usuario
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var usuario = await _usuarioService.LoginAsync(request);

            if (usuario == null)
            {
                return Unauthorized(new ApiResponse<UsuarioDto>
                {
                    Success = false,
                    Message = "Credenciales incorrectas"
                });
            }

            return Ok(new ApiResponse<UsuarioDto>
            {
                Success = true,
                Message = "Login exitoso",
                Data = usuario
            });
        }
    }
}
