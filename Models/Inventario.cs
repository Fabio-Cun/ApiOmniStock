namespace ApiOmniStock.Models
{
    public class Inventario
    {
        public Inventario() { }

        public int IdInventario { get; set; }
        public int IdProducto { get; set; }
        public Producto? Producto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaIngreso { get; set; } = DateTime.Now;
    }
}
