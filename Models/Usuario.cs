namespace ApiOmniStock.Models
{
    public class Usuario
    {
        public Usuario() { }

        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public int IdRol { get; set; }
        public Rol? Rol { get; set; }

        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}
