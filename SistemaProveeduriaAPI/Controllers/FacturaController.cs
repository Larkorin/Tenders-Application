using DTOs;
using DTOs.ProyectoDTOs;
using Microsoft.AspNetCore.Mvc;
using SistemaProveeduriaCORE;

namespace SistemaProveeduriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : Controller
    {

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Factura factura)
        {
            var facturaManager = new FacturaManager();

            facturaManager.Create(factura);

            return Ok();
        }

        [HttpGet]
        [Route("RetriveByUserInCharge")]
        public async Task<IActionResult> RetriveByUserInCharge(int userId)
        {
            var facturaManager = new FacturaManager();
            List<Factura> facturas = facturaManager.RetriveByUserInCharge(userId);
            return Ok(facturas);
        }
        
        [HttpGet]
        [Route("RetriveByUserReceiver")]
        public async Task<IActionResult> RetriveByUserReceiver(int userId)
        {
            var facturaManager = new FacturaManager();
            List<Factura> facturas = facturaManager.RetriveByUserReceiver(userId);
            return Ok(facturas);
        }

        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> RetrieveAll()
        {
            var facturaManager = new FacturaManager();

            List<Factura> billsList = facturaManager.RetrieveAll();

            return Ok(billsList);
        }
    }
}
