using OmniStock.Aplicacion.DTO.RolesDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.Interfaces
{
    public interface IRolservice
    {
        Task<List<RolDto>> ObtenerRolesAsync();
        Task<RolDto?> ObtenerPorIdAsync(int idRol);
        Task<RolDto> CrearRolAsync(RolDto rol);
        Task<bool> ActualizarRolAsync(RolDto rol);
        Task<bool> EliminarRolAsync(int idRol);
    }
}
