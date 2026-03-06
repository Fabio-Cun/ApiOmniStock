using OmniStock.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Infraestructura.Interfaces
{
    public interface ICategoriaRepositorio
    {
        Task<List<CategoriaDominio>> ObtenerTodasAsync();
        Task<CategoriaDominio?> ObtenerPorIdAsync(int id);
        Task<CategoriaDominio> CrearAsync(string nombreCategoria);
        Task ActualizarAsync(CategoriaDominio categoria);
        Task EliminarAsync(int id);
    }
}
