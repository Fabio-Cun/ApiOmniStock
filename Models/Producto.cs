namespace ApiOmniStock.Models
{
    public class Producto
    {
        public Producto() { }

        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public DateTime CreadoEn { get; set; } = DateTime.Now;

        public int? IdCategoria { get; set; }
        public Categoria? Categoria { get; set; }

        public Inventario? Inventario { get; set; }
        public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }
}
