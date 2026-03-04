using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Dominio
{
    public class InventarioDominio
    {
        public int IdInventario { get; set; }

        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public DateTime FechaIngreso { get; set; }

        // Navegación opcional
        public ProductoDominio? Producto { get; set; }
    }
}
