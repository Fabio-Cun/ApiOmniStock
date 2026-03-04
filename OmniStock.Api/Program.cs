
using Microsoft.OpenApi;
using DotNetEnv;
using OmniStock.Infraestructura;
using OmniStock.Infraestructura.InyeccionDependencias;
namespace OmniStock.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Env.Load(); // Carga las variables de entorno desde el archivo .env

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.DependenciasInfraestructura(builder.Configuration);

            // Add services to the container.

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
