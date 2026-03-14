using Microsoft.EntityFrameworkCore;
using OmniStock.Dominio;
using OmniStock.Infraestructura.Datos;
using OmniStock.Infraestructura.Interfaces;
using OmniStock.Infraestructura.Modelos;

namespace OmniStock.Infraestructura.Repositorios
{
    public class ClienteRepositorio: IClienteRepositorio
    {
        private readonly OmniStockDbContext _context;

        public ClienteRepositorio(OmniStockDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClienteDominio>> ObtenerTodosAsync()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return clientes.Select(MapToDomain).ToList();
        }

        public async Task<ClienteDominio?> ObtenerPorIdAsync(int id)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.IdCliente == id);

            if (cliente == null)
                return null;

            return MapToDomain(cliente);
        }

        public async Task<ClienteDominio> CrearAsync(int cedula, string nombre, string apellido, string email, string telefono)
        {
            var cliente = new Cliente
            {
                NombreCompleto = $"{nombre} {apellido}",
                Cedula = cedula,
                Email = email,
                Telefono = telefono,
                FechaCreacion = DateTime.Now
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return MapToDomain(cliente);
        }

        public async Task ActualizarAsync(ClienteDominio clienteDominio)
        {
            var cliente = await _context.Clientes.FindAsync(clienteDominio.IdCliente);

            if (cliente == null)
                return;
            cliente.Cedula = clienteDominio.Cedula; 
            cliente.NombreCompleto = $"{clienteDominio.Nombre} {clienteDominio.Apellido}";
            cliente.Email = clienteDominio.Email;
            cliente.Telefono = clienteDominio.Telefono;

            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }

        private static ClienteDominio MapToDomain(Cliente cliente)
        {
            var partes = cliente.NombreCompleto?.Split(' ', 2) ?? [];

            return new ClienteDominio
            {
                IdCliente = cliente.IdCliente,
                Cedula = cliente.Cedula,
                Nombre = partes.Length > 0 ? partes[0] : string.Empty,
                Apellido = partes.Length > 1 ? partes[1] : string.Empty,
                Email = cliente.Email ?? string.Empty,
                Telefono = cliente.Telefono ?? string.Empty,
                FechaRegistro = cliente.FechaCreacion
            };
        }
    }
}
