using System;
using System.Collections.Generic;

namespace OmniStock.Infraestructura.Modelos;

public partial class Venta
{
    public int IdVenta { get; set; }

    public int? IdCliente { get; set; }

    public int IdUsuario { get; set; }

    public DateTime FechaVenta { get; set; }

    public decimal Total { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
