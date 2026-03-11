using System;
using DotNetEnv;
using Microsoft.OpenApi;
using OmniStock.Aplicacion.InyeccionDependencias;
using OmniStock.Infraestructura.InyeccionDependencias;
namespace OmniStock.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Env.Load(); // Carga las variables de entorno desde el archivo .env

            var builder = WebApplication.CreateBuilder(args);

            // se registran las dependencias de la infraestructura y la aplicación
            builder.Services.DependenciasInfraestructura();
            builder.Services.DependenciasAplicacion();


            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",  new OpenApiInfo
                {
                    Title = "ApiOmniStock",
                    Version = "v1",
                    Description = "API para gestión de inventario"
                });
            });

            // CORS: usa ALLOWED_ORIGINS (coma-separados). Si no existe, permite cualquier origen.
            var allowedOrigins = Environment.GetEnvironmentVariable("ALLOWED_ORIGINS");
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    if (!string.IsNullOrWhiteSpace(allowedOrigins))
                    {
                        var origins = allowedOrigins.Split(',', StringSplitOptions.RemoveEmptyEntries);
                        policy.WithOrigins(origins)
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    }
                    else
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            /*if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            */
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            // habilitar CORS antes de la autorización y de mapear controladores
            app.UseCors("CorsPolicy");

            app.UseAuthorization();
                    

            app.MapControllers();

            app.Run();
        }
    }
}
