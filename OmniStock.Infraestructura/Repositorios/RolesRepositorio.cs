using Microsoft.EntityFrameworkCore;
using OmniStock.Dominio;
using OmniStock.Infraestructura.Datos;
using OmniStock.Infraestructura.Interfaces;
using OmniStock.Infraestructura.Modelos;

namespace OmniStock.Infraestructura.Repositorios
{
    public class RolesRepositorio : IRolRepositorio
    {
        private readonly OmniStockDbContext _context;

        public RolesRepositorio(OmniStockDbContext context)
        {
            _context = context;
        }
        // Listar todos los roles
        public async Task<List<RolDominio>> ListarAsync()
        {
            var roles = await _context.Roles.ToListAsync();

            return roles.Select(MapToDomain).ToList();
        }

        // Obtener rol por Id
        public async Task<RolDominio?> ObtenerPorIdAsync(int idRol)
        {
            var rol = await _context.Roles.FirstOrDefaultAsync(r => r.IdRol == idRol);
            if (rol == null) return null;

            return MapToDomain(rol);
        }

        // Insertar rol
        public async Task<RolDominio> InsertarAsync(RolDominio rolDominio)
        {
            var nuevoRol = new Role
            {
                NombreRol = rolDominio.NombreRol
            };

            _context.Roles.Add(nuevoRol);
            await _context.SaveChangesAsync();

            return MapToDomain(nuevoRol);
        }

        // Actualizar rol
        public async Task<bool> ActualizarAsync(RolDominio rolDominio)
        {
            var rolExistente = await _context.Roles.FirstOrDefaultAsync(r => r.IdRol == rolDominio.IdRol);
            if (rolExistente == null) return false;

            rolExistente.NombreRol = rolDominio.NombreRol;
            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar rol
        public async Task<bool> EliminarAsync(int idRol)
        {
            var rol = await _context.Roles.FirstOrDefaultAsync(r => r.IdRol == idRol);
            if (rol == null) return false;

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
            return true;
        }

        // --- Mapeo de EF a Dominio ---
        private static RolDominio MapToDomain(Role rol)
        {
            return new RolDominio
            {
                IdRol = rol.IdRol,
                NombreRol = rol.NombreRol
            };
        }
    }

}