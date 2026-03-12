using OmniStock.Aplicacion.DTO.LoginDto;
using OmniStock.Aplicacion.DTO.UsuariosDtos;

namespace OmniStock.Aplicacion.Interfaces
{

    public interface IUsuarioService
    {
        Task<List<UsuarioDto>> ObtenerUsuariosAsync();
        Task<UsuarioDto?> ObtenerUsuarioPorIdAsync(int id);
        Task<UsuarioDto?> RegistrarAsync(RegistrarUsuarioDto registrar);
        Task<UsuarioDto?> LoginAsync(LoginRequestDto login);
        Task<bool> EliminarUsuarioAsync(int id);
        Task<ActualizarUsuarioDto>ActualizarUsuarioAsync(ActualizarUsuarioDto actualizar);
    }

}
