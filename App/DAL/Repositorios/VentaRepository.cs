using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DBContext;
using DAL.Repositorios.Contrato;
using Model;

namespace DAL.Repositorios
{
    public class VentaRepository:GenericRepository<Venta>, IVentaRepository
    {
        private readonly SistemaFarmaciaContext _context;

        public VentaRepository(SistemaFarmaciaContext dbcontext):base(dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta venta = new Venta();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Producto producto = _context.Productos.Where(p=>p.Id==dv.IdProducto).First();
                        producto.Stock=producto.Stock-dv.Cantidad;
                        _context.Productos.Update(producto);
                    }
                    await _context.SaveChangesAsync();

                    NumeroDocumento correlativo = _context.NumeroDocumentos.First();
                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro=DateTime.Now;

                    _context.NumeroDocumentos.Update(correlativo);
                    await _context.SaveChangesAsync();

                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros+correlativo.UltimoNumero.ToString();

                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);

                    modelo.NumeroDocumento = numeroVenta;
                    await _context.Venta.AddAsync(modelo);
                    await _context.SaveChangesAsync();

                    venta = modelo;

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return venta;
        }
    }
}
