﻿using System;
using System.Collections.Generic;

namespace Model;

public partial class Producto
{
    public int Id { get; set; }

    public string? NombreProducto { get; set; }

    public int? IdCategoria { get; set; }

    public int? Stock { get; set; }

    public decimal? Precio { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }
}
