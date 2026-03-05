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
        public static IServiceCollection DependenciasInfraestructura(this IServiceCollection services, IConfiguration configuration)
        {
          
            var connectionString = configuration["MYSQL_CONNECTION_STRING"];

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("MYSQL_CONNECTION_STRING no está configurado.");

            var serverVersion = ServerVersion.AutoDetect(connectionString);
            
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();  // Registrar el repositorio de usuario
            services.AddDbContext<OmniStockDbContext>(options =>
                options.UseMySql(connectionString, serverVersion)
            );
            

            return services;
        }
    }
}
