using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OmniStock.Infraestructura.Datos;
using OmniStock.Infraestructura.Interfaces;
using OmniStock.Infraestructura.Repositorios;

namespace OmniStock.Infraestructura.InyeccionDependencias
{
    public static class InyeccionInfraestructura
    {
        public static IServiceCollection DependenciasInfraestructura(this IServiceCollection services)
        {
          
            var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("MYSQL_CONNECTION_STRING no está configurado.");

            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services.AddScoped<IVentaRepositorio, VentaRepositorio>(); // Registrar el repositorio de ventas
            services.AddScoped<IProductoRepositorio, ProductoRepositorio>(); // Registrar el repositorio de productos
            services.AddScoped<IInventarioRepositorio, InventarioRepositorio>(); // Registrar el repositorio de productos
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>(); // Registrar el repositorio de clientes
            services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>(); // Registrar el repositorio de categorías
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();  // Registrar el repositorio de usuario
            services.AddScoped<IRolRepositorio, RolesRepositorio>(); // Registrar el repositorio de roles
            services.AddDbContext<OmniStockDbContext>(options =>
                options.UseMySql(connectionString, serverVersion)
            );
            

            return services;
        }
    }
}
