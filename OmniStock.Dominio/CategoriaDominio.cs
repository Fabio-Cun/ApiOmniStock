namespace OmniStock.Dominio
{
    public class CategoriaDominio
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; } = null!; 
        public List<ProductoDominio> Productos { get; set; } = new List<ProductoDominio>();
    }
}
