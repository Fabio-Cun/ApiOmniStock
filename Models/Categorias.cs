namespace ApiOmniStock.Models
{
    public class Categoria
    {
        public Categoria() { }

        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;

        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
