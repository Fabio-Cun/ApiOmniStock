using System;
using System.Collections.Generic;
using System.Text;
using static OmniStock.Dominio.DetalleVentaDominio;

namespace OmniStock.Dominio
{
    public class ProductoDominio
    {
        public int IdProducto { get; set; }

        public string NombreProducto { get; set; } = null!;

        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int? IdCategoria { get; set; }

        public DateTime CreadoEn { get; set; }

        public List<DetalleVentaDominio> DetalleVenta { get; set; } = new List<DetalleVentaDominio>();

        public CategoriaDominio? IdCategoriaNavigation { get; set; }

        public InventarioDominio? Inventario { get; set; }
    }
}
