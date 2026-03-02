namespace ApiOmniStock.Models
{
    public class DetalleVenta
    {
        public DetalleVenta() { }

        public int IdDetalle { get; set; }
        public int IdVenta { get; set; }
        public Venta? Venta { get; set; }

        public int IdProducto { get; set; }
        public Producto? Producto { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
