using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.DTO.ClientesDto
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
