using OmniStock.Aplicacion.DTO;
using OmniStock.Aplicacion.DTO.ClientesDto;
using OmniStock.Aplicacion.Interfaces;
using OmniStock.Dominio;
using OmniStock.Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.Servicios
{
    public class ClienteServicio: IclienteServicio
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteServicio(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        /// <summary>
        /// Obtiene todos los clientes registrados.
        /// </summary>
        public async Task<List<ClienteDto>> ObtenerTodosAsync()
        {
            var clientes = await _clienteRepositorio.ObtenerTodosAsync();

            var resultado = new List<ClienteDto>();

            foreach (var cliente in clientes)
            {
                resultado.Add(new ClienteDto
                {
                    IdCliente = cliente.IdCliente,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Email = cliente.Email,
                    Telefono = cliente.Telefono,
                    FechaRegistro = cliente.FechaRegistro
                });
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene un cliente por su ID. Lanza excepción si no existe.
        /// </summary>
        public async Task<ClienteDto> ObtenerPorIdAsync(int id)
        {
            var cliente = await _clienteRepositorio.ObtenerPorIdAsync(id);

            if (cliente == null)
                throw new KeyNotFoundException($"No se encontró el cliente con ID {id}.");

            var resultado = new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                FechaRegistro = cliente.FechaRegistro
            };

            return resultado;
        }

        /// <summary>
        /// Registra un nuevo cliente con sus datos de contacto.
        /// </summary>
        public async Task<ClienteDto> CrearAsync(string nombre, string apellido, string email, string telefono)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del cliente es obligatorio.");

            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido del cliente es obligatorio.");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email del cliente es obligatorio.");

            var cliente = await _clienteRepositorio.CrearAsync(
                nombre.Trim(),
                apellido.Trim(),
                email.Trim(),
                telefono?.Trim() ?? string.Empty);

            return new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                FechaRegistro = cliente.FechaRegistro
            };
        }

        /// <summary>
        /// Actualiza los datos de un cliente existente.
        /// </summary>
        public async Task ActualizarAsync(int id, string nombre, string apellido, string email, string telefono)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del cliente es obligatorio.");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email del cliente es obligatorio.");

            var cliente = await _clienteRepositorio.ObtenerPorIdAsync(id)
                ?? throw new KeyNotFoundException($"No se encontró el cliente con ID {id}.");

            cliente.Nombre = nombre.Trim();
            cliente.Apellido = apellido.Trim();
            cliente.Email = email.Trim();
            cliente.Telefono = telefono?.Trim() ?? string.Empty;

            await _clienteRepositorio.ActualizarAsync(cliente);
        }

        /// <summary>
        /// Elimina un cliente por su ID.
        /// </summary>
        public async Task EliminarAsync(int id)
        {
            var cliente = await _clienteRepositorio.ObtenerPorIdAsync(id)
                ?? throw new KeyNotFoundException($"No se encontró el cliente con ID {id}.");

            await _clienteRepositorio.EliminarAsync(cliente.IdCliente);
        }
    }
}