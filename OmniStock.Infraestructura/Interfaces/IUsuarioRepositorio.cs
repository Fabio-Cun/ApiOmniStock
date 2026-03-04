using OmniStock.Infraestructura.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Infraestructura.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<List<Usuario>> ObtenerTodosAsync();
        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario);
        Task<Usuario> CrearAsync(Usuario usuario);
        Task ActualizarAsync(Usuario usuario);
        Task EliminarAsync(int id);
    }
}
