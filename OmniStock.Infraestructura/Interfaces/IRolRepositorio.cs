using OmniStock.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Infraestructura.Interfaces
{
    public interface IRolRepositorio
    {
        Task<RolDominio?> ObtenerPorIdAsync(int idRol);
        Task<List<RolDominio>> ListarAsync();
        Task<RolDominio> InsertarAsync(RolDominio rolDominio);
        Task<bool> ActualizarAsync(RolDominio rolDominio);
        Task<bool> EliminarAsync(int idRol);
    }
}
