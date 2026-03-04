using System;
using System.Collections.Generic;

namespace OmniStock.Infraestructura.Modelos;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string? NombreCompleto { get; set; }

    public int IdRol { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
