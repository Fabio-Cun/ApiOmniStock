namespace ApiOmniStock.Models
{
    public class Venta
    {
        public Venta()
        {
            Detalles = new List<DetalleVenta>();
        }

        public int IdVenta { get; set; }
        public DateTime FechaVenta { get; set; } = DateTime.Now;
        public decimal Total { get; set; }

        public int? IdCliente { get; set; }
        public Cliente? Cliente { get; set; }

        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

        public ICollection<DetalleVenta> Detalles { get; set; }
    }
}
