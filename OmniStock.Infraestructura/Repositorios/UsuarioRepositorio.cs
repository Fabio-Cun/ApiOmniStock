using Microsoft.EntityFrameworkCore;
using OmniStock.Dominio;
using OmniStock.Infraestructura.Datos;
using OmniStock.Infraestructura.Interfaces;
using OmniStock.Infraestructura.Modelos;

namespace OmniStock.Infraestructura.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly OmniStockDbContext _context;

        public UsuarioRepositorio(OmniStockDbContext context)
        {
            _context = context;
        }

        public async Task<List<UsuarioDominio>> ObtenerTodosUsuariosAsync()
        {
            var usuarios = await _context.Usuarios.ToListAsync();

            return usuarios.Select(MapToDomain).ToList();
        }

        public async Task<UsuarioDominio?> ObtenerPorIdAsync(int id)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
                return null;

            return MapToDomain(usuario);
        }

        public async Task<UsuarioDominio?> RegistrarAsync(string nombreUsuario,string nombreCompleto, string contrasena, int idRol)
        {
            var existe = await _context.Usuarios
                .AnyAsync(u => u.NombreUsuario == nombreUsuario);

            if (existe)
                return null;

            var usuario = new Usuario
            {
                NombreUsuario = nombreUsuario,
                NombreCompleto = nombreCompleto,
                Contrasena = contrasena,
                IdRol = idRol,
                FechaCreacion = DateTime.Now
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return MapToDomain(usuario);
        }

        public async Task<UsuarioDominio?> LoginAsync(string nombreUsuario, string password)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

            if (usuario == null || usuario.Contrasena != password)
                return null;

            return MapToDomain(usuario);
        }

        public async Task EliminarUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ActualizarUsuarioAsync(UsuarioDominio usuarioDominio)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioDominio.IdUsuario);

            if (usuario == null)
                return;

            usuario.NombreUsuario = usuarioDominio.NombreUsuario;
            usuario.NombreCompleto = usuarioDominio.NombreCompleto;
            usuario.IdRol = usuarioDominio.IdRol;

            await _context.SaveChangesAsync();
        }

        private static UsuarioDominio MapToDomain(Usuario usuario)
        {
            return new UsuarioDominio
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                NombreCompleto = usuario.NombreCompleto,
                IdRol = usuario.IdRol,
                FechaCreacion = usuario.FechaCreacion
            };
        }
    }
}