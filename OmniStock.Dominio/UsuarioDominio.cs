using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OmniStock.Dominio
{
    public class UsuarioDominio
    {
        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;

        public string? NombreCompleto { get; set; }

        public int IdRol { get; set; }

        public DateTime FechaCreacion { get; set; }

        // Navegación opcional
        public RolDominio? Rol { get; set; }

        public List<VentaDominio> Ventas { get; set; } = new List<VentaDominio>();
    }
}
