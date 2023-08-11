using DTOs;
using Microsoft.AspNetCore.Mvc;
using SistemaProveeduriaCORE;

namespace SistemaProveeduriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitacoraController : Controller
    {

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Bitacora bitacora)
        {
            var bitacoraManager = new BitacoraManager();

            bitacoraManager.Create(bitacora);

            return Ok();
        }

        [HttpGet]
        [Route("RetrieveByUserId")]
        public async Task<IActionResult> RetrieveByUserId(int userId)
        {
            var bitacoraManager = new BitacoraManager();
            List<Bitacora> bitacoras = bitacoraManager.RetrieveBitacorasByUserId(userId);
            return Ok(bitacoras);
        }
    }
}
