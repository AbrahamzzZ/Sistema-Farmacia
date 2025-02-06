using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public string? NombreCompleto { get; set; }

        public string? CorreoElectronico { get; set; }

        public int? IdRol { get; set; }

        public string? RolDescripcion { get; set; }

        public string? Clave { get; set; }

        public int? Activo { get; set; }
    }
}
