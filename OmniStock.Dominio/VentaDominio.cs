using System;
using System.Collections.Generic;
using System.Text;
using static OmniStock.Dominio.DetalleVentaDominio;

namespace OmniStock.Dominio
{
    public class VentaDominio
    {
        public int IdVenta { get; set; }

        public int? IdCliente { get; set; }

        public int IdUsuario { get; set; }

        public DateTime FechaVenta { get; set; }

        public decimal Total { get; set; }

        public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();

        // Navegaciones opcionales
        public ClienteDominio? Cliente { get; set; }

        public UsuarioDominio? Usuario { get; set; }
    }
}
