using Microsoft.EntityFrameworkCore;
using ApiOmniStock.Models;


namespace ApiOmniStock.Data
{
    public class ApiOmniStockContext : DbContext
    {
        public ApiOmniStockContext(DbContextOptions<ApiOmniStockContext> options)
            : base(options)
        {
        }

        // Tablas (DbSets) que representan tus entidades
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> VentaDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relaciones
            modelBuilder.Entity<Venta>()
                .HasMany(v => v.Detalles)
                .WithOne(d => d.Venta)
                .HasForeignKey(d => d.IdVenta);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);
        }
    }
}
