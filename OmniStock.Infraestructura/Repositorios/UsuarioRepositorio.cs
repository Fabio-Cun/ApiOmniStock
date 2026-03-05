using Microsoft.EntityFrameworkCore;
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

        // Obtener todos
        public async Task<List<Usuario>> ObtenerTodosUsuariosAsync()
        {
            return await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .ToListAsync();
        }

        // Obtener por Id
        public async Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        // Obtener por nombre usuario (para login)
        public async Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
        }

        // Actualizar
        public async Task ActualizarUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        // Eliminar
        public async Task EliminarUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Usuario?> RegistrarAsync(string nombreUsuario, string contrasena, int idRol)
        {
            // Verificar si ya existe
            var existe = await _context.Usuarios.AnyAsync(u => u.NombreUsuario == nombreUsuario);

            if (existe)
            {
                return null; // Usuario ya existe
            }
            else if (nombreUsuario == null || contrasena == null)
            {
                return null; // Datos incompletos
            }
            var usuario = new Usuario
            {
                NombreUsuario = nombreUsuario,
                Contrasena = contrasena,
                IdRol = idRol
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario?> LoginAsync(string nombreUsuario, string password)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

            if (usuario == null)
                return null;
            if (password != usuario.Contrasena)
            {
                return null;
            }
            else
            {
                return usuario;

            }
        }
    }
}
