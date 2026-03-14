namespace OmniStock.Dominio
{
    public class ClienteDominio
    {
        public int IdCliente { get; set; }
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public List<VentaDominio> Venta { get; set; } = new List<VentaDominio>();

    }
}
