using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.DTO
{
    public class RegistrarUsuarioDto
    {
        public string NombreUsuario { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public int IdRol { get; set; }
    }
}
