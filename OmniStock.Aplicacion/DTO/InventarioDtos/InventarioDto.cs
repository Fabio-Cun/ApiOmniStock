using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.DTO.InventarioDtos
{
    public class InventarioDto
    {
        public int IdInventario { get; set; }

        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public DateTime FechaIngreso { get; set; }

        public string? NombreProducto { get; set; }

        public decimal Precio { get; set; } = 0;
    }
}
