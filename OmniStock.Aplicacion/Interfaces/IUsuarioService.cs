using OmniStock.Aplicacion.DTO;

namespace OmniStock.Aplicacion.Interfaces
{

    public interface IUsuarioService
    {
        Task<List<UsuarioDto>> ObtenerUsuariosAsync();
        Task<UsuarioDto?> ObtenerUsuarioPorIdAsync(int id);
        Task<UsuarioDto?> RegistrarAsync(string nombreUsuario, string contrasena, int idRol);
        Task<UsuarioDto?> LoginAsync(string nombreUsuario, string password);
        Task<bool> EliminarUsuarioAsync(int id);
    }

}
