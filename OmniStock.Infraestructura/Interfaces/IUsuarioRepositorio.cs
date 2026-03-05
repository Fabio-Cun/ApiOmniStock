using OmniStock.Dominio;
using OmniStock.Infraestructura.Modelos;

namespace OmniStock.Infraestructura.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<UsuarioDominio> RegistrarAsync(string nombreUsuario,string nombreCompleto, string password, int idRol);
        Task<UsuarioDominio> LoginAsync(string nombreUsuario, string password);
        Task<List<UsuarioDominio>> ObtenerTodosUsuariosAsync();
        Task<UsuarioDominio> ObtenerPorIdAsync(int id);
        Task ActualizarUsuarioAsync(UsuarioDominio usuario);
        Task EliminarUsuarioAsync(int id);

    }
}
