using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.DTO.UsuariosDtos
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string? NombreCompleto { get; set; }
        public int IdRol { get; set; }
    }
}
