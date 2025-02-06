using System;
using System.Collections.Generic;

namespace Model;

public partial class Usuario
{
    public int Id { get; set; }

    public string? NombreCompleto { get; set; }

    public string? CorreoElectronico { get; set; }

    public int? IdRol { get; set; }

    public string? Clave { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}
