using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductoDTO
    {
        public int Id { get; set; }

        public string? NombreProducto { get; set; }

        public int? IdCategoria { get; set; }

        public string? CategoriaDescripcion { get; set; }

        public int? Stock { get; set; }

        public string? Precio { get; set; }

        public int? Activo { get; set; }
    }
}
