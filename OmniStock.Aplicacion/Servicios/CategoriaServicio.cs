using OmniStock.Dominio;
using OmniStock.Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.Servicios
{
    public class CategoriaServicio
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;

        public CategoriaServicio(ICategoriaRepositorio categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
        }

        /// <summary>
        /// Obtiene todas las categorías registradas.
        /// </summary>
        public async Task<List<CategoriaDominio>> ObtenerTodasAsync()
        {
            return await _categoriaRepositorio.ObtenerTodasAsync();
        }

        /// <summary>
        /// Obtiene una categoría por su ID. Lanza excepción si no existe.
        /// </summary>
        public async Task<CategoriaDominio> ObtenerPorIdAsync(int id)
        {
            var categoria = await _categoriaRepositorio.ObtenerPorIdAsync(id);

            if (categoria == null)
                throw new KeyNotFoundException($"No se encontró la categoría con ID {id}.");

            return categoria;
        }

        /// <summary>
        /// Crea una nueva categoría validando que el nombre no esté vacío.
        /// </summary>
        public async Task<CategoriaDominio> CrearAsync(string nombreCategoria)
        {
            if (string.IsNullOrWhiteSpace(nombreCategoria))
                throw new ArgumentException("El nombre de la categoría es obligatorio.");

            return await _categoriaRepositorio.CrearAsync(nombreCategoria.Trim());
        }

        /// <summary>
        /// Actualiza el nombre de una categoría existente.
        /// </summary>
        public async Task ActualizarAsync(int id, string nuevoNombre)
        {
            if (string.IsNullOrWhiteSpace(nuevoNombre))
                throw new ArgumentException("El nombre de la categoría es obligatorio.");

            var categoria = await _categoriaRepositorio.ObtenerPorIdAsync(id)
                ?? throw new KeyNotFoundException($"No se encontró la categoría con ID {id}.");

            categoria.NombreCategoria = nuevoNombre.Trim();
            await _categoriaRepositorio.ActualizarAsync(categoria);
        }

        /// <summary>
        /// Elimina una categoría por su ID.
        /// </summary>
        public async Task EliminarAsync(int id)
        {
            var categoria = await _categoriaRepositorio.ObtenerPorIdAsync(id)
                ?? throw new KeyNotFoundException($"No se encontró la categoría con ID {id}.");

            await _categoriaRepositorio.EliminarAsync(categoria.IdCategoria);
        }
    }
}
