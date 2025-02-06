using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using DTO;
using Model;

namespace Utility
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion Rol

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion Menu

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino => destino.RolDescripcion,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.NombreRol))
                .ForMember(destino => destino.Activo,
                opt => opt.MapFrom(origen => origen.Activo == true ? 1 : 0));

            CreateMap<Usuario, SesionDTO>().ForMember(destino => destino.RolDescripcion,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.NombreRol));

            CreateMap<UsuarioDTO, Usuario>().ForMember(destino => destino.IdRolNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.Activo,
                opt => opt.MapFrom(origen => origen.Activo == 1 ? true : false));
            #endregion Usuario

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion Categoria

            #region Producto
            CreateMap<Producto, ProductoDTO>()
                .ForMember(destino => destino.NombreProducto,
                otp => otp.MapFrom(origen => origen.IdCategoriaNavigation.NombreCategoria))
                .ForMember(destino => destino.Precio,
                otp => otp.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE"))))
                .ForMember(destino => destino.Activo,
                opt => opt.MapFrom(origen => origen.Activo == true ? 1 : 0));

            CreateMap<ProductoDTO, Producto>()
                .ForMember(destino => destino.IdCategoriaNavigation,
                otp => otp.Ignore())
                .ForMember(destino => destino.Precio,
                otp => otp.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-PE"))))
                .ForMember(destino => destino.Activo,
                opt => opt.MapFrom(origen => origen.Activo == 1 ? true : false));
            #endregion Producto

            #region Venta
            CreateMap<Venta, VentaDTO>()
                .ForMember(destino => destino.Total,
                otp => otp.MapFrom(origen => Convert.ToDecimal(origen.Total.Value, new CultureInfo("es-PE"))))
                .ForMember(destino => destino.FechaRegistro,
                otp => otp.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyyy")));

            CreateMap<VentaDTO, Venta>()
                .ForMember(destino => destino.Total,
                otp => otp.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PE"))));
            #endregion Venta

            #region DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(destino => destino.ProductoDescripcion,
                opt => opt.MapFrom(origen => origen.IdProductoNavigation.NombreProducto))
                .ForMember(destino => destino.Precio,
                otp => otp.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE"))))
                .ForMember(destino => destino.Total,
                otp => otp.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
                );

            CreateMap<DetalleVentaDTO, DetalleVenta>()
                .ForMember(destino => destino.Precio,
                otp => otp.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-PE"))))
                .ForMember(destino => destino.Total,
                otp => otp.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PE")))
                );
            #endregion Detalleventa

            #region Reporte
            CreateMap<DetalleVenta, ReporteDTO>()
                .ForMember(destino => destino.FechaRegistro,
                otp => otp.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyyy")))
                .ForMember(destino => destino.NumeroDocumento,
                otp => otp.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento))
                .ForMember(destino => destino.TipoPago,
                otp => otp.MapFrom(origen => origen.IdVentaNavigation.TipoPago))
                .ForMember(destino => destino.TotalVenta,
                otp => otp.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-PE"))))
                .ForMember(destino => destino.Producto,
                otp => otp.MapFrom(origen => origen.IdProductoNavigation.NombreProducto))
                .ForMember(destino => destino.Precio,
                otp => otp.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE"))))
                .ForMember(destino => destino.Total,
                otp => otp.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
                );
            #endregion Reporte
        }
    }
}
