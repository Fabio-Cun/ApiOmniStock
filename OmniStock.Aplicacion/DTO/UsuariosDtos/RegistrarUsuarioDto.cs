using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.DTO.UsuariosDtos
{
    public class RegistrarUsuarioDto
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public int IdRol { get; set; }
    }
}
