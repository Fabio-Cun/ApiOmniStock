namespace ApiOmniStock.Models
{
    public class Cliente
    {
        public Cliente() { }

        public int IdCliente { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}
