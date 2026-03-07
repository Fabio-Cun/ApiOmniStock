using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.DTO.ProductosDtos
{
    public class ProductoDto
    {
        public int IdProducto { get; set; }

        public string NombreProducto { get; set; } = null!;

        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int? IdCategoria { get; set; }

        public string? NombreCategoria { get; set; }

        public int? CantidadStock { get; set; }

        public DateTime CreadoEn { get; set; }
    }
}
