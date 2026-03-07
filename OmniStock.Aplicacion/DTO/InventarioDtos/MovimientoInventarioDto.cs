using OmniStock.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.DTO.InventarioDtos
{
    public class MovimientoInventarioDto
    {
        public int IdInventario { get; set; }

        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public DateTime FechaIngreso { get; set; }
    }
}
