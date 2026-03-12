using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.DTO.ReporteDtos
{
    public class ReporteVentasDto
    {

            /// <summary>
            /// Fecha inicial del reporte.
            /// Formato: yyyy-MM-dd HH:mm:ss
            /// Ejemplo: 2026-03-01 00:00:00
            /// </summary>
            public DateTime Desde { get; set; } = DateTime.Today;

            /// <summary>
            /// Fecha final del reporte.
            /// Formato: yyyy-MM-dd HH:mm:ss
            /// Ejemplo: 2026-03-31 23:59:59
            /// </summary>
            public DateTime Hasta { get; set; } = DateTime.Now;
        }
    
}
