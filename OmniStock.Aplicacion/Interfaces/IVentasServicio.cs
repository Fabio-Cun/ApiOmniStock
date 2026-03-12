using OmniStock.Aplicacion.DTO.VentasDtos;
using OmniStock.Aplicacion.Servicios;
using OmniStock.Dominio;
using OmniStock.Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniStock.Aplicacion.Interfaces
{
    public interface IVentasServicio
    {
        Task<List<VentaDominio>> ObtenerTodasAsync();
        Task<VentaDominio> ObtenerPorIdAsync(int id);
        Task<List<VentaDominio>> ObtenerPorClienteAsync(int idCliente);
        Task<List<VentaDominio>> ObtenerPorUsuarioAsync(int idUsuario);
        Task<List<VentaDominio>> ObtenerPorRangoFechaAsync(DateTime desde, DateTime hasta);
        Task<VentaDominio> ProcesarVentaAsync(int? idCliente, int idUsuario, List<ItemVentaDto> items);
        Task CancelarVentaAsync(int idVenta);
        Task<ResumenVentasDto> ObtenerResumenVentasAsync(DateTime desde,DateTime hasta);


    }
}
