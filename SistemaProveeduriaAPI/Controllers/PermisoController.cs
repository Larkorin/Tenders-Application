using DTOs;
using Microsoft.AspNetCore.Mvc;
using SistemaProveeduriaCORE;

namespace SistemaProveeduriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisoController : Controller
    {
        [HttpGet]
        [Route("RetrieveById")]
        public async Task<IActionResult> RetrieveById(int id)
        {
            var permisoManager = new PermisoManager();

            var permiso = permisoManager.RetrieveById(id);

            return Ok(permiso);
        }

        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> RetrieveByAll()
        {
            var permisoManager = new PermisoManager();

            List<Permiso> permisos = permisoManager.RetrieveAll();

            return Ok(permisos);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Permiso permiso)
        {
            var permisoManager = new PermisoManager();

            permisoManager.Create(permiso);

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Permiso permiso)
        {
            var permisoManager = new PermisoManager();

            permisoManager.Update(permiso);

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var permisoManager = new PermisoManager();

            permisoManager.Delete(id);

            return Ok();
        }
    }
}
