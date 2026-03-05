using OmniStock.Aplicacion.DTO;
using OmniStock.Aplicacion.DTO.UsuariosDtos;
using OmniStock.Aplicacion.Interfaces;
using OmniStock.Dominio;
using OmniStock.Infraestructura.Interfaces;
using OmniStock.Infraestructura.Modelos;

namespace OmniStock.Aplicacion.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioService(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<List<UsuarioDto>> ObtenerUsuariosAsync()
        {
            var usuarios = await _usuarioRepositorio.ObtenerTodosUsuariosAsync();

            return usuarios.Select(u => new UsuarioDto
            {
                IdUsuario = u.IdUsuario,
                NombreUsuario = u.NombreUsuario,
                NombreCompleto = u.NombreCompleto,
                IdRol = u.IdRol
            }).ToList();
        }

        public async Task<UsuarioDto?> ObtenerUsuarioPorIdAsync(int id)
        {
            var usuario = await _usuarioRepositorio.ObtenerPorIdAsync(id);

            if (usuario == null) return null;

            return new UsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                NombreCompleto = usuario.NombreCompleto,
                IdRol = usuario.IdRol
            };
        }

        public async Task<UsuarioDto?> RegistrarAsync(RegistrarUsuarioDto registrarDto)
        {
            var usuario = await _usuarioRepositorio.RegistrarAsync(registrarDto.NombreUsuario,
                                                                   registrarDto.NombreCompleto,
                                                                   registrarDto.Contrasena,
                                                                   registrarDto.IdRol);

            if (usuario == null) return null;

            return new UsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                NombreCompleto = usuario.NombreCompleto,
                IdRol = usuario.IdRol
            };
        }

        public async Task<ActualizarUsuarioDto?> ActualizarUsuarioAsync(ActualizarUsuarioDto actualizarDto)
        {
            var usuarios = await _usuarioRepositorio.ObtenerTodosUsuariosAsync();
            if (usuarios == null)
            {
                return null;
            }

            var encontrarUsuario = usuarios.FirstOrDefault(u => u.NombreUsuario == actualizarDto.NombreUsuario);
            if (encontrarUsuario != null)
            {
                var usuarioDominio = new UsuarioDominio
                {
                    IdUsuario = encontrarUsuario.IdUsuario,
                    NombreUsuario = actualizarDto.NombreUsuario,
                    NombreCompleto = actualizarDto.NombreCompleto,
                    IdRol = actualizarDto.IdRol
                };

                await _usuarioRepositorio.ActualizarUsuarioAsync(usuarioDominio);

                return actualizarDto;
            }

            return null;
        }

        public async Task<UsuarioDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var usuario = await _usuarioRepositorio.LoginAsync(loginRequestDto.NombreUsuario,
                                                               loginRequestDto.Password);

            if (usuario == null) return null;

            return new UsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                IdRol = usuario.IdRol
            };
        }

        public async Task<bool> EliminarUsuarioAsync(int id)
        {
            var usuario = await _usuarioRepositorio.ObtenerPorIdAsync(id);

            if (usuario == null)
                return false;

            await _usuarioRepositorio.EliminarUsuarioAsync(id);
            return true;
        }
    }
}
