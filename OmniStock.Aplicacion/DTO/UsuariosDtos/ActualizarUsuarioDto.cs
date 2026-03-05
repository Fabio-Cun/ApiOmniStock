using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.DTO.UsuariosDtos
{
    public class ActualizarUsuarioDto
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string? NombreCompleto { get; set; }
        public string? Contrasena { get; set; }
        public int IdRol { get; set; }
    }
}
