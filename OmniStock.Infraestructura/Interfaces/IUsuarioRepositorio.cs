using OmniStock.Infraestructura.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Infraestructura.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario?> RegistrarAsync(string nombreUsuario, string password, int idRol);
        Task<Usuario?> LoginAsync(string nombreUsuario, string password);
        Task<List<Usuario>> ObtenerTodosUsuariosAsync();
        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario);
        Task ActualizarUsuarioAsync(Usuario usuario);
        Task EliminarUsuarioAsync(int id);
    }
}
