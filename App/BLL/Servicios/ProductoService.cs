﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ProductoService:IProductoService
    {
        private readonly IGenericRepository<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository<Producto> productoRepositorio, IMapper mapper)
        {
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                var queryProducto = await _productoRepositorio.Consultar();
                var listaProductos = queryProducto.Include(cat=>cat.IdCategoriaNavigation).ToList();
                return _mapper.Map<List<ProductoDTO>>(listaProductos.ToList());
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var productoCreado = await _productoRepositorio.Crear(_mapper.Map<Producto>(modelo));

                if (productoCreado.Id == 0)
                {
                    throw new TaskCanceledException("No se pudo crear.");
                }

                return _mapper.Map<ProductoDTO>(productoCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var productoModelo = _mapper.Map<Producto>(modelo);
                var productoEncontrado = await _productoRepositorio.Obtener(u=>u.Id==modelo.Id);
                if (productoModelo == null)
                {
                    throw new TaskCanceledException("El producto no existe.");
                }

                productoEncontrado.NombreProducto = productoModelo.NombreProducto;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;
                productoEncontrado.Activo = productoModelo.Activo;
                bool respuesta = await _productoRepositorio.Editar(productoEncontrado);

                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo editar.");
                }

                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var productoEncontrado = await _productoRepositorio.Obtener(p=>p.Id==id);
                if (productoEncontrado == null)
                {
                    throw new TaskCanceledException("El producto no existe.");
                }

                bool respuesta =  await _productoRepositorio.Eliminar(productoEncontrado);
                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo eliminar.");
                }

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
