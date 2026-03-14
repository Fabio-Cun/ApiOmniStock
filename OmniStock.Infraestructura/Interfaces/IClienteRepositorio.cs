using OmniStock.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Infraestructura.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<List<ClienteDominio>> ObtenerTodosAsync();
        Task<ClienteDominio?> ObtenerPorIdAsync(int id);
        Task<ClienteDominio> CrearAsync(int cedula, string nombre, string apellido, string email, string telefono);
        Task ActualizarAsync(ClienteDominio cliente);
        Task EliminarAsync(int id);
    }
}
