using OmniStock.Aplicacion.DTO.ClientesDto;
using OmniStock.Aplicacion.DTO.UsuariosDtos;

namespace OmniStock.Aplicacion.DTO.VentasDtos
{
    public class VentaDto
    {
        public int IdVenta { get; set; }

        public int? IdCliente { get; set; }

        public int IdUsuario { get; set; }

        public DateTime FechaVenta { get; set; }

        public decimal Total { get; set; }

        public List<DetalleVentaDto> Detalles { get; set; } = new List<DetalleVentaDto>();

        public ClienteDto? Cliente { get; set; }

        public UsuarioDto? Usuario { get; set; }
    }
}
