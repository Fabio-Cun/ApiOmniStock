using OmniStock.Aplicacion.DTO.RolesDtos;
using OmniStock.Aplicacion.Interfaces;
using OmniStock.Dominio;
using OmniStock.Infraestructura.Interfaces;

namespace OmniStock.Aplicacion.Servicios
{
    public class RolService : IRolservice
    {
        private readonly IRolRepositorio _rolesRepositorio;

        public RolService(IRolRepositorio rolesRepositorio)
        {
            _rolesRepositorio = rolesRepositorio;
        }

        public async Task<List<RolDto>> ObtenerRolesAsync()
        {
            var rolesDominio = await _rolesRepositorio.ListarAsync();
            return rolesDominio.Select(r => MapToDto(r)).ToList();
        }

        public async Task<RolDto?> ObtenerPorIdAsync(int idRol)
        {
            var rol = await _rolesRepositorio.ObtenerPorIdAsync(idRol);
            if (rol == null) return null;

            return MapToDto(rol);
        }

        public async Task<RolDto> CrearRolAsync(RolDto rolDto)
        {
            var rolDominio = new RolDominio
            {
                NombreRol = rolDto.NombreRol
            };

            var rolCreado = await _rolesRepositorio.InsertarAsync(rolDominio);
            return MapToDto(rolCreado);
        }

        public async Task<bool> ActualizarRolAsync(RolDto rolDto)
        {
            var rolDominio = new RolDominio
            {
                IdRol = rolDto.IdRol,
                NombreRol = rolDto.NombreRol
            };

            return await _rolesRepositorio.ActualizarAsync(rolDominio);
        }

        public async Task<bool> EliminarRolAsync(int idRol)
        {
            return await _rolesRepositorio.EliminarAsync(idRol);
        }

        // Mapeo dominio → DTO
        private static RolDto MapToDto(RolDominio rol)
        {
            return new RolDto
            {
                IdRol = rol.IdRol,
                NombreRol = rol.NombreRol
            };
        }
    }
}