using Microsoft.EntityFrameworkCore;
using OmniStock.Dominio;
using OmniStock.Infraestructura.Datos;
using OmniStock.Infraestructura.Interfaces;
using OmniStock.Infraestructura.Modelos;

namespace OmniStock.Infraestructura.Repositorios
{
    public class CategoriaRepositorio:ICategoriaRepositorio
    {
        private readonly OmniStockDbContext _context;

        public CategoriaRepositorio(OmniStockDbContext context)
        {
            _context = context;
        }


        public async Task<List<CategoriaDominio>> ObtenerTodasAsync()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return categorias.Select(MapToDomain).ToList();
        }

        public async Task<CategoriaDominio?> ObtenerPorIdAsync(int id)
        {
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.IdCategoria == id);

            if (categoria == null)
                return null;

            return MapToDomain(categoria);
        }

        public async Task<CategoriaDominio> CrearAsync(string nombreCategoria)
        {
            var categoria = new Categoria
            {
                NombreCategoria = nombreCategoria
            };

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return MapToDomain(categoria);
        }

        public async Task ActualizarAsync(CategoriaDominio categoriaDominio)
        {
            var categoria = await _context.Categorias.FindAsync(categoriaDominio.IdCategoria);

            if (categoria == null)
                return;

            categoria.NombreCategoria = categoriaDominio.NombreCategoria;

            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
        }

        private static CategoriaDominio MapToDomain(Categoria categoria)
        {
            return new CategoriaDominio
            {
                IdCategoria = categoria.IdCategoria,
                NombreCategoria = categoria.NombreCategoria
            };
        }
    }
}
