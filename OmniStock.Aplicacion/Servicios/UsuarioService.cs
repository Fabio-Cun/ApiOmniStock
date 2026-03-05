using OmniStock.Aplicacion.DTO;
using OmniStock.Aplicacion.Interfaces;
using OmniStock.Infraestructura.Interfaces;

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

        public async Task<UsuarioDto?> RegistrarAsync(string nombreUsuario, string contrasena, int idRol)
        {
            var usuario = await _usuarioRepositorio.RegistrarAsync(nombreUsuario, contrasena, idRol);

            if (usuario == null) return null;

            return new UsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                IdRol = usuario.IdRol
            };
        }

        public async Task<UsuarioDto?> LoginAsync(string nombreUsuario, string password)
        {
            var usuario = await _usuarioRepositorio.LoginAsync(nombreUsuario, password);

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
