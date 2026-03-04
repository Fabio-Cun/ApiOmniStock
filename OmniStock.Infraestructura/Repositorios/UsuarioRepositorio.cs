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
        public async Task<List<Usuario>> ObtenerTodosAsync()
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

        // Crear
        public async Task<Usuario> CrearAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        // Actualizar
        public async Task ActualizarAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        // Eliminar
        public async Task EliminarAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
