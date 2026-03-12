using OmniStock.Aplicacion.DTO.ClientesDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.Interfaces
{
    public interface IclienteServicio
    {
        Task<List<ClienteDto>> ObtenerTodosAsync();
        Task<ClienteDto> ObtenerPorIdAsync(int id);
        Task<ClienteDto> CrearAsync(string nombre, string apellido, string email, string telefono);
        Task ActualizarAsync(int id, string nombre, string apellido, string email, string telefono);
        Task EliminarAsync(int id);

    }
}
