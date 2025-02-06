using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Servicios.Contrato;
using DTO;
using Utilidad;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaServicio;

        public CategoriaController(ICategoriaService categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<CategoriaDTO>>();
            try
            {
                rsp.status = true;
                rsp.value = await _categoriaServicio.Lista();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.mensaje = ex.Message;
            }
            return Ok(rsp);
        }
    }
}
