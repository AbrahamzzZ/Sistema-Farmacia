﻿using System;
using System.Collections.Generic;

namespace Model;

public partial class Menu
{
    public int Id { get; set; }

    public string? NombreMenu { get; set; }

    public string? Icono { get; set; }

    public string? Url { get; set; }

    public virtual ICollection<MenuRol> MenuRols { get; set; } = new List<MenuRol>();
}
