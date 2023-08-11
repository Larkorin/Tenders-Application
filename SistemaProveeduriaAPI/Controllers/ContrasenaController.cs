using DTOs;
using Microsoft.AspNetCore.Mvc;
using SistemaProveeduriaCORE;

namespace SistemaProveeduriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContrasenaController : Controller
    {
        [HttpGet]
        [Route("RetrieveById")]
        public async Task<IActionResult> RetrieveById(int id)
        {
            var contrasenaManager = new ContrasenaManager();

            var contrasena = contrasenaManager.RetrieveById(id);

            return Ok(contrasena);
        }

        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> RetrieveByAll()
        {
            var contrasenaManager = new ContrasenaManager();

            List<Contrasena> contrasenas = contrasenaManager.RetrieveAll();

            return Ok(contrasenas);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Contrasena contrasena)
        {
            var contrasenaManager = new ContrasenaManager();

            contrasenaManager.Create(contrasena);

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Contrasena contrasena)
        {
            var contrasenaManager = new ContrasenaManager();

            contrasenaManager.Update(contrasena);

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var contrasenaManager = new ContrasenaManager();

            contrasenaManager.Delete(id);

            return Ok();
        }

        [HttpGet]
        [Route("CheckLast5Passwords")]
        public async Task<IActionResult> CheckLast5Passwords(int idUsuario, string password)
        {
            var contrasenaManager = new ContrasenaManager();

            var confirmacion = contrasenaManager.CheckLast5Passwords(idUsuario, password);

            return Ok(confirmacion);

        }

        [HttpPost]
        [Route("SetNewPassword")]
        public async Task<IActionResult> SetNewPassword(int idUsuario, string password)
        {
            var contrasenaManager = new ContrasenaManager();

            contrasenaManager.SetNewPassword(idUsuario, password);

            return Ok();
        }
    }
}
