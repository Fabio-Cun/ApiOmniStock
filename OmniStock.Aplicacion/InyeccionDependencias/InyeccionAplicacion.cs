using Microsoft.Extensions.DependencyInjection;
using OmniStock.Aplicacion.Interfaces;
using OmniStock.Aplicacion.Servicios;
using OmniStock.Infraestructura.Interfaces;
using OmniStock.Infraestructura.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.InyeccionDependencias
{
    public static class InyeccionAplicacion
    {
        public static IServiceCollection DependenciasAplicacion(this IServiceCollection services)
        {
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IRolservice, RolService>();
            services.AddScoped<IInventarioServicio, InventarioServicio>();
            services.AddScoped<IVentasServicio, VentaServicio>();  
            services.AddScoped<IclienteServicio, ClienteServicio>();    

            return services;
        }
    }
}
