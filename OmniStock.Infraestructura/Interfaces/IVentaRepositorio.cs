using OmniStock.Dominio;
using OmniStock.Infraestructura.Modelos;

namespace OmniStock.Infraestructura.Interfaces
{
    public interface IVentaRepositorio
    {
        Task<List<VentaDominio>> ObtenerTodasAsync();
        Task<VentaDominio?> ObtenerPorIdAsync(int id);
        Task<List<VentaDominio>> ObtenerPorClienteAsync(int idCliente);
        Task<List<VentaDominio>> ObtenerPorUsuarioAsync(int idUsuario);
        Task<List<VentaDominio>> ObtenerPorRangoFechaAsync(DateTime desde, DateTime hasta);
        Task<VentaDominio> CrearAsync(int? idCliente, int idUsuario, List<DetalleVentaDominio> detalles);
        Task EliminarAsync(int id);
    }
}
