using System;
using System.Collections.Generic;

namespace OmniStock.Infraestructura.Modelos;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public int Cedula { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
