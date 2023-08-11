using DTOs;
using Microsoft.AspNetCore.Mvc;
using SistemaProveeduriaCORE;

namespace SistemaProveeduriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscuelaController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Escuela escuela)
        {
            var escuelaManager = new EscuelaManager();
            escuelaManager.Create(escuela);
            return Ok();
        }

        [HttpGet]
        [Route("RetrieveById")]
        public async Task<IActionResult> RetrieveById(int id)
        {
            var escuelaManager = new EscuelaManager();
            var escuela = escuelaManager.RetrieveById(id);
            return Ok(escuela);
        }

        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> RetrieveAll()
        {
            var escuelaManager = new EscuelaManager();
            List<Escuela> escuela = escuelaManager.RetrieveAll();
            return Ok(escuela);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Escuela escuela)
        {
            var escuelaManager = new EscuelaManager();
            escuelaManager.Update(escuela);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Escuela escuela)
        {
            var escuelaManager = new EscuelaManager();
            escuelaManager.Delete(escuela);
            return Ok();
        }
    }
}
