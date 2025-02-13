﻿using System;
using System.Collections.Generic;

namespace Model;

public partial class Categoria
{
    public int Id { get; set; }

    public string? NombreCategoria { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
