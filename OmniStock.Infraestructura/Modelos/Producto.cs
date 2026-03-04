using System;
using System.Collections.Generic;

namespace OmniStock.Infraestructura.Modelos;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int? IdCategoria { get; set; }

    public DateTime CreadoEn { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual Inventario? Inventario { get; set; }
}
