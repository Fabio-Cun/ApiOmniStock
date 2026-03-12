using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.DTO.VentasDtos
{
    public class ResumenVentasDto
    {
        public DateTime Desde { get; set; }

        public DateTime Hasta { get; set; }

        public int CantidadVentas { get; set; }

        public decimal TotalIngresos { get; set; }

        public decimal PromedioVenta { get; set; }
    }
}
