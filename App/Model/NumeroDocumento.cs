using System;
using System.Collections.Generic;

namespace Model;

public partial class NumeroDocumento
{
    public int Id { get; set; }

    public int UltimoNumero { get; set; }

    public DateTime? FechaRegistro { get; set; }
}
