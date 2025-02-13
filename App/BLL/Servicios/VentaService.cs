﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BLL.Servicios.Contrato;
using DAL.Repositorios.Contrato;
using DTO;
using Model;

namespace BLL.Servicios
{
    public class VentaService:IVentaService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IGenericRepository<DetalleVenta> _detalleVentaRepositorio;
        private readonly IMapper _mapper;

        public VentaService(IVentaRepository ventaRepository, IGenericRepository<DetalleVenta> detalleVentaRepositorio, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _detalleVentaRepositorio = detalleVentaRepositorio;
            _mapper = mapper;
        }

        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try
            {
                var ventaGenerada = await _ventaRepository.Registrar(_mapper.Map<Venta>(modelo));
                if (ventaGenerada.Id == 0)
                {
                    throw new TaskCanceledException("No se puede crear.");
                }

                return _mapper.Map<VentaDTO>(ventaGenerada);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<VentaDTO>> Historial(string buscar, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> query = await _ventaRepository.Consultar();
            var ListaResultado = new List<Venta>();
            try
            {
                if (buscar=="fecha")
                {
                    DateTime fecha_Inicio = DateTime.ParseExact(fechaInicio,"dd/MM/yyyy", new CultureInfo("es-PER"));
                    DateTime fecha_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PER"));

                    ListaResultado = await query.Where(v => v.FechaRegistro.Value.Date >= fecha_Inicio && v.FechaRegistro.Value.Date <= fecha_Fin.Date)
                    .Include(dv=>dv.DetalleVenta).ThenInclude(p=>p.IdProductoNavigation).ToListAsync();
                }
                else
                {
                    ListaResultado = await query.Where(v => v.NumeroDocumento == numeroVenta)
                   .Include(dv => dv.DetalleVenta).ThenInclude(p => p.IdProductoNavigation).ToListAsync();
                }
            }
            catch
            {
                throw;
            }
            return _mapper.Map<List<VentaDTO>>(ListaResultado);
        }

        public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
        {
            IQueryable<DetalleVenta> query = await _detalleVentaRepositorio.Consultar();
            var ListaResultado = new List<DetalleVenta>();

            try
            {
                DateTime fecha_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PER"));
                DateTime fecha_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PER"));

                ListaResultado = await query.Include(p => p.IdProductoNavigation).Include(v => v.IdVentaNavigation)
                .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= fecha_Inicio.Date && dv.IdVentaNavigation.FechaRegistro.Value.Date <= fecha_Fin).ToListAsync();
            }
            catch
            {
                throw;
            }
            return _mapper.Map<List<ReporteDTO>>(ListaResultado);
        }
    }
}
