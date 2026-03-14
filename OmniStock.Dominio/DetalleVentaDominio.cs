using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Dominio
{
    public class DetalleVentaDominio
    {

        public int IdDetalle { get; set; }

        public int IdVenta { get; set; }

        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }

    }

}
